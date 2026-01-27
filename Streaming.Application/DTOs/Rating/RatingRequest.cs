namespace Streaming.Application.DTOs.Rating;

public class RatingRequest
{
    public Guid ContentId { get; set; }
    public string Value { get; set; } = string.Empty; // "Like" o "Dislike"
}