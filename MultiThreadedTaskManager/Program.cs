using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MultiThreadedTaskManager
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== C# Parallel & Async Processing Demo ===");
            Console.WriteLine("Processing data batches using Task Parallel Library (TPL)...");
            Console.WriteLine();

            List<int> dataItems = new List<int>();
            for (int i = 1; i <= 20; i++)
            {
                dataItems.Add(i);
            }

            Stopwatch stopwatch = Stopwatch.StartNew();

            // 1. Concurrent processing using Parallel.ForEach
            Console.WriteLine("--- Executing Parallel.ForEach ---");
            Parallel.ForEach(dataItems, item =>
            {
                ProcessItem(item);
            });

            Console.WriteLine($"\nParallel operation completed in: {stopwatch.ElapsedMilliseconds} ms\n");

            // 2. Asynchronous task execution
            Console.WriteLine("--- Executing Async Tasks ---");
            stopwatch.Restart();
            await ProcessTasksAsync();
            Console.WriteLine($"Async tasks completed in: {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("\nAll processing completed successfully.");
            Console.ReadLine();
        }

        private static void ProcessItem(int itemId)
        {
            // Simulating a CPU-intensive operation
            Task.Delay(100).Wait();
            Console.WriteLine($"[Thread {Environment.CurrentManagedThreadId}] Processed Item #{itemId}");
        }

        private static async Task ProcessTasksAsync()
        {
            List<Task> tasks = new List<Task>();

            for (int i = 1; i <= 5; i++)
            {
                int taskId = i;
                tasks.Add(Task.Run(async () =>
                {
                    await Task.Delay(200);
                    Console.WriteLine($"Async Task #{taskId} executed on Thread {Environment.CurrentManagedThreadId}");
                }));
            }

            await Task.WhenAll(tasks);
        }
    }
}