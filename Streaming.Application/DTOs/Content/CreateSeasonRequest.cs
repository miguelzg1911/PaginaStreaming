namespace Streaming.Application.DTOs.Content;

public class CreateSeasonRequest
{
    public Guid ContentId { get; set; }
    public int SeasonNumber { get; set; }
}