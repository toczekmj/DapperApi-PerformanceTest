namespace CardApi.Models;

public class CardModel
{
    public required string Id { get; set; }
    public required string Content { get; set; }
    public required string Color { get; set; }
    public required string ProjectId { get; set; }
}