using MediatR;
using Tyuiu.Courses.Programming.Application.Abstractions;
using Tyuiu.Courses.Programming.Core.Shared;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Abstractions;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;
using Tyuiu.Courses.Programming.Infrastructure.Providers.Identity;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.CloneDiscipline
{
	public record class CloneDisciplineCommand(
		int Id, 
		string? NewDisciplineName) : ICommand;

	internal class CloneDisciplineCommandHandler : IRequestHandler<CloneDisciplineCommand, Result>
	{
		private readonly IUnitOfWork _uow;
		private readonly IUserService _userService;

		public CloneDisciplineCommandHandler(
			IUnitOfWork uow,
			IUserService userService)
		{
			_uow = uow;
			_userService = userService;
		}

		public async Task<Result> Handle(CloneDisciplineCommand request, CancellationToken cancellationToken)
		{
			var sourceDiscipline = await _uow.Disciplines.GetByIdFullAsync(request.Id, cancellationToken);

			if (sourceDiscipline == null)
				return Result<int>.NotFound(Error.NotFound());

			var baseName = string.IsNullOrWhiteSpace(request.NewDisciplineName)
				? sourceDiscipline.Name
				: request.NewDisciplineName;

			var newDiscipline = new DisciplineEntity
			{
				Name = await GenerateUniqueDisciplineNameAsync(baseName, cancellationToken),
				AuthorId = _userService.GetCurrentUserId()
			};

			foreach (var sourceSprint in sourceDiscipline.Sprints ?? Enumerable.Empty<SprintEntity>())
			{
				var newSprint = new SprintEntity
				{
					Name = sourceSprint.Name,
					IndexNumber = sourceSprint.IndexNumber,
					Discipline = newDiscipline
				};

				newDiscipline.Sprints.Add(newSprint);

				foreach (var sourceTheme in sourceSprint.Themes ?? Enumerable.Empty<ThemeEntity>())
				{
					var newTheme = new ThemeEntity
					{
						Name = sourceTheme.Name,
						IndexNumber = sourceTheme.IndexNumber,
						Introduction = sourceTheme.Introduction,
						Theory = sourceTheme.Theory,
						VideoURL = sourceTheme.VideoURL,
						Sprint = newSprint
					};

					newSprint.Themes.Add(newTheme);

					foreach (var sourceFile in sourceTheme.Files ?? Enumerable.Empty<ThemeFileEntity>())
					{
						newTheme.Files.Add(new ThemeFileEntity
						{
							FileId = sourceFile.FileId,
							Theme = newTheme
						});
					}

					foreach (var sourceTask in sourceTheme.Tasks ?? Enumerable.Empty<ThemeTaskEntity>())
					{
						newTheme.Tasks.Add(new ThemeTaskEntity
						{
							Variant = sourceTask.Variant,
							Description = sourceTask.Description,
							Theme = newTheme
						});
					}

					if (sourceTheme.Test != null)
					{
						var newTest = new ThemeTestEntity
						{
							QuestionsCount = sourceTheme.Test.QuestionsCount,
							PassingScore = sourceTheme.Test.PassingScore,
							Duration = sourceTheme.Test.Duration,
							SavedByUser = sourceTheme.Test.SavedByUser,
							Theme = newTheme
						};
						newTheme.Test = newTest;

						foreach (var sourceQuestion in sourceTheme.Test.Questions ?? Enumerable.Empty<QuestionEntity>())
						{
							var newQuestion = new QuestionEntity
							{
								ThemeTest = newTest,
								Title = sourceQuestion.Title,
								Description = sourceQuestion.Description,
								Type = sourceQuestion.Type,
								IgnoreCase = sourceQuestion.IgnoreCase,
							};
							newTest.Questions.Add(newQuestion);

							foreach (var sourceAnswer in sourceQuestion.Answers ?? Enumerable.Empty<QuestionAnswerEntity>())
							{
								newQuestion.Answers.Add(new QuestionAnswerEntity
								{
									Text = sourceAnswer.Text,
									IsCorrect = sourceAnswer.IsCorrect,
									Question = newQuestion
								});
							}
						}
					}
				}
			}

			_uow.Disciplines.Add([newDiscipline]);
			await _uow.SaveChangesAsync(cancellationToken);

			return Result<int>.Success(newDiscipline.Id);
		}

		private async Task<string> GenerateUniqueDisciplineNameAsync(string baseName, CancellationToken cancellationToken)
		{
			var uniqueName = baseName;
			var counter = 1;

			while (!await _uow.Disciplines.IsUniqueName(uniqueName, cancellationToken: cancellationToken))
			{
				uniqueName = $"{baseName} - копия {counter}";
				counter++;
			}

			return uniqueName;
		}
	}
}
