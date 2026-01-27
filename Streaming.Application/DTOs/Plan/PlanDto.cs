namespace Streaming.Application.DTOs.Plan;

public class PlanDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string MaxResolution { get; set; } = string.Empty;
}