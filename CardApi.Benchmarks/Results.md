# Results of the http client benchmark
## Baseline - BenchmarkCardsAwaitedAsync
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
#### 10000 items:
#### 100,000 items:
#### 1,000,000 items:
#### 10,000,000 items:


