using Microsoft.AspNetCore.Mvc;
using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Api.Extensions
{
	public static class ControllerResultExtensions
	{
		public static IActionResult ViewResult<T>(this Controller controller, Result<T> result, string? viewName = null)
		{
			if (result.IsSuccess)
			{
				return viewName == null
					? controller.View(result.Value)
					: controller.View(viewName, result.Value);
			}

			return controller.HandleErrorResult(result);
		}

		public static IActionResult PartialViewResult<T>(this Controller controller, string partialViewName, Result<T> result)
		{
			if (result.IsSuccess)
			{
				return controller.PartialView(partialViewName, result.Value);
			}

			return controller.HandleErrorResult(result);
		}

		public static async Task<IActionResult> ViewResultAsync<T>(
			this Controller controller,
			Task<Result<T>> resultTask,
			string? viewName = null)
		{
			var result = await resultTask;
			return controller.ViewResult(result, viewName);
		}

		public static async Task<IActionResult> PartialViewResultAsync<T>(
			this Controller controller,
			string partialViewName,
			Task<Result<T>> resultTask)
		{
			var result = await resultTask;
			return controller.PartialViewResult(partialViewName, result);
		}

		public static IActionResult CommandResult(
			this Controller controller,
			Result result,
			string? successRedirectUrl = null,
			object? routeValues = null)
		{
			if (result.IsSuccess)
			{
				if (!string.IsNullOrEmpty(successRedirectUrl))
					return controller.RedirectToAction(successRedirectUrl, routeValues);

				return controller.Ok();
			}

			return controller.HandleErrorResult(result);
		}

		public static async Task<IActionResult> CommandResultAsync(
			this Controller controller,
			Task<Result> resultTask,
			string? successRedirectUrl = null,
			object? routeValues = null)
		{
			var result = await resultTask;
			return controller.CommandResult(result, successRedirectUrl, routeValues);
		}


		public static IActionResult JsonResult<T>(this Controller controller, Result<T> result)
		{
			if (result.IsSuccess)
			{
				return controller.Json(new { success = true, data = result.Value });
			}

			return controller.Json(new
			{
				success = false,
				errors = result.Errors,
				message = result.Errors.FirstOrDefault()?.Message
			});
		}

		public static async Task<IActionResult> JsonResultAsync<T>(
			this Controller controller,
			Task<Result<T>> resultTask)
		{
			var result = await resultTask;
			return controller.JsonResult(result);
		}

		private static IActionResult HandleErrorResult(this Controller controller, Result result)
		{
			var firstError = result.Errors.FirstOrDefault();

			if (controller.Request.IsAjaxRequest())
			{
				return controller.Json(new
				{
					success = false,
					message = firstError?.Message,
					errors = result.Errors,
					errorCode = firstError?.Code
				});
			}

			controller.TempData["ErrorMessage"] = firstError?.Message;
			controller.TempData["ErrorCode"] = firstError?.Code;

			if (controller.Request.Headers.Referer.ToString() != null)
			{
				return controller.Redirect(controller.Request.Headers.Referer.ToString());
			}

			return controller.RedirectToAction("Index");
		}

		private static bool IsAjaxRequest(this HttpRequest request)
		{
			return request.Headers.XRequestedWith == "XMLHttpRequest";
		}
	}
}
