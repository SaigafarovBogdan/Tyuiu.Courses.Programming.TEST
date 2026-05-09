using MediatR;
using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Application.Abstractions
{
	public interface IQuery
	{
	}

	public interface IQuery<TResponse> : IRequest<Result<TResponse>>, IQuery
	{
	}
}
