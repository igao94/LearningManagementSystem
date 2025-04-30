using System.Text.Json.Serialization;

namespace Application.Courses.DTOs;

public class UpdateCourseDto
{
    [JsonIgnore] 
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string InstructorName { get; set; } = string.Empty;
}
