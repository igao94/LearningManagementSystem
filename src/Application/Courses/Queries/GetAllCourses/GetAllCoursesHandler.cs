using Application.Core;
using Application.Courses.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Courses.Queries.GetAllCourses;

public class GetAllCoursesHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetAllCoursesQuery, Result<PagedList<CourseDto, DateTime?>>>
{
    public async Task<Result<PagedList<CourseDto, DateTime?>>> Handle(GetAllCoursesQuery request,
        CancellationToken cancellationToken)
    {
        var currentUserId = userAccessor.GetUserId();

        var (courses, nextCursor) = await unitOfWork.CourseRepository
            .GetAllCoursesAsync(request.CourseParams.Search,
                request.CourseParams.Filter,
                currentUserId,
                request.CourseParams.PageSize,
                request.CourseParams.Cursor);

        return Result<PagedList<CourseDto, DateTime?>>
            .Success(new PagedList<CourseDto, DateTime?>
            {
                Items = mapper.Map<IEnumerable<CourseDto>>(courses),
                NextCursor = nextCursor
            });
    }
}
