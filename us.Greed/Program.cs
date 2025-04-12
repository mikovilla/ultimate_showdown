using System.Net;
using us.Service;

HttpListener listener = new HttpListener();
listener.Prefixes.Add("http://*:80/");
listener.Start();
Console.WriteLine("Greed is listening...");

bool isListeningFromSeed = true;

while (isListeningFromSeed)
{
    BinarySearchTreeService.GetResponse(listener, us.Domain.Constants.SearchType.Greedy, "programme");
    isListeningFromSeed = false;
}