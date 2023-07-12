using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.CommonWork;
using System;
using System.Threading.Tasks;

namespace RabbitMQ.SenderApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMQTutorialController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IRequestClient<BalanceUpdate> _client;
        public RabbitMQTutorialController(IBus bus,IRequestClient<BalanceUpdate> client)
        {
            _bus = bus;
            _client= client;
        }
        //Command Send Part
        [HttpPost("SendTutorial")]
        public async Task<IActionResult> Test1()
        {
            var product = new Product
            {
                Name = "Computer",
                Price = 2022
            };
            var url = new Uri("rabbitmq://localhost/send-tutorial");
            var endpoint = await _bus.GetSendEndpoint(url);
            await endpoint.Send(product);
            return Ok("hello command send tutorial ");
        }

        [HttpPost("publish-tutorial")]
        public async Task<ActionResult> Test2()
        {
            await _bus.Publish(new Person
            {
                Name = "Nana",
                Email = "Nana@gmail.com"
            });
            return Ok("publish Tutorial Done");
        }


        [HttpPost("RequestAndRespondTutorial")]
        public async Task<ActionResult> Test3()
        {
            var requestData = new BalanceUpdate
            {
                TypeOfInstruction="minusAmount",
                Amount=350
            };

            var request =  _client.Create(requestData);
            var response = await request.GetResponse<NowBalanceClass>();

            return Ok(response);
        }
    }
    
}
