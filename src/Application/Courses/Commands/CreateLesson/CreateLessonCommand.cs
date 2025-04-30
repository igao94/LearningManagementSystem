using Application.Core;
using Application.Courses.DTOs;
using MediatR;

namespace Application.Courses.Commands.CreateLesson;

public class CreateLessonCommand(CreateLessonDto createLessonDto) : IRequest<Result<LessonDto>>
{
    public CreateLessonDto CreateLessonDto { get; set; } = createLessonDto;
}
