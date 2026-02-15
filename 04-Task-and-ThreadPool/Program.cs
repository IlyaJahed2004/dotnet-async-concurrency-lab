using System.Runtime.CompilerServices;

namespace _04_Task_and_ThreadPool;

class Program
{
    static void Main(string[] args)
    {
        // var thread = new Thread(SaveFile);
        // thread.Start();
        // thread.IsBackground = true;

        Task<int> task = Task.Run(() =>
        {
            Console.WriteLine("Hello World!");
            return 3;
        });

        Task<int> task1 = Task.Factory.StartNew(
            () =>
            {
                Console.WriteLine("Hello World from Task.Factory!");
                return 4;
            },
            TaskCreationOptions.LongRunning
        );

        task.Wait();
        Console.WriteLine($"Task result: {task.Result}");

        task1.Wait();
        Console.WriteLine($"Task1 result: {task1.Result}");
    }
}
