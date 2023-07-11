using CqrsMediatrApi.Models;
using CqrsMediatrApi.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CqrsMediatrApi.Handlers
{
    public class CachInvalidationHandler : INotificationHandler<ProductAddNotification>
    {
        private readonly FakeDataStore _fakeDataStore;

        public CachInvalidationHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }

        public async Task Handle(ProductAddNotification notification, CancellationToken cancellationToken)
        {
            await _fakeDataStore.EventOccured(notification.Prodyct, "Cache Invalidted");
            await Task.CompletedTask;
        }
    }
}
