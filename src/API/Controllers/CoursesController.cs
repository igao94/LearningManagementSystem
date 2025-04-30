using Application.Courses;
using Application.Courses.Commands.CreateCourse;
using Application.Courses.Commands.CreateLesson;
using Application.Courses.DTOs;
using Application.Courses.Queries.GetAllCourses;
using Application.Courses.Queries.GetCourseById;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost]
    public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto createCourseDto)
    {
        var result = await Mediator.Send(new CreateCourseCommand(createCourseDto));

        return HandleCreatedResult(result, nameof(GetCourseById), new { id = result.Value?.Id }, result.Value);
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost("create-lesson")]
    public async Task<ActionResult<LessonDto>> CreateLesson(CreateLessonDto createLessonDto)
    {
        return HandleResult(await Mediator.Send(new CreateLessonCommand(createLessonDto)));
    }
}
