using Microsoft.AspNetCore.Mvc;

namespace CardApi.Benchmarks;

[ApiController]
[Route("[controller]")]
public class BenchmarkController(IBenchmarkRepository benchmarkRepository) : ControllerBase
{
    [HttpGet("BenchmarkCardsAwaitedAsync/{amount}")]
    public async Task<IActionResult> BenchmarkCardsAwaitedAsync(int amount, CancellationToken ct)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await benchmarkRepository.BenchmarkCardsAwaitedAsync(amount, ct);
        return result.ResponseCategory is not Resultify.Enums.ResponseCategory.Success 
            ? StatusCode((int)result.StatusCode!, result.ErrorMessage) 
            : Ok();
    }
    
    [HttpGet("BenchmarkCardsWhenAllAsync/{amount}")]
    public async Task<IActionResult> BenchmarkCardsWhenAllAsync(int amount, CancellationToken ct)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await benchmarkRepository.BenchmarkCardsWhenAllAsync(amount, ct);
        return result.ResponseCategory is not Resultify.Enums.ResponseCategory.Success 
            ? StatusCode((int)result.StatusCode!, result.ErrorMessage) 
            : Ok();
    }
    
    [HttpGet("BenchmarkCardsParallel/{amount}")]
    public IActionResult BenchmarkCardsParallel(int amount, CancellationToken ct)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = benchmarkRepository.BenchmarkCardsParallel(amount, ct);
        return result.ResponseCategory is not Resultify.Enums.ResponseCategory.Success 
            ? StatusCode((int)result.StatusCode!, result.ErrorMessage) 
            : Ok();
    }
    
    [HttpGet("BenchmarkCardsParallelAsync/{amount}")]
    public async Task<IActionResult> BenchmarkCardsParallelAsync(int amount, CancellationToken ct)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await benchmarkRepository.BenchmarkCardsParallelAsync(amount, ct);
        return result.ResponseCategory is not Resultify.Enums.ResponseCategory.Success 
            ? StatusCode((int)result.StatusCode!, result.ErrorMessage) 
            : Ok();
    }
}