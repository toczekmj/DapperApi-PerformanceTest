using CardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DbController : ControllerBase
{
    private readonly IDbOperationRepository _dbOperationRepository;

    public DbController(IDbOperationRepository dbOperationRepository)
    {
        _dbOperationRepository = dbOperationRepository;
    }

    [HttpPost("rebuild/{projects}/{cardsPerProject}")]
    public IActionResult RebuildDb(int projects, int cardsPerProject)
    {
        _dbOperationRepository.RebuildDatabase(projects, cardsPerProject);
        return Ok();
    }
}