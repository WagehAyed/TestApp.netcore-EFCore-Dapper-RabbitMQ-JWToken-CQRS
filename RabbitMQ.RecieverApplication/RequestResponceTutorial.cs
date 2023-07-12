using MassTransit;
using RabbitMQ.CommonWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.RecieverApplication
{
    public class RequestResponceTutorial : IConsumer<BalanceUpdate>
    {
        public async Task Consume(ConsumeContext<BalanceUpdate> context)
        {
            var data = context.Message;
            var nowBalance = new NowBalanceClass
            {
                Balance=1000
            };
            await context.RespondAsync<NowBalanceClass>(nowBalance);
                
        }
    }
}
