using Application.Core;
using Application.Students.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Courses.Queries.GetCourseAttendees;

public class GetCourseAttendeesHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetCourseAttendeesQuery, Result<IEnumerable<StudentDto>>>
{
    public async Task<Result<IEnumerable<StudentDto>>> Handle(GetCourseAttendeesQuery request,
        CancellationToken cancellationToken)
    {
        var attendees = await unitOfWork.CourseRepository.GetCourseAttendees(request.Id);

        return Result<IEnumerable<StudentDto>>.Success(mapper.Map<IEnumerable<StudentDto>>(attendees));
    }
}
