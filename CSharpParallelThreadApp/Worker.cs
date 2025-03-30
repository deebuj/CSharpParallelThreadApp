using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Threading.Channels;
using System.Collections.Concurrent;

namespace CSharpParallelThreadApp
{
    public class Worker
    {
        private IConfiguration _configuration;
        public Worker(IConfiguration configuration) { 
            _configuration = configuration; 
        }
        public async Task Run()
        {
            Console.WriteLine($"Started running {_configuration.GetSection("HostData").Value}");

            const int totalTasks = 10;
            const int maxConcurrent = 2;

            Console.WriteLine($"Starting {totalTasks} tasks with max {maxConcurrent} concurrent...");
            // Create a range of numbers to process
            var numbers = Enumerable.Range(1, totalTasks);

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = maxConcurrent
            };

            /*Channel*/
            var channel = Channel.CreateBounded<int>(totalTasks);
            await Parallel.ForEachAsync(numbers, options, async (number, ct) => {
                var random = new Random();
                Console.WriteLine($"Task {number} started");
                await Task.Delay(500, ct);
                await channel.Writer.WriteAsync(number * 2, ct);
            });
            channel.Writer.Complete();
            await foreach (var result in channel.Reader.ReadAllAsync())
            {
                Console.WriteLine($"Task {result / 2}: Result: {result}");
            }

            /*Concurrent bag*/
            ConcurrentBag<int> concurrentBag = new ConcurrentBag<int>();
            await Parallel.ForEachAsync(numbers, options, async (number, ct) => {
                var random = new Random();
                Console.WriteLine($"Task {number} started");
                await Task.Delay(100, ct);
                concurrentBag.Add(number * 2);
            });

            foreach (int data in concurrentBag)
            {
                Console.WriteLine($"ConcurentBag-{data}");
            }

            Console.WriteLine("All tasks completed!");
        }
    }
}
