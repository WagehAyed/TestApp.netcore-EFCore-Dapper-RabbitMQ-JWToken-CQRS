using MassTransit;
using RabbitMQ.CommonWork;
using System.Threading.Tasks;

namespace RabbitMQ.RecieverApplication
{
    public class PublisherTutorial : IConsumer<Person>
    {
        public Task Consume(ConsumeContext<Person> context)
        {
            var person = context.Message;
            return Task.CompletedTask;
        }
    }


    public class PublisherTutorial2 : IConsumer<Person>
    {
        public Task Consume(ConsumeContext<Person> context)
        {
            var person = context.Message;
            return Task.CompletedTask;
        }
    }


    public class PublisherTutorial3 : IConsumer<Person>
    {
        public Task Consume(ConsumeContext<Person> context)
        {
            var person = context.Message;
            return Task.CompletedTask;
        }
    }
}
