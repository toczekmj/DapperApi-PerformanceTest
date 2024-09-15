using System.Diagnostics;

namespace CardApi.Benchmarks;

public class Program
{
    private static int port = 5001;

    public static async Task Main()
    {
        Console.WriteLine("Enter the port number of the API:");
        port = int.Parse(Console.ReadLine());
        Console.WriteLine("Press any key to begin benchmarking...");
        Console.ReadKey();

        var tests = new[]
        {
            1000, 2000, 5000, 7000, 10_000, 20_000, 50_000, 70_000, 100_000, 200_000, 500_000, 700_000, 1_000_000,
            1_500_0000, 2_000_000, 3_000_000, 5_000_000, 7_000_000, 8_000_000, 10_000_000
        };
        var amountOfTests = tests.Length;
        long[] awaited = new long[amountOfTests],
            whenAll = new long[amountOfTests],
            parallel = new long[amountOfTests],
            parallelAsync = new long[amountOfTests];

        for (var i = 0; i < amountOfTests; i++)
        {
            Console.WriteLine($"\nNaive benchmark for {tests[i]} items");
            (awaited[i], whenAll[i], parallel[i], parallelAsync[i]) = await MyFastNaiveBenchmark(tests[i]);
        }

        Console.WriteLine("\nNaive benchmark results:");
        Console.WriteLine(
            $"BenchmarkCardsAwaitedAsync: {awaited[0]}ms, {awaited[1]}ms, {awaited[2]}ms, {awaited[3]}ms, {awaited[4]}ms");
        Console.WriteLine(
            $"BenchmarkCardsWhenAllAsync: {whenAll[0]}ms, {whenAll[1]}ms, {whenAll[2]}ms, {whenAll[3]}ms, {whenAll[4]}ms");
        Console.WriteLine(
            $"BenchmarkCardsParallel: {parallel[0]}ms, {parallel[1]}ms, {parallel[2]}ms, {parallel[3]}ms, {parallel[4]}ms");
        Console.WriteLine(
            $"BenchmarkCardsParallelAsync: {parallelAsync[0]}ms, {parallelAsync[1]}ms, {parallelAsync[2]}ms, {parallelAsync[3]}ms, {parallelAsync[4]}ms");

        Console.WriteLine("\nAverage results:");
        Console.WriteLine($"BenchmarkCardsAwaitedAsync: {awaited.Average()}ms");
        Console.WriteLine($"BenchmarkCardsWhenAllAsync: {whenAll.Average()}ms");
        Console.WriteLine($"BenchmarkCardsParallel: {parallel.Average()}ms");
        Console.WriteLine($"BenchmarkCardsParallelAsync: {parallelAsync.Average()}ms");


        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    private static async Task<(long awaited, long whenall, long parallel, long parallelasync)> MyFastNaiveBenchmark(
        int amount = 1000, int tests = 10)
    {
        Console.WriteLine($"Benchmarking {amount} items");
        var client = new HttpClient();
        client.Timeout = TimeSpan.FromMinutes(60);
        var stopwatch = new Stopwatch();
        var baseUrl = "http://localhost:" + port;
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