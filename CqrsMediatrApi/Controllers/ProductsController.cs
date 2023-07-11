using CqrsMediatrApi.Commands;
using CqrsMediatrApi.Models;
using CqrsMediatrApi.Notifications;
using CqrsMediatrApi.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CqrsMediatrApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    { 
        private readonly ISender _sender;
        private readonly IPublisher _publisher;
        public ProductsController(  ISender sender, IPublisher publisher)
        { 
            _sender = sender;
            _publisher = publisher;

        }
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var product = await _sender.Send(new GetProductsQuery());
            return Ok(product);
        }
        [HttpGet("{id:int}",Name ="GetProductById")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _sender.Send(new GetProductByIdQuery(id));
            return Ok(product); 

        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] Product product)
        {
            var newProduct =await _sender.Send(new AddProductCommand(product));
            await _publisher.Publish(new ProductAddNotification(newProduct));
            return  CreatedAtRoute("GetProductById", new {id= newProduct .Id},newProduct);
        }
    }
}
