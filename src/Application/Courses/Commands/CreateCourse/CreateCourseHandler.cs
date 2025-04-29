using Application.Core;
using Application.Courses.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Courses.Commands.CreateCourse;

public class CreateCourseHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateCourseCommand, Result<CourseDto>>
{
    public async Task<Result<CourseDto>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = mapper.Map<Course>(request.CreateCourseDto);

        unitOfWork.CourseRepository.AddCourse(course);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<CourseDto>.Success(mapper.Map<CourseDto>(course))
            : Result<CourseDto>.Failure("Failed to create course.", 400);
    }
}
