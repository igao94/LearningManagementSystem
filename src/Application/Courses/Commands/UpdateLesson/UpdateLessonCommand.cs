using Application.Core;
using Application.Courses.DTOs;
using MediatR;

namespace Application.Courses.Commands.UpdateLesson;

public class UpdateLessonCommand(UpdateLessonDto updateLessonDto) : IRequest<Result<Unit>>
{
    public UpdateLessonDto UpdateLessonDto { get; set; } = updateLessonDto;
}
