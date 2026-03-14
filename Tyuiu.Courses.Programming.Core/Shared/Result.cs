using Tyuiu.Courses.Programming.Core.Abstractions;

namespace Tyuiu.Courses.Programming.Core.Shared
{
	public class Result: IResult
	{
		public ResultStatus Status { get; protected set; }
		public bool IsSuccess => Status is ResultStatus.Ok or ResultStatus.Created or ResultStatus.NoContent;
		public IEnumerable<Error> Errors { get; protected set; } = [];
		public string Location { get; protected set; } = string.Empty;

		public virtual object? GetValue() => null;

		protected Result(ResultStatus status) => Status = status;
		protected Result(ResultStatus status, List<Error> errors) : this(status) => Errors = errors;

		public static Result Success() => new(ResultStatus.Ok);
		public static Result Created(string location = "") => new(ResultStatus.Created) { Location = location };
		public static Result NoContent() => new(ResultStatus.NoContent);

		public static Result Error(Error error) => new(ResultStatus.Error, new List<Error> { error });
		public static Result Error(List<Error> errors) => new(ResultStatus.Error, errors);
		public static Result Invalid(Error error) => new(ResultStatus.Invalid, new List<Error> { error });
		public static Result Invalid(List<Error> errors) => new(ResultStatus.Invalid, errors);
		public static Result NotFound(Error error) => new(ResultStatus.NotFound, new List<Error> { error });
		public static Result Conflict(Error error) => new(ResultStatus.Conflict, new List<Error> { error });
		public static Result Forbidden(Error error) => new(ResultStatus.Forbidden, new List<Error> { error });
		public static Result Unauthorized(Error error) => new(ResultStatus.Unauthorized, new List<Error> { error });
		public static Result CriticalError(Error error) => new(ResultStatus.CriticalError, new List<Error> { error });
		public static Result Unavailable(Error error) => new(ResultStatus.Unavailable, new List<Error> { error });
	}

	public class Result<T> : Result
	{
		private readonly T? _value;

		public T? Value => IsSuccess
			? _value!
			: throw new InvalidOperationException("Невозможно обратиться к Value, когда результат неуспешный.");

		public override object? GetValue() => _value;

		private Result(ResultStatus status, T value) : base(status) => _value = value;
		private Result(ResultStatus status, List<Error> errors) : base(status, errors) { }

		public static Result<T> Success(T value) => new(ResultStatus.Ok, value);
		public static Result<T> Created(T value, string location = "") => new(ResultStatus.Created, value) { Location = location };

		public new static Result<T> Error(Error error) => new(ResultStatus.Error, new List<Error> { error });
		public new static Result<T> Error(List<Error> errors) => new(ResultStatus.Error, errors);
		public new static Result<T> Invalid(Error error) => new(ResultStatus.Invalid, new List<Error> { error });
		public new static Result<T> Invalid(List<Error> errors) => new(ResultStatus.Invalid, errors);
		public new static Result<T> NotFound(Error error) => new(ResultStatus.NotFound, new List<Error> { error });
		public new static Result<T> Conflict(Error error) => new(ResultStatus.Conflict, new List<Error> { error });
		public new static Result<T> Forbidden(Error error) => new(ResultStatus.Forbidden, new List<Error> { error });
		public new static Result<T> Unauthorized(Error error) => new(ResultStatus.Unauthorized, new List<Error> { error });
		public new static Result<T> CriticalError(Error error) => new(ResultStatus.CriticalError, new List<Error> { error });
		public new static Result<T> Unavailable(Error error) => new(ResultStatus.Unavailable, new List<Error> { error });

		public static implicit operator Result<T>(T value) => Success(value);
		public static implicit operator Result<T>(Error error) => Error(error);
	}
}
