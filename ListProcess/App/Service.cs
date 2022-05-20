using ListProcess.Extensions;

namespace ListProcess.App
{
    internal class Service
    {
        // sample code #1: do loop async
        public async Task AddAsync(List<int> data)
        {
            List<Task> tasks = new List<Task>();
            foreach (var id in data)
            {
                var task = CallInsertApiAsync(id);
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
        }

        // sample code #2: do loop async with returning result
        public async Task<SampleModel[]> GetIdsAsync(List<int> data)
        {
            List<Task<SampleModel>> tasks = new List<Task<SampleModel>>();
            foreach (var id in data)
            {
                var task = CallGetNameApiAsync(id);
                tasks.Add(task);
            }
            var result = await Task.WhenAll(tasks);
            return result;
        }

        // sample code #3: do loop async with maximumConcurrentTask (prevent consuming too much server resource)
        public async Task AddAsync(List<int> data, int maximumConcurrentTask)
        {
            var tasks = new List<Task>();
            foreach (var id in data)
            {
                var task = CallInsertApiAsync(id);
                tasks.Add(task);

                while (tasks.Count >= maximumConcurrentTask)
                {
                    Task finishedTask = await Task.WhenAny(tasks);
                    tasks.Remove(finishedTask);
                }
            }

            // make sure all tasks are finished
            await Task.WhenAll(tasks);
        }

        // sample code #4: add using extension
        public async Task AddWithExtensionAsync(List<int> data, int maximumConcurrentTask)
        {
            var timeout = TimeSpan.FromSeconds(30);
            CancellationTokenSource cts = new CancellationTokenSource(timeout);

            await data.RunTasksAsync(CallInsertApiAsync, maximumConcurrentTask, cts.Token);
        }

        // sample code #5: get ids using extension
        public async Task<List<SampleModel>> GetIdsdWithExtensionAsync(List<int> data, int maximumConcurrentTask)
        {
            var timeout = TimeSpan.FromSeconds(30);
            CancellationTokenSource cts = new CancellationTokenSource(timeout);

            var result = await data.GetResultListAsync(CallGetNameApiAsync, maximumConcurrentTask, cts.Token);
            return result;
        }

        private async Task CallInsertApiAsync(int id)
        {
            await Task.Delay(100);
        }

        private async Task<SampleModel> CallGetNameApiAsync(int id)
        {
            await Task.Delay(100);
            return new SampleModel
            {
                Id = id,
                Name = $"name of {id}"
            };
        }
    }

    internal class SampleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}