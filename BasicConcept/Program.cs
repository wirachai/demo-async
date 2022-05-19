using BasicConcept.App;
using System.Diagnostics;

namespace BasicConcept
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("====================================");
            Console.WriteLine("1) Start Synchonous App");
            Console.WriteLine("====================================");
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var syncApp = new SyncApp();
            syncApp.Process();

            stopwatch.Stop();
            Console.WriteLine($"Total Elapsed Time: {stopwatch.Elapsed}");


            Console.WriteLine("====================================");
            Console.WriteLine("2) Start Asynchonous App (like sync)");
            Console.WriteLine("====================================");

            stopwatch.Reset();
            stopwatch.Start();

            var asyncApp = new AsyncApp();
            await asyncApp.Process();

            stopwatch.Stop();
            Console.WriteLine($"Total Elapsed Time: {stopwatch.Elapsed}");


            Console.WriteLine("====================================");
            Console.WriteLine("3) Start Asynchonous App");
            Console.WriteLine("====================================");

            stopwatch.Reset();
            stopwatch.Start();

            await asyncApp.ProcessAsync();

            stopwatch.Stop();
            Console.WriteLine($"Total Elapsed Time: {stopwatch.Elapsed}");
            

            Console.WriteLine("====================================");
            Console.WriteLine("4) Start Asynchonous App (optimized)");
            Console.WriteLine("====================================");

            stopwatch.Reset();
            stopwatch.Start();

            await asyncApp.OptimizedProcessAsync();

            stopwatch.Stop();
            Console.WriteLine($"Total Elapsed Time: {stopwatch.Elapsed}");
        }
    }
}