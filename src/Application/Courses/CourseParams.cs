using Application.Core;

namespace Application.Courses;

public class CourseParams : PaginationParams<DateTime?>
{
    private string? _search;

    public string Search
    {
        get => _search ?? string.Empty;

        set => _search = value?.ToLower();
    }

    public string? Filter { get; set; }
}
