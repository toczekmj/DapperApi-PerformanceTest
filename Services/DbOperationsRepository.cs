using CardApi.DbConnections;
using Dapper;

namespace CardApi.Services;

public class DbOperationsRepository(SqliteDbConnectionFactory connectionFactory) : IDbOperationRepository
{
    private void DeleteData()
    {
        using var connection = connectionFactory.CreateConnection();
        const string query = """
                                DROP TABLE IF EXISTS Card;
                                DROP TABLE IF EXISTS Project;
                             """;
        connection.Execute(query);
    }

    private void CreateData(int projects, int cardsPerProject)
    {
        using var connection = connectionFactory.CreateConnection();
        const string createTableQuery = """
                                CREATE TABLE Project (
                                    Id TEXT PRIMARY KEY,
                                    Name TEXT NOT NULL
                                );
                                CREATE TABLE Card (
                                    id TEXT PRIMARY KEY,
                                    projectId TEXT NOT NULL,
                                    color TEXT NOT NULL,
                                    content TEXT NOT NULL,
                                    FOREIGN KEY(ProjectId) REFERENCES Project(Id)
                                );
                             """;
        const string insertProjectsQuery = "INSERT INTO Project (Id, Name) VALUES (@Id, @Name)";
        const string insertCardsQuery = "INSERT INTO Card (id, projectId, content, color) VALUES (@Id, @ProjectId, @Content, @Color)";
        
        connection.Execute(createTableQuery);
        var cardId = 1;
        
        for(var i = 1; i <= projects; i++)
        {
            connection.Execute(new CommandDefinition(insertProjectsQuery, new { Id = i, Name = $"Project {i}" }));  
            for(var j = 1; j <= cardsPerProject; j++)
            {
                var parameters = new
                {
                    Id = cardId++,
                    ProjectId = i, 
                    Content = $"Content {j}", 
                    Color = "Red"
                };
                connection.Execute(new CommandDefinition(insertCardsQuery, parameters: parameters));
            }
        }
    }

    public void RebuildDatabase(int projects, int cardsPerProject)
    {
        DeleteData();
        CreateData(projects, cardsPerProject);
    }
}

public interface IDbOperationRepository
{
    void RebuildDatabase(int projects, int cardsPerProject);
}