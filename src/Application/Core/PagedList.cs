namespace Application.Core;

public class PagedList<T, TCursor>
{
    public IEnumerable<T> Items { get; set; } = [];
    public TCursor? NextCursor { get; set; }
}
