// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;

namespace Tyuiu.Courses.Programming.Api.Areas.Identity.Pages.Account
{
	public class LoginModel : PageModel
	{
		private readonly SignInManager<UserEntity> _signInManager;
		private readonly UserManager<UserEntity> _userManager;
		private readonly ILogger<LoginModel> _logger;

		public LoginModel(SignInManager<UserEntity> signInManager,
			ILogger<LoginModel> logger,
			UserManager<UserEntity> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_logger = logger;
		}

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		[BindProperty]
		public InputModel Input { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		public IList<AuthenticationScheme> ExternalLogins { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		public string ReturnUrl { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		[TempData]
		public string ErrorMessage { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		public class InputModel
		{
			/// <summary>
			///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
			///     directly from your code. This API may change or be removed in future releases.
			/// </summary>
			[Required]
			public string Login { get; set; }

			/// <summary>
			///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
			///     directly from your code. This API may change or be removed in future releases.
			/// </summary>
			[Required]
			[DataType(DataType.Password)]
			public string Password { get; set; }

			/// <summary>
			///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
			///     directly from your code. This API may change or be removed in future releases.
			/// </summary>
			[Display(Name = "Remember me?")]
			public bool RememberMe { get; set; }
		}

		public async Task<IActionResult> OnGetAsync(string returnUrl = null)
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Learn", "Courses");
			}

			if (!string.IsNullOrEmpty(ErrorMessage))
			{
				ModelState.AddModelError(string.Empty, ErrorMessage);
			}

			returnUrl ??= Url.Content("~/");

			// Clear the existing external cookie to ensure a clean login process
			await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

			ReturnUrl = returnUrl;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Learn", "Courses");
			}

			returnUrl ??= Url.Content("~/");

			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

			if (ModelState.IsValid)
			{
				// This doesn't count login failures towards account lockout
				// To enable password failures to trigger account lockout, set lockoutOnFailure: true
				Regex emailRegex = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
				if (emailRegex.IsMatch(Input.Login))
				{
					var user = await _userManager.FindByEmailAsync(Input.Login);
					Input.Login = user?.UserName ?? "";
				}

				var result = await _signInManager.PasswordSignInAsync(Input.Login, Input.Password, isPersistent: true, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					_logger.LogInformation("User logged in.");
					var user = await _signInManager.UserManager.FindByNameAsync(Input.Login);
					await CreateUserInfoClaims(user);
					return LocalRedirect(returnUrl);
				}
				if (result.RequiresTwoFactor)
				{
					return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
				}
				if (result.IsLockedOut)
				{
					_logger.LogWarning("Аккаунт заблокирован");
					return RedirectToPage("./Lockout");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
					return Page();
				}
			}

			// If we got this far, something failed, redisplay form
			return Page();
		}
		public async Task CreateUserInfoClaims(UserEntity user)
		{
			var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
			identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
			identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
			identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
			var userRoles = await _userManager.GetRolesAsync(user);
			var rolesString = string.Join(",", userRoles);
			identity.AddClaim(new Claim("UserRoles", rolesString));
			identity.AddClaim(new Claim("Name", user.Name));
			identity.AddClaim(new Claim("Surname", user.Surname));
			if (!string.IsNullOrEmpty(user.Patronymic))
			{
				identity.AddClaim(new Claim("Patronymic", user.Patronymic));
			}
			await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));
		}
	}
}
