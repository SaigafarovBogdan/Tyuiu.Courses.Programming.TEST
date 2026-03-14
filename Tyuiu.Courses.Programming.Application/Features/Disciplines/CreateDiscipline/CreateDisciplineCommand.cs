using System;
using System.Collections.Generic;
using System.Text;
using Tyuiu.Courses.Programming.Application.Abstractions;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.CreateDiscipline
{
	public record class CreateDisciplineCommand(string DisciplineName) : ICommand;
}
