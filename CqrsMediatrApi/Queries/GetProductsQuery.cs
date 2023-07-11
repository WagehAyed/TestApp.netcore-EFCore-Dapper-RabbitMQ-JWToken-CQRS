using CqrsMediatrApi.Models;
using MediatR;
using System.Collections;
using System.Collections.Generic;

namespace CqrsMediatrApi.Queries
{
    public record GetProductsQuery:IRequest<IEnumerable<Product>>
    {
    }
}
