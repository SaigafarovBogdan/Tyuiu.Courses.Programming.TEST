using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Core.Abstractions
{
	public interface IResult
	{
		ResultStatus Status { get; }
		bool IsSuccess { get; }
		IEnumerable<Error> Errors { get; }
		object? GetValue();
		string Location { get; }
	}
}
