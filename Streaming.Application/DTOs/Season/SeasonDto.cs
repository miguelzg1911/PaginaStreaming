using Streaming.Application.DTOs.Episode;

namespace Streaming.Application.DTOs.Season;

public class SeasonDto
{
    public int SeasonNumber { get; set; }
    public List<EpisodeDto> Episodes { get; set; } = new();
}