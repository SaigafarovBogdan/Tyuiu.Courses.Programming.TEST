namespace Tyuiu.Courses.Programming.Core.Shared
{
	public class Error
	{
		public string Code { get; }
		public string Message { get; }
		public string? Property { get; }

		public Error(string code, string message, string? property = null)
		{
			Code = code;
			Message = message;
			Property = property;
		}

		public static Error Validation(string property, string message, string code = nameof(RequestResult.INVALID_DATA)) => 
			new(code, message, property);

		public static Error NotFound() =>
			new(nameof(RequestResult.NOT_FOUND), RequestResult.NOT_FOUND);

		public static Error Forbidden(string message = RequestResult.ACCESS_DENIED) =>
			new(nameof(RequestResult.ACCESS_DENIED), message);
	}
}
