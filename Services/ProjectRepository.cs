using System.Net;
using CardApi.DbConnections;
using CardApi.Models;
using Dapper;
using Resultify.Handlers;
using static Resultify.Resultify;
namespace CardApi.Services;

public class ProjectRepository(SqliteDbConnectionFactory connectionFactory) : IProjectRepository
{
    public async Task<ResultifyHandler<IReadOnlyCollection<ProjectModel>>> GetProjectsAsync(CancellationToken ct)
    {
        using var connection = connectionFactory.CreateConnection();
        const string query = "SELECT * FROM Project";

        var queryAsync = await connection.QueryAsync<ProjectModel>(
            new CommandDefinition(query, cancellationToken: ct));

        var result = queryAsync.ToList();
        return result.Count != 0
            ? Success<IReadOnlyCollection<ProjectModel>>(result, HttpStatusCode.OK)
            : GenericError<IReadOnlyCollection<ProjectModel>>("No projects found", HttpStatusCode.NotFound);
    }
    
    public async Task<ResultifyHandler<IReadOnlyCollection<ProjectModelWithCards>>> GetProjectWithCardsByIdAsync(string id, CancellationToken ct)
    {
        using var connection = connectionFactory.CreateConnection();
        const string projectQuery = "SELECT * FROM Project WHERE id = @Id";
        const string cardQuery = "SELECT * FROM Card WHERE ProjectId = @ProjectId";
        
        var projects = await connection.QueryAsync<ProjectModel>(
            new CommandDefinition(projectQuery, new { Id = id }, cancellationToken: ct));
        var cards = await connection.QueryAsync<CardModel>(
            new CommandDefinition(cardQuery, new { ProjectId = id }, cancellationToken: ct));

        var result = projects.Select(project => new ProjectModelWithCards
        {
            Id = project.Id,
            Name = project.Name,
            Cards = cards,
        }).ToList();
        
        return result.Count != 0
            ? Success<IReadOnlyCollection<ProjectModelWithCards>>(result, HttpStatusCode.OK)
            : GenericError<IReadOnlyCollection<ProjectModelWithCards>>("No projects found", HttpStatusCode.NotFound);
    }

    public async Task<ResultifyHandler<IReadOnlyCollection<ProjectModelWithCards>>> GetProjectsWithCardsAsync(CancellationToken ct)
    {
        using var connection = connectionFactory.CreateConnection();
        const string projectQuery = "SELECT * FROM Project";
        const string cardQuery = "SELECT * FROM Card";
        
        var projects = connection.QueryAsync<ProjectModel>(
            new CommandDefinition(projectQuery, cancellationToken: ct));
        var cards = connection.QueryAsync<CardModel>(
            new CommandDefinition(cardQuery, cancellationToken: ct));

        await Task.WhenAll(projects, cards);

        var result = projects.Result.Select(project => new ProjectModelWithCards
        {
            Id = project.Id,
            Name = project.Name,
            Cards = cards.Result.Where(c => c.ProjectId == project.Id),
        }).ToList();
        
        return result.Count != 0
            ? Success<IReadOnlyCollection<ProjectModelWithCards>>(result, HttpStatusCode.OK)
            : GenericError<IReadOnlyCollection<ProjectModelWithCards>>("No projects found", HttpStatusCode.NotFound);

    }
}

public interface IProjectRepository
{
    Task<ResultifyHandler<IReadOnlyCollection<ProjectModel>>> GetProjectsAsync(CancellationToken ct);
    Task<ResultifyHandler<IReadOnlyCollection<ProjectModelWithCards>>> GetProjectWithCardsByIdAsync(string id, CancellationToken ct);
    Task<ResultifyHandler<IReadOnlyCollection<ProjectModelWithCards>>> GetProjectsWithCardsAsync(CancellationToken ct);
}