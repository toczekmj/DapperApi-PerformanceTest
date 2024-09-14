using CardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CardController : ControllerBase
{
    private readonly ICardRepository _cardRepository;
    public CardController(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }
    
    [HttpGet("{cardId}")]
    public async Task<IActionResult> GetCardByIdAsync(string cardId, CancellationToken ct)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _cardRepository.GetCardAsync(cardId, ct);
        return result.ResponseCategory is not Resultify.Enums.ResponseCategory.Success 
            ? StatusCode((int)result.StatusCode!, result.ErrorMessage) 
            : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCardsAsync(CancellationToken ct)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _cardRepository.GetCardsAsync(ct);
        return result.ResponseCategory is not Resultify.Enums.ResponseCategory.Success 
            ? StatusCode((int)result.StatusCode!, result.ErrorMessage) 
            : Ok(result);
    }
    
    [HttpGet("from-project/{projectId}")]
    public async Task<IActionResult> GetCardsByProjectIdAsync(string projectId, CancellationToken ct)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _cardRepository.GetCardsByProjectIdAsync(projectId, ct);
        return result.ResponseCategory is not Resultify.Enums.ResponseCategory.Success 
            ? StatusCode((int)result.StatusCode!, result.ErrorMessage) 
            : Ok(result);
    }
}