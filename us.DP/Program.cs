using System.Net;
using us.Service;

HttpListener listener = new HttpListener();
listener.Prefixes.Add("http://*:80/");
listener.Start();
Console.WriteLine("DynamicProgramming is listening...");

bool isListeningFromSeed = true;

while (isListeningFromSeed)
{
    BinarySearchTreeService.GetResponse(listener, us.Domain.Constants.SearchType.DynamicProgramming, "programme");
    isListeningFromSeed = false;
}