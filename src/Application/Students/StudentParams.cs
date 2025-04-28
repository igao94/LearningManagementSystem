namespace Application.Students;

public class StudentParams
{
    private string? _search;

    public string Search
    {
        get => _search ?? string.Empty;

        set => _search = value?.ToLower();
    }
}
