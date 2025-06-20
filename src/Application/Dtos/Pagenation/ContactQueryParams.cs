namespace Application.Dtos.Pagenation;

public class ContactQueryParams
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Search { get; set; }
    public long? UserId { get; set; }
}