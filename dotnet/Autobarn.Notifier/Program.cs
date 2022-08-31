using System;
using System.Threading.Tasks;
using Autobarn.Messages;
using EasyNetQ;
using Microsoft.AspNetCore.SignalR.Client;

namespace Autobarn.Notifier {
    internal class Program {
        const string SIGNALR_HUB_URL = "https://workshop.ursatile.com:5001/hub";

        static async Task Main(string[] args) {
            var hub = new HubConnectionBuilder().WithUrl(SIGNALR_HUB_URL).Build();
            //await hub.StartAsync();
            Console.WriteLine("Hub started!");
            var bus = RabbitHutch.CreateBus(
                "amqps://ivikyigb:EAi4szwzo1_0gQhg-3SS3gPkGgw0JJnn@sweet-pink-deer.rmq4.cloudamqp.com/ivikyigb");
            bus.PubSub.Subscribe("Autobarn.Notifier", CreateHandler(hub));
            Console.WriteLine("Subscribed to NewVehiclePriceMessage");
            Console.ReadLine();
            //await hub.StopAsync();
        }

        private static Func<NewVehiclePriceMessage, Task> CreateHandler(HubConnection hub) {
            return async message => { Console.WriteLine(message); };
        }
    }
}