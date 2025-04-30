using Application.Core;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Courses.Commands.UpdateLesson;

public class UpdateLessonHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateLessonCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await unitOfWork.CourseRepository.GetLessonByIdAsync(request.UpdateLessonDto.Id);

        if (lesson is null)
        {
            return Result<Unit>.Failure("Lesson not found.", 404);
        }

        mapper.Map(request.UpdateLessonDto, lesson);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update lesson.", 400);
    }
}
