using MediatR;
using Tyuiu.Courses.Programming.Application.Abstractions;
using Tyuiu.Courses.Programming.Core.Shared;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Abstractions;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.CreateDiscipline
{
	public record class CreateDisciplineCommand(string DisciplineName) : ICommand;

	internal class CreateDisciplineCommandHandler : IRequestHandler<CreateDisciplineCommand, Result>
	{
		private readonly IUnitOfWork _uow;
		private readonly IUserService _userService;

		public CreateDisciplineCommandHandler(
			IUnitOfWork uow,
			IUserService userService)
		{
			_uow = uow;
			_userService = userService;
		}

		public async Task<Result> Handle(CreateDisciplineCommand request, CancellationToken cancellationToken)
		{
			var currentUserId = _userService.GetCurrentUserId();

			if (!await _uow.Disciplines.IsUniqueName(request.DisciplineName, cancellationToken: cancellationToken))
			{
				var error = new Error(
					code: "Discipline.AlreadyExists",
					message: "Дисциплина с таким именем уже существует",
					property: nameof(request.DisciplineName)
				);
				return Result<int>.Conflict(error);
			}

			_uow.Disciplines.Add([new DisciplineEntity() {
				Name = request.DisciplineName,
				AuthorId = currentUserId}
			]);
			await _uow.SaveChangesAsync(cancellationToken);

			return Result.Success();
		}
	}
}
