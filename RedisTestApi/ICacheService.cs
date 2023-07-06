using System;
using System.Threading;
using System.Threading.Tasks;

namespace RedisTestApi
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key,CancellationToken cancellationToken=default)where T:class;
        Task<bool> SetAsync<T>(string key,  T value, DateTimeOffset expirationTime, CancellationToken cancellationToken = default) where T:class;
        Task<bool> RemoveAsync(string key,CancellationToken cancellationToken = default) ;
    }
}
