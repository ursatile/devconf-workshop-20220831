using EasyNetQ;
using Messages;

var bus = RabbitHutch.CreateBus("amqps://ivikyigb:EAi4szwzo1_0gQhg-3SS3gPkGgw0JJnn@sweet-pink-deer.rmq4.cloudamqp.com/ivikyigb");
bus.PubSub.Subscribe<Greeting>("dylan-beattie", Handle);
Console.WriteLine("Subscribed to Greeting messsages");
Console.ReadLine();


void Handle(Greeting greeting) {
    if (greeting.Name.EndsWith("5")) throw new Exception("5 is not allowed!");
    Console.WriteLine(greeting);
}
