using MassTransit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublisherRabbitMq
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("rabbitmq://rabbit"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });

            await bus.StartAsync();

            for (int i = 0; i < 10; i++)
            {
                await Task.WhenAll(Enumerable.Range(0, 100).Select(i =>
                {
                    var message = new Message { Value = $"Message {i}" };
                    return bus.Publish(message);
                }));

                Thread.Sleep(10000);
            }

            await bus.StopAsync();
        }
    }
}
