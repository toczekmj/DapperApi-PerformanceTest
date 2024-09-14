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
#### 1000 items:
- WhenAllAsync: 86.67%
- Parallel: 92.78%
- ParallelAsync: 90.56%

#### 10000 items:
- WhenAllAsync: 11.63%
- Parallel: 77.21%
- ParallelAsync: 79.07%

#### 100,000 items:
- WhenAllAsync: 7.09%
- Parallel: 73.04%
- ParallelAsync: 62.98%

#### 1,000,000 items:
- WhenAllAsync: -6.16% (slower than baseline)
- Parallel: 62.33%
- ParallelAsync: 66.41%

#### 10,000,000 items:
- WhenAllAsync: -28.77% (slower than baseline)
- Parallel: 45.91%
- ParallelAsync: 38.01%

#### Overall Percentage Improvements (for all sets combined):
- WhenAllAsync: -26.32% (slower than baseline)
- Parallel: 47.65%
- ParallelAsync: 40.76%

### Average of 10 cycles per test:

