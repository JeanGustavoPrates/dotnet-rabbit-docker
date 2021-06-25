using MassTransit;
using Newtonsoft.Json;
using PublisherRabbitMq;
using System;
using System.Threading.Tasks;

namespace ConsumerRabbitMq
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://rabbit"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("value-events-listener", e =>
                {
                    e.Consumer<ValueConsumer>();
                });
            });

            await bus.StartAsync();
            Console.WriteLine("Waiting for messages...");
            Console.ReadLine();

            await bus.StopAsync();
        }

        public class ValueConsumer : IConsumer<Message>
        {
            public Task Consume(ConsumeContext<Message> context)
            {
                Console.WriteLine($"Received [x] {JsonConvert.SerializeObject(context.Message)} [at] {DateTime.Now:u}");
                return Task.CompletedTask;
            }
        }
    }
}
