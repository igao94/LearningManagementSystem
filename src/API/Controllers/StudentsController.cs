using Application.Students;
using Application.Students.DTOs;
using Application.Students.Queries.GetAllStudents;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class StudentsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudents([FromQuery] StudentParams studentParams)
    {
        return HandleResult(await Mediator.Send(new GetAllStudentsQuery(studentParams)));
    }
}
