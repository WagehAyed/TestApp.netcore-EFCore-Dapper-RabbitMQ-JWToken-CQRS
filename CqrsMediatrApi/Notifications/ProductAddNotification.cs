using CqrsMediatrApi.Models;
using MediatR;

namespace CqrsMediatrApi.Notifications
{
    public record ProductAddNotification(Product Prodyct):INotification;
     
}
