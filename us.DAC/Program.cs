using System.Diagnostics;
using System.Net;
using us.Service;

HttpListener listener = new HttpListener();
listener.Prefixes.Add("http://*:80/");
listener.Start();
Console.WriteLine("DivideAndConquer is listening...");

bool isListeningFromSeed = true;

while (isListeningFromSeed)
{
    var sw = Stopwatch.StartNew();
    BinarySearchTreeService.GetResponse(listener, us.Domain.Constants.SearchType.DivideAndConquer, "programme");
    Console.WriteLine($"DivideAndConquer search ran for {sw.ElapsedMilliseconds}ms.");
    sw.Restart();
    isListeningFromSeed = false;
}