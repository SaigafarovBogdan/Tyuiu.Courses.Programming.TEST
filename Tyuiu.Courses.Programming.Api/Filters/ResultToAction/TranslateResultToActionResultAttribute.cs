using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tyuiu.Courses.Programming.Core.Constants;
using Tyuiu.Courses.Programming.Core.Enums;
using Tyuiu.Courses.Programming.Core.Shared;
using IResult = Tyuiu.Courses.Programming.Core.Abstractions.IResult;

namespace Tyuiu.Courses.Programming.Api.Filters.ResultToAction
{
	public class TranslateResultToActionResultAttribute : ActionFilterAttribute
	{
		private static readonly ResultStatusMapping _mapping = new ResultStatusMapping().AddDefaultMap();

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			if (context.Result is ObjectResult objectResult)
			{
				IResult? result = objectResult.Value as IResult ??
								  (objectResult.Value as Result) ??
								  (objectResult.Value as Result<object>);

				if (result != null && context.Controller is ControllerBase controller)
				{
					context.Result = ToActionResult(controller, result);
				}
			}
		}

		private static ActionResult ToActionResult(ControllerBase controller, IResult result)
		{
			var options = _mapping.GetMapping(result.Status);

			var statusCode = options.GetStatusCode(controller.HttpContext.Request.Method);

			if (result.IsSuccess)
			{
				return result.Status switch
				{
					ResultStatus.Ok => controller.Ok(result.GetValue()),
					ResultStatus.Created => HandleCreated(controller, result),
					ResultStatus.NoContent => controller.NoContent(),
					_ => controller.StatusCode(statusCode, result.GetValue())
				};
			}

			return HandleError(controller, result, statusCode, options.ResponseType);
		}

		private static CreatedResult HandleCreated(ControllerBase controller, IResult result)
		{
			if (!string.IsNullOrEmpty(result.Location))
			{
				var httpRequest = controller.HttpContext.Request;
				var locationUri = new UriBuilder(
					httpRequest.Scheme,
					httpRequest.Host.Host,
					httpRequest.Host.Port ?? -1,
					result.Location).Uri.AbsoluteUri;

				return controller.Created(locationUri, result.GetValue());
			}

			return controller.Created(string.Empty, result.GetValue());
		}

		private static ActionResult HandleError(ControllerBase controller, IResult result, int statusCode, Type? responseType)
		{
			if (responseType == typeof(ValidationProblemDetails))
			{
				return CreateValidationProblemDetails(controller, result, statusCode);
			}

			return CreateProblemDetails(controller, result, statusCode);
		}

		private static ActionResult CreateValidationProblemDetails(ControllerBase controller, IResult result, int statusCode)
		{
			var errors = result.Errors
				.Where(e => e.Property != null)
				.GroupBy(e => e.Property!)
				.ToDictionary(
					g => g.Key,
					g => g.Select(e => e.Message).ToArray()
				);

			var problemDetails = new ValidationProblemDetails(errors)
			{
				Status = statusCode,
				Title = RequestResult.INVALID_DATA,
				Detail = result.Errors.FirstOrDefault()?.Message,
				Instance = controller.HttpContext.Request.Path
			};

			problemDetails.Extensions["errorCode"] = result.Errors.FirstOrDefault()?.Code;

			return controller.ValidationProblem(problemDetails);
		}

		private static ObjectResult CreateProblemDetails(ControllerBase controller, IResult result, int statusCode)
		{
			var problemDetails = new ProblemDetails
			{
				Status = statusCode,
				Title = result.Errors.FirstOrDefault()?.Message ?? RequestResult.SOMETHING_WENT_WRONG,
				Detail = result.Errors.FirstOrDefault()?.Code,
				Instance = controller.HttpContext.Request.Path
			};

			problemDetails.Extensions["errors"] = result.Errors;

			return controller.StatusCode(statusCode, problemDetails);
		}
	}
}