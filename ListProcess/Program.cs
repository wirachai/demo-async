using ListProcess.App;
using System.Diagnostics;

namespace ListProcess
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var data = CreateSampleData();
            var service = new Service();

            Console.WriteLine("====================================");
            Console.WriteLine($"1) Async Loop (normal loop takes time {data.Count} x 0.1 sec = {data.Count * 0.1} sec)");
            Console.WriteLine("====================================");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            await service.AddAsync(data);

            stopwatch.Stop();
            Console.WriteLine($"Total Elapsed Time: {stopwatch.Elapsed}");

            Console.WriteLine("====================================");
            Console.WriteLine("2) Async Loop with returning result");
            Console.WriteLine("====================================");

            stopwatch.Reset();
            stopwatch.Start();

            await service.GetIdsAsync(data);

            stopwatch.Stop();
            Console.WriteLine($"Total Elapsed Time: {stopwatch.Elapsed}");

            Console.WriteLine("====================================");
            Console.WriteLine("3) Async Loop with maximum concurrent task (effective way)");
            Console.WriteLine("====================================");

            stopwatch.Reset();
            stopwatch.Start();

            await service.AddAsync(data, maximumConcurrentTask: 100);

            stopwatch.Stop();
            Console.WriteLine($"Total Elapsed Time: {stopwatch.Elapsed}");

            Console.WriteLine("====================================");
            Console.WriteLine("4) Add using Extension (more simply code)");
            Console.WriteLine("====================================");

            stopwatch.Reset();
            stopwatch.Start();

            await service.AddWithExtensionAsync(data, maximumConcurrentTask: 100);

            stopwatch.Stop();
            Console.WriteLine($"Total Elapsed Time: {stopwatch.Elapsed}");

            Console.WriteLine("====================================");
            Console.WriteLine("5) Get Ids using Extension (more simply code)");
            Console.WriteLine("====================================");

            stopwatch.Reset();
            stopwatch.Start();

            await service.GetIdsdWithExtensionAsync(data, maximumConcurrentTask: 100);

            stopwatch.Stop();
            Console.WriteLine($"Total Elapsed Time: {stopwatch.Elapsed}");
        }

        private static List<int> CreateSampleData()
        {
            List<int> data = new List<int>();
            for (int i = 0; i < 10000; i++)
            {
                data.Add(i);
            }
            return data;
        }
    }
}