using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class PaginationRequestDto<TSortBy>
{
    [Range(1, int.MaxValue, ErrorMessage = "Page must be at least 1.")]
    [DefaultValue(1)]
    public int Page { get; set; } = 1;

    [Range(1, 100, ErrorMessage = "ItemsOnPage must be between 1 and 100.")]
    [DefaultValue(10)]
    public int ItemsOnPage { get; set; } = 10;

    public TSortBy? SortBy { get; set; }

    [DefaultValue(ListSortDirection.Ascending)]
    public ListSortDirection Direction { get; set; }
}
