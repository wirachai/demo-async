namespace BasicConcept.App
{
    internal class SyncApp
    {
        public void Process()
        {
            string tx = CallApi();
            int id = Query1();
            string name = Query2(id);

            // process final result
            Console.WriteLine($"{name} did {tx}");
        }

        private string CallApi()
        {
            Console.WriteLine(">> Start CallApi ...");
            Task.Delay(2000).Wait();  // simulate processing time
            Console.WriteLine("<< Finish CallApi");

            return "some transaction";
        }

        private int Query1()
        {
            Console.WriteLine(">> Start Query1 ...");
            Task.Delay(800).Wait();  // simulate processing time
            Console.WriteLine("<< Finish Query1");

            return 0;
        }

        private string Query2(int id)
        {
            Console.WriteLine(">> Start Query2 ...");
            Task.Delay(1000).Wait();  // simulate processing time
            Console.WriteLine("<< Finish Query2");

            return $"tx-{id}";
        }
    }
}