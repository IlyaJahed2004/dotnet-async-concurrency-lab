namespace _02_Thread_State;

class Program
{
    static void Main(string[] args)
    {
        var thread = new Thread(Run);
        thread.Priority = ThreadPriority.Highest;
        thread.Start('T');

        Run('M');
        void Run(object? obj)
        {
            char c = (char)obj!;
            for (int i = 0; i < 100; i++)
            {
                Console.Write(c + " ");
            }
        }
    }

    static void Main3(string[] args)
    {
        for (int i = 0; i < 10; i++)
        {
            var thread = new Thread(() =>
            {
                Console.Write(i);
            }).Start();
        }
    }

    static void Main2(string[] args)
    {
        bool finished = false;
        new Thread(Run).Start();

        void Run()
        {
            if (!finished)
            {
                finished = true;
                for (int cycles = 0; cycles < 100; cycles++)
                {
                    Console.Write("? ");
                }
            }
        }
    }

    static void Main1(string[] args)
    {
        var thread = new Thread(Run);
        thread.Start('t');
        // thread.Join();

        Run('m');
        void Run(object? obj)
        {
            char c = (char)obj!;
            for (int cycles = 0; cycles < 100; cycles++)
            {
                Console.Write(c + " ");
            }
        }
    }
}
