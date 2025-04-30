using Application.Core;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Courses.Commands.UpdateCourse;

public class UpdateCourseHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateCourseCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await unitOfWork.CourseRepository.GetCourseByIdAsync(request.UpdateCourseDto.Id);

        if (course is null)
        {
            return Result<Unit>.Failure("Course not found.", 404);
        }

        mapper.Map(request.UpdateCourseDto, course);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update course.", 400);
    }
}
