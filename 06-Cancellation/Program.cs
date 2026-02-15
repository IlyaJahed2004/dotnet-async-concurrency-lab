namespace _06_Cancellation;

class Program
{
    public static async Task Main(string[] args)
    {
        CancellationTokenSource cancellationtokensource = new();
        Console.WriteLine(cancellationtoken.IsCancellationRequested);
        await GetAllDataFromDatabaseAsync(cancellationtokensource.Token);

        cancellationtokensource.Cancel();

        await GetAllDataFromDatabaseAsync(cancellationtokensource.Token);
    }

    public static async Task GetAllDataFromDatabaseAsync(CancellationToken cancellationToken)
    {
        HttpClient client = new();
        var result = await client.GetAsync("https://google.com", cancellationToken);
        Console.WriteLine(result);
    }
}
