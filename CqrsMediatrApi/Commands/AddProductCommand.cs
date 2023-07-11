using CqrsMediatrApi.Models;
using MediatR;

namespace CqrsMediatrApi.Commands
{
    public record AddProductCommand(Product Product) :IRequest<Product>;
 
}
