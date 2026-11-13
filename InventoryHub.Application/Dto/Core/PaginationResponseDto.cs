public class PaginationResponseDto<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int Page { get; set; }
    public int Total { get; set; }
}
