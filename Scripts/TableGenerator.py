import numpy as np
import pandas as pd

# Data extracted from the provided file
data = {
    'items':                       [1000, 2000, 5000, 7000, 10000, 20000, 50000, 70000, 100000, 200000, 500000, 700000, 1000000, 2000000, 3000000, 5000000, 7000000, 8000000, 10000000,  15000000, 'mean'],
    'BenchmarkCardsAwaitedAsync':  [36,   35,   74,   80,   113,   213,   574,   759,   1050,   2261,   5405,   7644,   11081,   24283,   34157,   56654,   80681,   96592,   122523,    184176,   31419 ],
    'BenchmarkCardsWhenAllAsync':  [18,   32,   69,   74,   105,   212,   576,   776,   1098,   2193,   5537,   7790,   11157,   24026,   33944,   56695,   81327,   96553,   123147,    184381,   31485 ],
    'BenchmarkCardsParallel':      [6,    10,   24,   25,   34,    102,   183,   255,   430,    810,    2047,   2888,   4249,    10538,   13312,   22102,   32275,   40016,   50607,     81421,    13066 ],
    'BenchmarkCardsParallelAsync': [6,    11,   25,   27,   39,    70,    175,   249,   375,    851,    2070,   2951,   4309,    10360,   13484,   23006,   33121,   40109,   51118,     81576,    13196 ],
}

# Convert to DataFrame
df = pd.DataFrame(data)

# Compute speedups relative to BenchmarkCardsAwaitedAsync (Baseline)
df['WhenAllAsync_speedup'] = df['BenchmarkCardsAwaitedAsync'] / df['BenchmarkCardsWhenAllAsync']
df['Parallel_speedup'] = df['BenchmarkCardsAwaitedAsync'] / df['BenchmarkCardsParallel']
df['ParallelAsync_speedup'] = df['BenchmarkCardsAwaitedAsync'] / df['BenchmarkCardsParallelAsync']

# Formatting the results for README.md
md_output = "# Benchmark Comparison\n\n"
md_output += "Comparing execution times against `BenchmarkCardsAwaitedAsync` as the baseline.\n\n"
md_output += "| Items | AwaitedAsync (ms) | WhenAllAsync (ms) | Parallel (ms) | ParallelAsync (ms) | Speedup WhenAllAsync | Speedup Parallel | Speedup ParallelAsync |\n"
md_output += "|-------|------------------|------------------|---------------|-------------------|----------------------|------------------|-----------------------|\n"

# Add rows
for i, row in df.iterrows():
    md_output += f"| {row['items']} | {row['BenchmarkCardsAwaitedAsync']} | {row['BenchmarkCardsWhenAllAsync']} | {row['BenchmarkCardsParallel']} | {row['BenchmarkCardsParallelAsync']} | {row['WhenAllAsync_speedup']:.2f}x | {row['Parallel_speedup']:.2f}x | {row['ParallelAsync_speedup']:.2f}x |\n"

# Display the result for README.md
print(md_output)

