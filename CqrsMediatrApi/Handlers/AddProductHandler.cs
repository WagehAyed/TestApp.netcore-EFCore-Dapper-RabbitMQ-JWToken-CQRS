using CqrsMediatrApi.Commands;
using CqrsMediatrApi.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CqrsMediatrApi.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
    {
        private readonly FakeDataStore _fakeDataStore;
        public AddProductHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }
        public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            await _fakeDataStore.AddProduct(request.Product);
            return request.Product;
        }
    }
}
