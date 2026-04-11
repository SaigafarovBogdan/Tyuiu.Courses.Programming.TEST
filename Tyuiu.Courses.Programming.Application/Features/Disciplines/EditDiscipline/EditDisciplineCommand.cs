using MediatR;
using Microsoft.EntityFrameworkCore;
using Tyuiu.Courses.Programming.Application.Abstractions;
using Tyuiu.Courses.Programming.Core.Constants;
using Tyuiu.Courses.Programming.Core.Shared;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Abstractions;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.EditDiscipline
{
	public record class EditDisciplineCommand(
		int Id, 
		string DisciplineName): ICommand;

	internal class EditDisciplineCommandHandler : IRequestHandler<EditDisciplineCommand, Result>
	{
		private readonly IUnitOfWork _uow;
		private readonly IUserService _userService;

		public EditDisciplineCommandHandler(
			IUnitOfWork uow,
			IUserService userService)
		{
			_uow = uow;
			_userService = userService;
		}

		public async Task<Result> Handle(EditDisciplineCommand request, CancellationToken cancellationToken)
		{
			var currentUserId = _userService.GetCurrentUserId();

			var discipline = await _uow.Disciplines.GetByIdAsync(request.Id, cancellationToken);
			if (discipline is null)
			{
				return Result.NotFound(Error.NotFound());
			}

			if (discipline.AuthorId != currentUserId)
			{
				return Result.Forbidden(Error.Forbidden("Только автор может редактировать дисциплину"));
			}

			if (!await _uow.Disciplines.IsUniqueName(request.DisciplineName, request.Id, cancellationToken))
			{
				var error = new Error(
					code: "Discipline.AlreadyExists",
					message: "Дисциплина с таким именем уже существует",
					property: nameof(request.DisciplineName)
				);
				return Result.Conflict(error);
			}

			discipline.Name = request.DisciplineName;
			discipline.AuthorId = _userService.GetCurrentUserId();

			try
			{
				await _uow.SaveChangesAsync(cancellationToken);
				return Result.Success();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await _uow.Disciplines.IsExistsAsync(request.Id, cancellationToken))
				{
					return Result.NotFound(new Error(
						code: "Discipline.Deleted",
						message: RequestResult.NOT_FOUND
					));
				}
				else
				{
					return Result.Conflict(new Error(
						code: "Discipline.Conflict",
						message: RequestResult.SOMETHING_WENT_WRONG
					));
				}
			}
		}
	}
}
