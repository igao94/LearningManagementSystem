using Application.Courses;
using Application.Courses.DTOs;
using Application.Courses.Queries.GetAllCourses;
using Application.Courses.Queries.GetCourseById;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CoursesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses([FromQuery] CourseParams courseParams)
    {
        return HandleResult(await Mediator.Send(new GetAllCoursesQuery(courseParams)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>> GetCourseById(string id)
    {
        return HandleResult(await Mediator.Send(new GetCourseByIdQuery(id)));
    }
}
