using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine($"Main start   | Thread {Environment.CurrentManagedThreadId}");

        await A();

        Console.WriteLine($"Main end     | Thread {Environment.CurrentManagedThreadId}");
    }

    static async Task A()
    {
        Console.WriteLine($"A start      | Thread {Environment.CurrentManagedThreadId}");

        await B();

        Console.WriteLine($"A end        | Thread {Environment.CurrentManagedThreadId}");
    }

    static async Task B()
    {
        Console.WriteLine($"B start      | Thread {Environment.CurrentManagedThreadId}");

        await Task.Delay(1000);

        Console.WriteLine($"B end        | Thread {Environment.CurrentManagedThreadId}");
    }
}
