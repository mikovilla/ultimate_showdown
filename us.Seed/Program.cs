using System.Text;

Console.WriteLine("Seed is starting");
Thread.Sleep(30000);
string relativePath = "dictionary.json";
string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
string dictionary = File.ReadAllText(fullPath);

try
{
    using (HttpClient client = new HttpClient())
    {
        var podNames = new[] { "greed-service", "dac-service", "dp-service" };
        StringContent content = new StringContent($"{dictionary}", Encoding.UTF8, "application/json");

        foreach(var podName in podNames)
        {
            string receiverUrl = $"http://{podName}:80";
            HttpResponseMessage response = await client.PostAsync(receiverUrl, content);
            string reply = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Receiver replied: {reply}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

