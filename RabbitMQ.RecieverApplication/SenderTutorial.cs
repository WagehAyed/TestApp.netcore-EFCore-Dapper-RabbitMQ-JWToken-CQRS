using MassTransit;
using RabbitMQ.CommonWork;
using System.Threading.Tasks;

namespace RabbitMQ.RecieverApplication
{
    public class SenderTutorial : IConsumer<Product>
    {
        public Task Consume(ConsumeContext<Product> context)
        {
            var product = context.Message;
            return Task.CompletedTask;
        }
    }

    public class SenderTutorial2 : IConsumer<Product>
    {
        public Task Consume(ConsumeContext<Product> context)
        {
            var product = context.Message;
            return Task.CompletedTask;
        }
    }

}
