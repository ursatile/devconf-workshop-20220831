using System;
using Autobarn.Messages;
using EasyNetQ;

namespace Autobarn.PricingClient {
    internal class Program {
        static void Main(string[] args) {
            var bus = RabbitHutch.CreateBus(
                "amqps://ivikyigb:EAi4szwzo1_0gQhg-3SS3gPkGgw0JJnn@sweet-pink-deer.rmq4.cloudamqp.com/ivikyigb");
            bus.PubSub.Subscribe<NewVehicleMessage>("Autobarn.PricingClient",
                HandleNewVehicleMessage
            );
            Console.WriteLine("Subscribed to NewVehicleMessage");
            Console.ReadLine();
        }

        private static void HandleNewVehicleMessage(NewVehicleMessage message) {
            Console.WriteLine(message);
        }
    }
}
