import matplotlib.pyplot as plt

# Data from the table
items = [1000, 2000, 5000, 7000, 10000, 20000, 50000, 70000, 100000, 200000, 
         500000, 700000, 1000000, 2000000, 3000000, 5000000, 7000000, 8000000, 
         10000000, 15000000]

awaited_async = [36, 35, 74, 80, 113, 213, 574, 759, 1050, 2261, 5405, 7644, 
                 11081, 24283, 34157, 56654, 80681, 96592, 122523, 184176]
whenall_async = [18, 32, 69, 74, 105, 212, 576, 776, 1098, 2193, 5537, 7790, 
                 11157, 24026, 33944, 56695, 81327, 96553, 123147, 184381]
parallel = [6, 10, 24, 25, 34, 102, 183, 255, 430, 810, 2047, 2888, 4249, 
            10538, 13312, 22102, 32275, 40016, 50607, 81421]
parallel_async = [6, 11, 25, 27, 39, 70, 175, 249, 375, 851, 2070, 2951, 4309, 
                  10360, 13484, 23006, 33121, 40109, 51118, 81576]

# Plotting Execution Times with log scale
plt.figure(figsize=(10, 6))
plt.plot(items, awaited_async, label='AwaitedAsync', marker='o')
plt.plot(items, whenall_async, label='WhenAllAsync', marker='o')
plt.plot(items, parallel, label='Parallel', marker='o')
plt.plot(items, parallel_async, label='ParallelAsync', marker='o')

# Chart formatting
plt.title('Execution Times for Different Asynchronous Methods')
plt.xlabel('Items')
plt.ylabel('Time (ms)')
plt.xscale('log')  # Logarithmic scale for better visualization of trends
plt.yscale('log')  # Logarithmic y-axis
plt.grid(True, which="both", ls="--")
plt.legend()
plt.tight_layout()

# Save the execution times plot
plt.savefig('execution_times_log_scale.png', dpi=300)
plt.show()

# Speedups from the table
speedup_whenall_async = [2.00, 1.09, 1.07, 1.08, 1.08, 1.00, 1.00, 0.98, 0.96, 1.03, 
                         0.98, 0.98, 0.99, 1.01, 1.01, 1.00, 0.99, 1.00, 0.99, 1.00]
speedup_parallel = [6.00, 3.50, 3.08, 3.20, 3.32, 2.09, 3.14, 2.98, 2.44, 2.79, 
                    2.64, 2.65, 2.61, 2.30, 2.57, 2.56, 2.50, 2.41, 2.42, 2.26]
speedup_parallel_async = [6.00, 3.18, 2.96, 2.96, 2.90, 3.04, 3.28, 3.05, 2.80, 2.66, 
                          2.61, 2.59, 2.57, 2.34, 2.53, 2.46, 2.44, 2.41, 2.40, 2.26]

# Plotting Speedups with log scale
plt.figure(figsize=(10, 6))
plt.plot(items, speedup_whenall_async, label='Speedup WhenAllAsync', marker='o')
plt.plot(items, speedup_parallel, label='Speedup Parallel', marker='o')
plt.plot(items, speedup_parallel_async, label='Speedup ParallelAsync', marker='o')

# Chart formatting
plt.title('Speedup of WhenAllAsync, Parallel, and ParallelAsync')
plt.xlabel('Items')
plt.ylabel('Speedup (Relative to AwaitedAsync)')
plt.xscale('log')  # Logarithmic scale on x-axis
plt.grid(True, which="both", ls="--")
plt.legend()
plt.tight_layout()

# Save the speedup plot
plt.savefig('speedup_log_scale.png', dpi=300)
plt.show()
