using Microsoft.AspNetCore.Mvc;
using Tyuiu.Courses.Programming.Core.Enums;

namespace Tyuiu.Courses.Programming.Api.Filters.ResultToAction
{
	public class ResultStatusOptions
	{
		public int StatusCode { get; set; }
		public Type? ResponseType { get; set; }

		public int GetStatusCode(string httpMethod)
		{
			if (StatusCode == 200 && httpMethod == "POST")
				return 201;
			return StatusCode;
		}
	}

	public class ResultStatusMapping
	{
		private readonly Dictionary<ResultStatus, ResultStatusOptions> _map = [];

		public ResultStatusMapping AddDefaultMap()
		{
			_map[ResultStatus.Ok] = new() { StatusCode = 200 };
			_map[ResultStatus.Created] = new() { StatusCode = 201 };
			_map[ResultStatus.NoContent] = new() { StatusCode = 204 };
			_map[ResultStatus.Error] = new() { StatusCode = 400, ResponseType = typeof(ProblemDetails) };
			_map[ResultStatus.Invalid] = new() { StatusCode = 400, ResponseType = typeof(ValidationProblemDetails) };
			_map[ResultStatus.NotFound] = new() { StatusCode = 404, ResponseType = typeof(ProblemDetails) };
			_map[ResultStatus.Conflict] = new() { StatusCode = 409, ResponseType = typeof(ProblemDetails) };
			_map[ResultStatus.Forbidden] = new() { StatusCode = 403, ResponseType = typeof(ProblemDetails) };
			_map[ResultStatus.Unauthorized] = new() { StatusCode = 401, ResponseType = typeof(ProblemDetails) };
			_map[ResultStatus.CriticalError] = new() { StatusCode = 500, ResponseType = typeof(ProblemDetails) };
			_map[ResultStatus.Unavailable] = new() { StatusCode = 503, ResponseType = typeof(ProblemDetails) };
			return this;
		}

		public ResultStatusOptions GetMapping(ResultStatus status) => _map[status];
	}
}
