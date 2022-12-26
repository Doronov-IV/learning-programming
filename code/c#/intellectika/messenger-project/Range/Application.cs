namespace Range
{
    public class Application
    {

        public async Task Run()
        {
            Console.WriteLine("Run start.");

            Task task = new(Foo);

            task.Start();
            
            var awaiter = task.GetAwaiter();

            task.ContinueWith((t) =>
            {
                var t2 = new Task(Done);

                t2.RunSynchronously();

                return t2;
            }, TaskContinuationOptions.AttachedToParent);

            Console.WriteLine("Run end.");

            Console.ReadKey();
        }

        public void Foo()
        {
            Console.WriteLine($"Foo start. Thread {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep(5000);

            Console.WriteLine("Foo end.");
        }


        public void Done()
        {
            Console.WriteLine($"Done start. Thread {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep(1000);

            Console.WriteLine("Done end.");
        }
    }
}
