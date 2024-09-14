namespace CardApi.Models;

public class ProjectModel
{
    public required string Id { get; set; }
    public required string Name { get; set; }
}

public class ProjectModelWithCards
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public IEnumerable<CardModel>? Cards { get; set; }
}