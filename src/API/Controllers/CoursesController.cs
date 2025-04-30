using Application.Courses;
using Application.Courses.Commands.CreateCourse;
using Application.Courses.Commands.CreateLesson;
using Application.Courses.Commands.DeleteCourse;
using Application.Courses.Commands.DeleteLesson;
using Application.Courses.Commands.UpdateCourse;
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

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(string id, UpdateCourseDto updateCourseDto)
    {
        updateCourseDto.Id = id;

        return HandleResult(await Mediator.Send(new UpdateCourseCommand(updateCourseDto)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteCouresCommand(id)));
    }    
    
    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpDelete("delete-lesson/{id}")]
    public async Task<ActionResult> DeleteLesson(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteLessonCommand(id)));
    }
}
