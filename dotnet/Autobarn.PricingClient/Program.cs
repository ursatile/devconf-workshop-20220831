using System;
using System.Threading.Tasks;
using Autobarn.Messages;
using Autobarn.PricingEngine;
using EasyNetQ;
using Grpc.Net.Client;

namespace Autobarn.PricingClient {
    internal class Program {
        static void Main(string[] args) {
            var bus = RabbitHutch.CreateBus("amqps://ivikyigb:EAi4szwzo1_0gQhg-3SS3gPkGgw0JJnn@sweet-pink-deer.rmq4.cloudamqp.com/ivikyigb");
            using var channel = GrpcChannel.ForAddress("https://workshop.ursatile.com:5003/");
            var grpc = new Pricer.PricerClient(channel);
            bus.PubSub.Subscribe("Autobarn.PricingClient", CreateHandler(grpc));
            Console.WriteLine("Subscribed to NewVehicleMessage");
            Console.ReadLine();
        }

        private static Func<NewVehicleMessage, Task> CreateHandler(Pricer.PricerClient client) {
            return async message => {
                Console.WriteLine(message);
                Console.WriteLine("Calculating price!");
                var priceRequest = new PriceRequest {
                    Color = message.Color,
                    Year = message.Year,
                    Manufacturer = message.ManufacturerName,
                    Model = message.ModelName
                };
                var reply = await client.GetPriceAsync(priceRequest);
                Console.WriteLine($"Got a price: {reply.Price} {reply.CurrencyCode}");
            };
        }
    }
}
