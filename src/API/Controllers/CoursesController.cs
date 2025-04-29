using Application.Courses;
using Application.Courses.DTOs;
using Application.Courses.Queries.GetAllCourses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CoursesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses([FromQuery] CourseParams courseParams)
    {
        return HandleResult(await Mediator.Send(new GetAllCoursesQuery(courseParams)));
    }
}
