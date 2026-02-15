namespace _03_Synchronization_Primitives;

class Program
{
    const string path =
        @"D:\term6\Dotnet_Exercises\dotnet-async-concurrency-lab\03-Synchronization-Primitives\text.txt";

    static readonly object _fileLock = new object();

    static void Main(string[] args)
    {
        new Thread(SaveFile).Start();

        SaveFile();

        void SaveFile()
        {
            lock (_fileLock)
            {
                File.AppendAllText(path, "Hello from another thread!\n");
            }
        }
    }
}
