# DapperApi-PerformanceTest
This repo is created in order to measure performance with different 
approaches to querying independent things from database in loop.
It's created because of pure curiosity.

Project contains: 
-  SQLite database to simulate work with real DB
-  Dapper as an ORM
-  Some controllers and services to simulate being a real API
-  Benchmark class and controllers which are being measured
-  Poorly written timer based benchmark in order to measure execute time

Disclaimer: This is not a real world scenario, it's just a simple test to measure performance of different approaches.
System was tested on MacBook Air 2020 with M1 chip and 16GB of RAM.

### What is being measured? 
#### How much faster are other approaches compared to baseline
-  Baseline - simple query in loop
    - Every query is being executed and awaited in loop
    - This is the baseline for other tests
-  WhenAllAsync - query in loop with WhenAllAsync
    - Every query is being added to list of tasks and awaited with WhenAllAsync
- Parallel - query in loop with Parallel.ForEach
    - Every query is being executed in Parallel.ForEach
- ParallelAsync - query in loop with Parallel.ForEach and async
    - Every query is being executed in Parallel.ForEach with async

### Worth noting
- I don't set any parallelization options in Parallel.ForEach
- Parallel variants are using ConcurrentBag to store results
- WhenAllAsync is using List to store tasks

## Results
### Single run results:

| Items          | WhenAllAsync     | Parallel | ParallelAsync |
|----------------|------------------|----------|---------------|
| **1000**       | 86.67%           | 92.78%   | 90.56%        |
| **10,000**     | 11.63%           | 77.21%   | 79.07%        |
| **100,000**    | 7.09%            | 73.04%   | 62.98%        |
| **1,000,000**  | -6.16% (slower)  | 62.33%   | 66.41%        |
| **10,000,000** | -28.77% (slower) | 45.91%   | 38.01%        |

## Overall Percentage Improvements
  
| Test Case         | Improvement      |
|-------------------|------------------|
| **WhenAllAsync**  | -26.32% (slower) |
| **Parallel**      | 47.65%           |
| **ParallelAsync** | 40.76%           |

### Average of 10 cycles per test:
## Naive Benchmark Results (1k, 10k, 100k, 1M, 10M, Average)

| Test Case                       | 1k   | 10k   | 100k   | 1M      | 10M      | Average   |
|---------------------------------|------|-------|--------|---------|----------|-----------|
| **BenchmarkCardsAwaitedAsync**  | 22ms | 112ms | 1250ms | 16435ms | 149898ms | 33543.4ms |
| **BenchmarkCardsWhenAllAsync**  | 48ms | 110ms | 1252ms | 17508ms | 148991ms | 33581.8ms |
| **BenchmarkCardsParallel**      | 8ms  | 56ms  | 668ms  | 9373ms  | 68367ms  | 15694.4ms |
| **BenchmarkCardsParallelAsync** | 4ms  | 39ms  | 480ms  | 8511ms  | 71174ms  | 16041.6ms |


#### Average results:
- BenchmarkCardsAwaitedAsync: 33543,4ms
- BenchmarkCardsWhenAllAsync: 33581,8ms
- BenchmarkCardsParallel: 15694,4ms
- BenchmarkCardsParallelAsync: 16041,6ms

![benchmark_comparison](https://github.com/user-attachments/assets/785e0a63-1f0f-47ab-93c5-b6e19e66a01e)


## Conclusion
| Benchmark                   | Performance Difference from Baseline |
|-----------------------------|--------------------------------------|
| BenchmarkCardsWhenAllAsync  | 0.11% slower                         |
| BenchmarkCardsParallel      | 53.21% faster                        |
| BenchmarkCardsParallelAsync | 52.18% faster                        |


- BenchmarkCardsWhenAllAsync is 0.11% slower than the baseline (BenchmarkCardsAwaitedAsync).
- BenchmarkCardsParallel is 53.21% faster than the baseline.
- BenchmarkCardsParallelAsync is 52.18% faster than the baseline



Are those results trustworthy? Maybe, or maybe not. But I'd take them into consideration while working with real data.

