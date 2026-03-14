using MediatR;
using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Application.Abstractions
{
	public interface ICommand<TResponse> : IRequest<Result<TResponse>>
	{
	}

	public interface ICommand : IRequest<Result>
	{
	}
}
