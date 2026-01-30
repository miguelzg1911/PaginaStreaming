namespace Streaming.Application.DTOs.MyList;

public class MyListDto
{
    public Guid ContentId { get; set; }
    public string ContentTitle { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public DateTime AddedAt { get; set; }
}