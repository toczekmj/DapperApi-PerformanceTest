using System.Collections.Concurrent;
using System.Net;
using CardApi.Models;
using CardApi.Services;
using Resultify.Handlers;
using static Resultify.Resultify;
namespace CardApi.Benchmarks;

public class BenchmarkRepository(ICardRepository cardRepository) : IBenchmarkRepository
{
    public async Task<ResultifyHandler<IReadOnlyCollection<CardModel>>> BenchmarkCardsAwaitedAsync(int amount, CancellationToken ct)
    {
        List<CardModel> cards = [];
        for (var i = 0; i < amount; i++)
        {
            var card = await cardRepository.GetCardAsync((i % 30).ToString(), ct);
            if (!AnyFail(card))
                cards.Add(card.Value!);
        }

        return Success<IReadOnlyCollection<CardModel>>(cards, HttpStatusCode.OK);
    }

    public async Task<ResultifyHandler<IReadOnlyCollection<CardModel>>> BenchmarkCardsWhenAllAsync(int amount, CancellationToken ct)
    {
        List<CardModel> cards = [];
        var tasks = new List<Task<ResultifyHandler<CardModel>>>();
        for (var i = 0; i < amount; i++)
        {
            var card = cardRepository.GetCardAsync((i % 30).ToString(), ct);
            tasks.Add(card);
        }

        await Task.WhenAll(tasks);
        
        cards.AddRange(tasks.Select(x => x.Result.Value!));
        return Success<IReadOnlyCollection<CardModel>>(cards, HttpStatusCode.OK);
    }
    
    public ResultifyHandler<IReadOnlyCollection<CardModel>> BenchmarkCardsParallel(int amount, CancellationToken ct)
    {
        ConcurrentBag<CardModel> cards = [];
        
        ParallelOptions options = new() { MaxDegreeOfParallelism = Environment.ProcessorCount, CancellationToken = ct };

        Parallel.For(0, amount, options, Body);

        return Success<IReadOnlyCollection<CardModel>>(cards, HttpStatusCode.OK);

        async void Body(int i)
        {
            var card = await cardRepository.GetCardAsync((i % 30).ToString(), ct);
            if (!AnyFail(card)) cards.Add(card.Value!);
        }
    }


    public async Task<ResultifyHandler<IReadOnlyCollection<CardModel>>> BenchmarkCardsParallelAsync(int amount, CancellationToken ct)
    {
        ConcurrentBag<CardModel> cards = [];

        ParallelOptions options = new() { MaxDegreeOfParallelism = Environment.ProcessorCount, CancellationToken = ct };

        await Parallel.ForAsync(0, amount, options, async (i, token) =>
        {
            var card = await cardRepository.GetCardAsync((i % 30).ToString(), token);
            if (!AnyFail(card)) cards.Add(card.Value!);
        });

        return Success<IReadOnlyCollection<CardModel>>(cards);
    }
}


public interface IBenchmarkRepository
{
    ResultifyHandler<IReadOnlyCollection<CardModel>> BenchmarkCardsParallel(int amount, CancellationToken ct);
    Task<ResultifyHandler<IReadOnlyCollection<CardModel>>> BenchmarkCardsAwaitedAsync(int amount, CancellationToken ct);
    Task<ResultifyHandler<IReadOnlyCollection<CardModel>>> BenchmarkCardsWhenAllAsync(int amount, CancellationToken ct);
    Task<ResultifyHandler<IReadOnlyCollection<CardModel>>> BenchmarkCardsParallelAsync(int amount, CancellationToken ct);
}