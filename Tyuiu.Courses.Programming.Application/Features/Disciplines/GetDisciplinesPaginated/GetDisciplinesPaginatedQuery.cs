using MediatR;
using Tyuiu.Courses.Programming.Application.Abstractions;
using Tyuiu.Courses.Programming.Application.Dtos;
using Tyuiu.Courses.Programming.Core.Shared;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Abstractions;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.GetDisciplinesPaginated
{
	public record class GetDisciplinesPaginatedQuery(
		int Page = 1,
		int PageSize = 10,
		string? SearchTerm = null) : IQuery<DisciplineSearchModel>;

	internal class GetDisciplinesPaginatedQueryHandler : IRequestHandler<GetDisciplinesPaginatedQuery, Result<DisciplineSearchModel>>
	{
		private readonly IUnitOfWork _uow;
		private readonly IUserService _userService;

		public GetDisciplinesPaginatedQueryHandler(
			IUnitOfWork uow,
			IUserService userService)
		{
			_uow = uow;
			_userService = userService;
		}

		public async Task<Result<DisciplineSearchModel>> Handle(
			GetDisciplinesPaginatedQuery request,
			CancellationToken cancellationToken)
		{
			var userId = _userService.GetCurrentUserId();

			(ICollection<DisciplineEntity> disciplines, int totalCount) = await _uow.Disciplines.GetWithPagination(
				request.Page,
				request.PageSize,
				request.SearchTerm,
				userId,
				cancellationToken: cancellationToken);

			var resultModel = new DisciplineSearchModel
			{
				Disciplines = disciplines.Select(d => new DisciplineSearchItemDto
					{
						Id = d.Id,
						Name = d.Name,
						AuthorName = d.Author?.FullName ?? "Автор удалён",
						IsCurrentUserAuthor = d.AuthorId == userId,
						HasCourses = d.Courses?.Any() ?? false
					}).ToList(),
				Pagination = new Pagination
				{
					Pages = new Pages(request.Page, totalCount, request.PageSize),
					SearchTerm = request.SearchTerm
				}
			};

			return Result<DisciplineSearchModel>.Success(resultModel);
		}
	}
}
