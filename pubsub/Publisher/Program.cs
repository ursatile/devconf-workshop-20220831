// See https://aka.ms/new-console-template for more information
using EasyNetQ;
using Messages;

var bus = RabbitHutch.CreateBus("amqps://ivikyigb:EAi4szwzo1_0gQhg-3SS3gPkGgw0JJnn@sweet-pink-deer.rmq4.cloudamqp.com/ivikyigb");
Console.WriteLine("Press any key to publish a message...");
int i = 0;
while (true) {
    Console.ReadKey();
    var greeting = new Greeting() {
        Name = $"Greeting {i++}"
    };
    bus.PubSub.Publish(greeting);
    Console.WriteLine($"Published {greeting}");
} 
