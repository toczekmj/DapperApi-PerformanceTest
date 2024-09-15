# DapperApi-PerformanceTest

- [Introduction](#introduction)
- [What is being measured?](#what-is-being-measured)
  - Baseline - simple query in loop
  - WhenAllAsync - query in loop with WhenAllAsync
  - Parallel - query in loop with Parallel.ForEach
  - ParallelAsync - query in loop with Parallel.ForEach and async
- [Worth noting](#worth-noting)
- [Results](#results)
  - [Single run results (how much faster compared to baseline)](#single-run-results-how-much-faster-compared-to-baseline)
  - [Overall Percentage Improvements](#overall-percentage-improvements)
  - [Naive Benchmark Results (1k, 2k, 5k, 7k, 10k, 20k, 50k, 70k, 100k, 200k, 500k, 700k, 1M, 2M, 3M, 5M, 7M, 8M, 10M, 15M, Average)](#naive-benchmark-results-1k-2k-5k-7k-10k-20k-50k-70k-100k-200k-500k-700k-1m-2m-3m-5m-7m-8m-10m-15m-average)
- [Summary of Results](#summary-of-results)
  - [Key Observations](#key-observations)
  - [Average Performance](#average-performance)
  - [Conclusion](#conclusion)

This repo is created in order to measure performance with different
approaches to querying independent things from database in loop.
It's created because of pure curiosity.

Project contains:

- SQLite database to simulate work with real DB
- Dapper as an ORM
- Some controllers and services to simulate being a real API
- Benchmark class and controllers which are being measured
- Poorly written timer based benchmark in order to measure execute time

Disclaimer: This is not a real world scenario, it's just a simple test to measure performance of different approaches.
System was tested on MacBook Air 2020 with M1 chip and 16GB of RAM.

### What is being measured?

#### How much faster are other approaches compared to baseline

- Baseline - simple query in loop
    - Every query is being executed and awaited in loop
    - This is the baseline for other tests
- WhenAllAsync - query in loop with WhenAllAsync
    - Every query is being added to list of tasks and awaited with WhenAllAsync
- Parallel - query in loop with Parallel.ForEach
    - Every query is being executed in Parallel.ForEach
- ParallelAsync - query in loop with Parallel.ForEach and async
    - Every query is being executed in Parallel.ForEach with async

### Worth noting

- I do set parallelism degree to Environment.ProcessorCount
- Parallel variants are using ConcurrentBag to store results
- Baseline and WhenAllAsync are using List to store tasks

## Results

### Single run results (how much faster compared to baseline)

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

## Naive Benchmark Results (1k, 2k, 5k, 7k, 10k, 20k, 50k, 70k, 100k, 200k, 500k, 700k, 1M, 2M, 3M, 5M, 7M, 8M, 10M, 15M, Average)

| Items    | AwaitedAsync (ms) | WhenAllAsync (ms) | Parallel (ms) | ParallelAsync (ms) | Speedup WhenAllAsync | Speedup Parallel | Speedup ParallelAsync |
|----------|-------------------|-------------------|---------------|--------------------|----------------------|------------------|-----------------------|
| 1000     | 36                | 18                | 6             | 6                  | 2.00x                | 6.00x            | 6.00x                 |
| 2000     | 35                | 32                | 10            | 11                 | 1.09x                | 3.50x            | 3.18x                 |
| 5000     | 74                | 69                | 24            | 25                 | 1.07x                | 3.08x            | 2.96x                 |
| 7000     | 80                | 74                | 25            | 27                 | 1.08x                | 3.20x            | 2.96x                 |
| 10000    | 113               | 105               | 34            | 39                 | 1.08x                | 3.32x            | 2.90x                 |
| 20000    | 213               | 212               | 102           | 70                 | 1.00x                | 2.09x            | 3.04x                 |
| 50000    | 574               | 576               | 183           | 175                | 1.00x                | 3.14x            | 3.28x                 |
| 70000    | 759               | 776               | 255           | 249                | 0.98x                | 2.98x            | 3.05x                 |
| 100000   | 1050              | 1098              | 430           | 375                | 0.96x                | 2.44x            | 2.80x                 |
| 200000   | 2261              | 2193              | 810           | 851                | 1.03x                | 2.79x            | 2.66x                 |
| 500000   | 5405              | 5537              | 2047          | 2070               | 0.98x                | 2.64x            | 2.61x                 |
| 700000   | 7644              | 7790              | 2888          | 2951               | 0.98x                | 2.65x            | 2.59x                 |
| 1000000  | 11081             | 11157             | 4249          | 4309               | 0.99x                | 2.61x            | 2.57x                 |
| 2000000  | 24283             | 24026             | 10538         | 10360              | 1.01x                | 2.30x            | 2.34x                 |
| 3000000  | 34157             | 33944             | 13312         | 13484              | 1.01x                | 2.57x            | 2.53x                 |
| 5000000  | 56654             | 56695             | 22102         | 23006              | 1.00x                | 2.56x            | 2.46x                 |
| 7000000  | 80681             | 81327             | 32275         | 33121              | 0.99x                | 2.50x            | 2.44x                 |
| 8000000  | 96592             | 96553             | 40016         | 40109              | 1.00x                | 2.41x            | 2.41x                 |
| 10000000 | 122523            | 123147            | 50607         | 51118              | 0.99x                | 2.42x            | 2.40x                 |
| 15000000 | 184176            | 184381            | 81421         | 81576              | 1.00x                | 2.26x            | 2.26x                 |
| Average  | 31419             | 31485             | 13066         | 13196              | 1.00x                | 2.40x            | 2.38x                 |

![execution_times_log_scale](https://github.com/user-attachments/assets/72b11214-ba28-4b30-b4a0-17b9bbc2949a)

![speedup_log_scale](https://github.com/user-attachments/assets/4d8a1c69-95ad-4338-952b-86761194158f)

## Summary of Results

The table compares the execution times (in milliseconds) for different asynchronous approaches—**AwaitedAsync**, *
*WhenAllAsync**, **Parallel**, and **ParallelAsync**—across a range of item counts (from 1,000 to 15,000,000). It also
includes the speedup for **WhenAllAsync**, **Parallel**, and **ParallelAsync** compared to **AwaitedAsync**.

### Key Observations

1. **Overall Execution Times**:
    - **AwaitedAsync** has the highest execution time across all item counts, with times growing significantly as the
      item count increases. For example, processing 10,000,000 items takes **122,523 ms**.
    - **WhenAllAsync** follows closely behind AwaitedAsync, with almost similar times, and rarely exceeds it by more
      than a small margin (e.g., 0.99x at 10,000,000 items).
    - **Parallel** consistently shows a much lower execution time compared to AwaitedAsync and WhenAllAsync. For
      instance, at 1,000 items, it takes just **6 ms** vs. **36 ms** (AwaitedAsync).
    - **ParallelAsync** is close to the performance of Parallel, with very similar execution times.

2. **Speedup Trends**:
    - **WhenAllAsync** offers a minor speedup, typically between **0.98x and 2.00x**, but as the item count increases,
      the speedup reduces (dropping to around **1.00x** on average).
    - **Parallel** consistently shows the best speedup, with values ranging from **6.00x** at lower item counts (1,000)
      to around **2.50x** at higher counts (7,000,000+).
    - **ParallelAsync** also provides significant speedup, similar to Parallel. For instance, with 1,000 items, it
      achieves a **6.00x** speedup, but this diminishes to **2.40x** at 10,000,000 items.

3. **Scalability**:
    - For smaller datasets (e.g., 1,000–10,000 items), **Parallel** and **ParallelAsync** provide much higher
      performance gains compared to AwaitedAsync.
    - For larger datasets (1,000,000+ items), the speedup becomes more moderate across all methods. The performance gap
      between Parallel and AwaitedAsync narrows with the item count increase, though Parallel and ParallelAsync still
      offer better scalability.

### Average Performance

- **Mean execution times** across all item counts:
    - **AwaitedAsync**: 31,419 ms
    - **WhenAllAsync**: 31,485 ms
    - **Parallel**: 13,066 ms
    - **ParallelAsync**: 13,196 ms

- **Average speedup**:
    - **WhenAllAsync**: 1.00x
    - **Parallel**: 2.40x
    - **ParallelAsync**: 2.38x

### Conclusion

In summary, **Parallel** and **ParallelAsync** provide substantial speedups, especially for smaller to mid-range
datasets, while **WhenAllAsync** offers only marginal improvements over AwaitedAsync. As the dataset size increases, the
speedup advantage of Parallel approaches diminishes but still remains significant compared to AwaitedAsync.

Are those results trustworthy? Maybe, or maybe not. But I'd take them into consideration while working with real data.
Also keep in mind that:

- MacBook Air M1 doesn't have active cooling, and OS might have dropped the CPU clock leading to worse performance
  during the end of testing phase
- M1 is an ARM CPU which has only 8 logical cores and no multi threading
- I am not certainly sure how MacOs handles concurrency and parallelism compared to other x64 Linux/Windows PCs
