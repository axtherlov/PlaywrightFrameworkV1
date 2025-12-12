using NUnit.Framework;

// Enable parallel execution at assembly level
[assembly: Parallelizable(ParallelScope.Fixtures)]

// Optional: Set the number of workers (default is number of CPU cores)
[assembly: LevelOfParallelism(1)]