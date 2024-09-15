using System.Net;
using CardApi.DbConnections;
using CardApi.Models;
using Dapper;
using Resultify.Handlers;
using static Resultify.Resultify;

namespace CardApi.Services;

public class CardRepository(
    SqliteDbConnectionFactory connectionFactory
) : ICardRepository
{
    public async Task<ResultifyHandler<CardModel>> GetCardAsync(string id, CancellationToken ct)
    {
        using var connection = connectionFactory.CreateConnection();
        const string query = "SELECT * FROM Card WHERE Id = @Id";
        var parameters = new { Id = id };

        var card = await connection.QuerySingleOrDefaultAsync<CardModel>(
            new CommandDefinition(query, parameters, cancellationToken: ct));

        return card is not null
            ? Success(card, HttpStatusCode.OK)
            : GenericError<CardModel>("Card not found", HttpStatusCode.NotFound);
    }

    public async Task<ResultifyHandler<IReadOnlyCollection<CardModel>>> GetCardsByProjectIdAsync(string projectId,
        CancellationToken ct)
    {
        var connection = connectionFactory.CreateConnection();
        const string query = "SELECT * FROM Card WHERE ProjectId = @ProjectId";
        var parameters = new { ProjectId = projectId };

        var queryAsync = await connection.QueryAsync<CardModel>(
            new CommandDefinition(query, parameters, cancellationToken: ct));

        var result = queryAsync.ToList();
        return result.Count != 0
            ? Success<IReadOnlyCollection<CardModel>>(result, HttpStatusCode.OK)
            : GenericError<IReadOnlyCollection<CardModel>>("No cards found", HttpStatusCode.NotFound);
    }

    public async Task<ResultifyHandler<IReadOnlyCollection<CardModel>>> GetCardsAsync(CancellationToken ct)
    {
        using var connection = connectionFactory.CreateConnection();
        const string query = "SELECT * FROM Card";

        var queryAsync = await connection.QueryAsync<CardModel>(
            new CommandDefinition(query, cancellationToken: ct));

        var result = queryAsync.ToList();
        return result.Count != 0
            ? Success<IReadOnlyCollection<CardModel>>(result, HttpStatusCode.OK)
            : GenericError<IReadOnlyCollection<CardModel>>("No cards found", HttpStatusCode.NotFound);
    }
}

public interface ICardRepository
{
    Task<ResultifyHandler<IReadOnlyCollection<CardModel>>> GetCardsAsync(CancellationToken ct);
    Task<ResultifyHandler<CardModel>> GetCardAsync(string id, CancellationToken ct);

    Task<ResultifyHandler<IReadOnlyCollection<CardModel>>> GetCardsByProjectIdAsync(string projectId,
        CancellationToken ct);
}