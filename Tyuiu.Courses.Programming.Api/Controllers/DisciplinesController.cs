using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tyuiu.Courses.Programming.Api.Extensions;
using Tyuiu.Courses.Programming.Application.Features.Disciplines.CloneDiscipline;
using Tyuiu.Courses.Programming.Application.Features.Disciplines.CreateDiscipline;
using Tyuiu.Courses.Programming.Application.Features.Disciplines.DeleteDiscipline;
using Tyuiu.Courses.Programming.Application.Features.Disciplines.EditDiscipline;
using Tyuiu.Courses.Programming.Application.Features.Disciplines.GetDisciplinesPaginated;

namespace Tyuiu.Courses.Programming.Api.Controllers
{
	public class DisciplinesController : Controller
	{
		private readonly IMediator _mediator;

		public DisciplinesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[Authorize]
		public async Task<IActionResult> Index(GetDisciplinesPaginatedQuery request)
		{
			return await this.ViewResultAsync(_mediator.Send(request));
		}

		[Authorize]
		public async Task<IActionResult> SearchDisciplinePartialAsync(GetDisciplinesPaginatedQuery request)
		{
			return await this.PartialViewResultAsync("_DisciplineSearchResult", _mediator.Send(request));
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateDisciplineCommand request)
		{
			return await this.CommandResultAsync(_mediator.Send(request), nameof(Index));
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(EditDisciplineCommand request)
		{
			return await this.CommandResultAsync(_mediator.Send(request), nameof(Index));
		}

		[HttpPost, ActionName("Delete")]
		[Authorize]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(DeleteDisciplineCommand request)
		{
			return await this.CommandResultAsync(_mediator.Send(request), nameof(Index));
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Clone(CloneDisciplineCommand request)
		{
			return await this.CommandResultAsync(_mediator.Send(request), nameof(Index));
		}
	}
}
