using Application.Core;
using Application.Courses.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Courses.Queries.GetCourseById;

public class GetCourseByIdHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetCourseByIdQuery, Result<CourseDto>>
{
    public async Task<Result<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await unitOfWork.CourseRepository.GetCourseByIdAsync(request.Id);

        if (course is null)
        {
            return Result<CourseDto>.Failure("Course not found", 404);
        }

        return Result<CourseDto>.Success(mapper.Map<CourseDto>(course));
    }
}
