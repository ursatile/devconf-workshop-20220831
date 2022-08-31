using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcGreeter;

// https://localhost:7247

using var channel = GrpcChannel.ForAddress("https://localhost:7247");
var client = new Greeter.GreeterClient(channel);
Console.WriteLine("Press a key to send a GRPC request");
while (true)
{
    var language = Console.ReadKey(true).Key switch {
        ConsoleKey.D1 => "pl-PL",
        ConsoleKey.D2 => "en-GB",
        ConsoleKey.D3 => "en-AU",
        _ => "en-US"
    };
    var request = new HelloRequest { 
        FirstName = "DevConf", 
        LastName = "2022",
        Language = language
    };
    var reply = await client.SayHelloAsync(request);
    Console.WriteLine("Greeting: " + reply.Message);
}