using EasyNetQ;
using Messages;

var bus = RabbitHutch.CreateBus("amqps://ivikyigb:EAi4szwzo1_0gQhg-3SS3gPkGgw0JJnn@sweet-pink-deer.rmq4.cloudamqp.com/ivikyigb");
bus.PubSub.Subscribe<Greeting>("SUBSCRIPTION_ID", greeting => {
    Console.WriteLine(greeting);
});
Console.WriteLine("Subscribed to Greeting messsages");
Console.ReadLine();

