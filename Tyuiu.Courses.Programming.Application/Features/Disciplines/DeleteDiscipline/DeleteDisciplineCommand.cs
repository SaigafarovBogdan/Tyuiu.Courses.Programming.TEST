using MediatR;
using Tyuiu.Courses.Programming.Application.Abstractions;
using Tyuiu.Courses.Programming.Core.Constants;
using Tyuiu.Courses.Programming.Core.Shared;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Abstractions;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites.Extensions;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.DeleteDiscipline
{
	public record class DeleteDisciplineCommand(int Id) : ICommand;

	internal class DeleteDisciplineCommandHandler : IRequestHandler<DeleteDisciplineCommand, Result>
	{
		private readonly IUnitOfWork _uow;
		private readonly IUserService _userService;

		public DeleteDisciplineCommandHandler(
			IUnitOfWork uow,
			IUserService userService)
		{
			_uow = uow;
			_userService = userService;
		}

		public async Task<Result> Handle(DeleteDisciplineCommand request, CancellationToken cancellationToken)
		{
			var discipline = await _uow.Disciplines.GetByIdAsync(request.Id, cancellationToken);
			if (discipline is null)
			{
				return Result.NotFound(Error.NotFound());
			}

			if (discipline.AuthorId != _userService.GetCurrentUserId())
			{
				return Result.Forbidden(Error.Forbidden(RequestResult.ACCESS_DENIED));
			}

			if (discipline.HasCourses())
			{
				return Result.Conflict(new Error(
					code: "Discipline.HasCourses",
					message: "Есть запущенные курсы по данной дисциплине",
					property: nameof(discipline.Courses)
				));
			}

			_uow.Disciplines.Remove([discipline]);
			await _uow.SaveChangesAsync(cancellationToken);

			return Result.Success();
		}
	}
}
