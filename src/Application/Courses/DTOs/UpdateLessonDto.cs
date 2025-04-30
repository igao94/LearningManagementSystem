using System.Text.Json.Serialization;

namespace Application.Courses.DTOs;

public class UpdateLessonDto
{
    [JsonIgnore]
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ContentUrl { get; set; } = string.Empty;
}
