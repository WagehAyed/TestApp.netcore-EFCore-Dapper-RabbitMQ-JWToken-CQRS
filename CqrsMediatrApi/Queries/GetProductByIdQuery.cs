using CqrsMediatrApi.Models;
using MediatR;

namespace CqrsMediatrApi.Queries
{
    public record GetProductByIdQuery(int Id):IRequest<Product>;
    
}
