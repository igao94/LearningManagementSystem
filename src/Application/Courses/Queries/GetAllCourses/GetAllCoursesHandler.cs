using Application.Core;
using Application.Courses.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Courses.Queries.GetAllCourses;

public class GetAllCoursesHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetAllCoursesQuery, Result<IEnumerable<CourseDto>>>
{
    public async Task<Result<IEnumerable<CourseDto>>> Handle(GetAllCoursesQuery request,
        CancellationToken cancellationToken)
    {
        var currentUserId = userAccessor.GetUserId();

        var courses = await unitOfWork.CourseRepository.GetAllCoursesAsync(request.CourseParams.Search,
            request.CourseParams.Filter,
            currentUserId);

        return Result<IEnumerable<CourseDto>>.Success(mapper.Map<IEnumerable<CourseDto>>(courses));
    }
}
