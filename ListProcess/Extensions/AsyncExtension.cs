namespace ListProcess.Extensions
{
    public static class AsyncExtension
    {
        /// <summary>
        /// Run async loop to execute the code concurrently with maximum number of tasks
        /// </summary>
        public static async Task RunTasksAsync<T>(this IEnumerable<T> list, Func<T, Task> action, int maximumConcurrentTask, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();
            foreach (var item in list)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                var task = action(item);
                tasks.Add(task);

                while (tasks.Count >= maximumConcurrentTask)
                {
                    Task finishedTask = await Task.WhenAny(tasks).ConfigureAwait(continueOnCapturedContext: false);
                    tasks.Remove(finishedTask);
                }
            }

            // make sure all tasks are finished
            await Task.WhenAll(tasks).ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <summary>
        /// Run async loop to execute the code concurrently, and await to get result from the tasks with maximum number of tasks
        /// </summary>
        public static async Task<List<R>> GetResultListAsync<T, R>(this IEnumerable<T> list, Func<T, Task<R>> action, int maximumConcurrentTask, CancellationToken cancellationToken)
        {
            var tasks = new List<Task<R>>();
            var results = new List<R>();
            foreach (var item in list)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                var task = action(item);
                tasks.Add(task);

                while (tasks.Count >= maximumConcurrentTask)
                {
                    Task<R> finishedTask = await Task.WhenAny(tasks).ConfigureAwait(continueOnCapturedContext: false);
                    tasks.Remove(finishedTask);

                    var result = await finishedTask;
                    results.Add(result);
                }
            }

            var remainResults = await Task.WhenAll(tasks).ConfigureAwait(continueOnCapturedContext: false);
            results.AddRange(remainResults);

            return results;
        }
    }
}