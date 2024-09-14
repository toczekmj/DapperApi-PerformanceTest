using System.Diagnostics;

namespace CardApi.Benchmarks;

public class Program
{
    public static async Task Main()
    {
        Console.WriteLine("Press any key to begin benchmarking...");
        Console.ReadKey();

        long[] awaited = new long[5], 
            whenall = new long[5], 
            parallel = new long[5], 
            parallelasync = new long[5];

        Console.WriteLine("\nNaive benchmark for 1000 items");
        (awaited[0], whenall[0], parallel[0], parallelasync[0]) = await MyFastNaiveBenchmark(1000);

        Console.WriteLine("\nNaive benchmark for 10_000 items");
        (awaited[1], whenall[1], parallel[1], parallelasync[1]) = await MyFastNaiveBenchmark(10_000);
        
        Console.WriteLine("\nNaive benchmark for 100_000 items");
        (awaited[2], whenall[2], parallel[2], parallelasync[2]) = await MyFastNaiveBenchmark(100_000);

        Console.WriteLine("\nNaive benchmark for 1_000_000 items");
        (awaited[3], whenall[3], parallel[3], parallelasync[3]) = await MyFastNaiveBenchmark(1_000_000);

        Console.WriteLine("\nNaive benchmark for 10_000_000 items");
        (awaited[4], whenall[4], parallel[4], parallelasync[4]) = await MyFastNaiveBenchmark(10_000_000);
        
        Console.WriteLine("\nNaive benchmark results:");
        Console.WriteLine($"BenchmarkCardsAwaitedAsync: {awaited[0]}ms, {awaited[1]}ms, {awaited[2]}ms, {awaited[3]}ms, {awaited[4]}ms"); 
        Console.WriteLine($"BenchmarkCardsWhenAllAsync: {whenall[0]}ms, {whenall[1]}ms, {whenall[2]}ms, {whenall[3]}ms, {whenall[4]}ms");
        Console.WriteLine($"BenchmarkCardsParallel: {parallel[0]}ms, {parallel[1]}ms, {parallel[2]}ms, {parallel[3]}ms, {parallel[4]}ms");
        Console.WriteLine($"BenchmarkCardsParallelAsync: {parallelasync[0]}ms, {parallelasync[1]}ms, {parallelasync[2]}ms, {parallelasync[3]}ms, {parallelasync[4]}ms");

        Console.WriteLine("\nAverage results:");
        Console.WriteLine($"BenchmarkCardsAwaitedAsync: {awaited.Average()}ms");
        Console.WriteLine($"BenchmarkCardsWhenAllAsync: {whenall.Average()}ms");
        Console.WriteLine($"BenchmarkCardsParallel: {parallel.Average()}ms");
        Console.WriteLine($"BenchmarkCardsParallelAsync: {parallelasync.Average()}ms");
        
        
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    private static async Task<(long awaited, long whenall, long parallel, long parallelasync)> MyFastNaiveBenchmark(int amount = 1000, int tests = 10)
    {
        Console.WriteLine($"Benchmarking {amount} items");
        var client = new HttpClient();
        client.Timeout = TimeSpan.FromMinutes(60);
        var stopwatch = new Stopwatch();
        var baseUrl = "https://localhost:7038";
        var benchmarkCardsAwaitedAsync = baseUrl + "/Benchmark/BenchmarkCardsAwaitedAsync/" + amount;
        var benchmarkCardsWhenAllAsync = baseUrl + "/Benchmark/BenchmarkCardsWhenAllAsync/" + amount;
        var benchmarkCardsParallel = baseUrl + "/Benchmark/BenchmarkCardsParallel/" + amount;
        var benchmarkCardsParallelAsync = baseUrl + "/Benchmark/BenchmarkCardsParallelAsync/" + amount;
        HttpResponseMessage? response;
        long awaitedMean, whenallMean, parallelMean, parallelasyncMean;
        var awaitedTimes = new long[tests];
        var whenallTimes = new long[tests];
        var parallelTimes = new long[tests];
        var parallelasyncTimes = new long[tests];
        
        for (var i = 0; i < tests; i++)
        {
            Console.WriteLine($"Test {i + 1}");
            stopwatch.Start();
            await client.GetAsync(benchmarkCardsAwaitedAsync);
            stopwatch.Stop();
            Console.WriteLine($"BenchmarkCardsAwaitedAsync: {stopwatch.ElapsedMilliseconds}ms");
            awaitedTimes[i] = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
        
            stopwatch.Start();
            await client.GetAsync(benchmarkCardsWhenAllAsync);
            stopwatch.Stop();
            Console.WriteLine($"BenchmarkCardsWhenAllAsync: {stopwatch.ElapsedMilliseconds}ms");
            whenallTimes[i] = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
        
            stopwatch.Start();
            await client.GetAsync(benchmarkCardsParallel);
            stopwatch.Stop();
            Console.WriteLine($"BenchmarkCardsParallel: {stopwatch.ElapsedMilliseconds}ms");
            parallelTimes[i] = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
        
            stopwatch.Start();
            await client.GetAsync(benchmarkCardsParallelAsync);
            stopwatch.Stop();
            Console.WriteLine($"BenchmarkCardsParallelAsync: {stopwatch.ElapsedMilliseconds}ms");
            parallelasyncTimes[i] = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();    
        }
        
        awaitedMean = awaitedTimes.Sum() / tests;
        whenallMean = whenallTimes.Sum() / tests;
        parallelMean = parallelTimes.Sum() / tests;
        parallelasyncMean = parallelasyncTimes.Sum() / tests;
        
        Console.WriteLine($"BenchmarkCardsAwaitedAsync mean: {awaitedMean}ms");
        Console.WriteLine($"BenchmarkCardsWhenAllAsync mean: {whenallMean}ms");
        Console.WriteLine($"BenchmarkCardsParallel mean: {parallelMean}ms");
        Console.WriteLine($"BenchmarkCardsParallelAsync mean: {parallelasyncMean}ms");
        
        return (awaitedMean, whenallMean, parallelMean, parallelasyncMean);
    }
}
