namespace BasicConcept.App
{
    internal class AsyncApp
    {
        public async Task Process()
        {
            string tx = await CallApi();
            int id = await Query1();
            string name = await Query2(id);

            // process final result
            Console.WriteLine($"{name} did {tx}");
        }

        public async Task ProcessAsync()
        {
            // call sub-process
            var callingApi = CallApi();
            var query1 = Query1();

            // get result, and call next step (Query2)
            string tx = await callingApi;
            int id = await query1;
            string name = await Query2(id);

            // process final result
            Console.WriteLine($"{name} did {tx}");
        }

        public async Task OptimizedProcessAsync()
        {
            // call all steps
            var callingApi = CallApi();
            var callingQuery = CallQuery();

            // await all task to be finished
            await Task.WhenAll(callingApi, callingQuery);

            // get result
            string tx = callingApi.Result;
            string name = callingQuery.Result;
            
            // process final result
            Console.WriteLine($"{name} did {tx}");
        }

        private async Task<string> CallApi()
        {
            Console.WriteLine(">> Start CallApi ...");
            await Task.Delay(2000);  // simulate processing time
            Console.WriteLine("<< Finish CallApi");

            return "some transaction";
        }

        private async Task<int> Query1()
        {
            Console.WriteLine(">> Start Query1 ...");
            await Task.Delay(800);  // simulate processing time
            Console.WriteLine("<< Finish Query1");

            return 0;
        }

        private async Task<string> Query2(int id)
        {
            Console.WriteLine(">> Start Query2 ...");
            await Task.Delay(1000);  // simulate processing time
            Console.WriteLine("<< Finish Query2");

            return $"tx-{id}";
        }

        private async Task<string> CallQuery()
        {
            int id = await Query1();
            string name = await Query2(id);
            return name;
        }
    }
}