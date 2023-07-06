using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace RedisTestApi
{
    public class CacheService : ICacheService
    {
        private IDatabase _db;
        public CacheService()
        {
            ConfigureRedis();
        }
        private void ConfigureRedis()
        {
            _db = ConnectionHelper.Connection.GetDatabase();
        }
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            var value =   _db.StringGetAsync(key);
            if (!string.IsNullOrEmpty(value.Result))
                return JsonConvert.DeserializeObject<T>(value.Result);

            return default;

        }
        public async Task<bool> SetAsync<T>(string key, T value, DateTimeOffset expirationTime, CancellationToken cancellationToken = default) where T : class
        {
            TimeSpan expireTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = await _db.StringSetAsync(key, JsonConvert.SerializeObject(value), expireTime);
            return isSet;
        }

        public async Task<bool> RemoveAsync(string key, CancellationToken cancellationToken = default)  
        {
            var _isKeyExist= await _db.KeyExistsAsync(key);
            if (_isKeyExist) {
            return await _db.KeyDeleteAsync(key);
            }
            return  false;
         }

        

         
    }
}
