namespace _01_Process_vs_Thread;

class Program
{
    static void Main(string[] args)
    {
        Thread t = new Thread(WriteT);
        t.Start();
        // t.Join(); // Waits for the thread to finish before proceeding

        for (int i = 0; i < 100; i++)
        {
            Console.Write("M ");
        }
    }

    private static void WriteT()
    {
        for (int i = 0; i < 100; i++)
        {
            Console.Write("T ");
        }
    }
}
