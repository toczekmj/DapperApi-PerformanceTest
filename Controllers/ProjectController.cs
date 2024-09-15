using CardApi.Services;
using Microsoft.AspNetCore.Mvc;
using Resultify.Enums;

namespace CardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectRepository _projectRepository;

    public ProjectController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetProjectByIdAsync(string projectId, CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await _projectRepository.GetProjectWithCardsByIdAsync(projectId, ct);
        return result.ResponseCategory is not ResponseCategory.Success
            ? StatusCode((int)result.StatusCode!, result.ErrorMessage)
            : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjectsWithCardsAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await _projectRepository.GetProjectsAsync(ct);
        return result.ResponseCategory is not ResponseCategory.Success
            ? StatusCode((int)result.StatusCode!, result.ErrorMessage)
            : Ok(result);
    }

    [HttpGet("WithCards")]
    public async Task<IActionResult> GetAllProjectsAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await _projectRepository.GetProjectsWithCardsAsync(ct);
        return result.ResponseCategory is not ResponseCategory.Success
            ? StatusCode((int)result.StatusCode!, result.ErrorMessage)
            : Ok(result);
    }
}