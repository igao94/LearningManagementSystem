using Application.Students;
using Application.Students.Commands.DeleteStudent;
using Application.Students.Commands.UpdateStudent;
using Application.Students.DTOs;
using Application.Students.Queries.GetAllStudents;
using Application.Students.Queries.GetStudentById;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class StudentsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudents([FromQuery] StudentParams studentParams)
    {
        return HandleResult(await Mediator.Send(new GetAllStudentsQuery(studentParams)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDto>> GetStudentById(string id)
    {
        return HandleResult(await Mediator.Send(new GetStudentByIdQuery(id)));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteStudent()
    {
        return HandleResult(await Mediator.Send(new DeleteStudentCommand()));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateStudent(UpdateStudentDto updateStudentDto)
    {
        return HandleResult(await Mediator.Send(new UpdateStudentCommand(updateStudentDto)));
    }
}
