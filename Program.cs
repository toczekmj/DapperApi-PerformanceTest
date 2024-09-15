using CardApi.Benchmarks;
using CardApi.DbConnections;
using CardApi.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("localDB");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddSingleton(new SqliteDbConnectionFactory(connectionString));
builder.Services.AddTransient<ICardRepository, CardRepository>();
builder.Services.AddTransient<IDbOperationRepository, DbOperationsRepository>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<IBenchmarkRepository, BenchmarkRepository>();

var app = builder.Build();
app.MapOpenApi();
app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "API benchmark v1"); });


app.UseHttpsRedirection();
app.MapControllers();
app.Run();