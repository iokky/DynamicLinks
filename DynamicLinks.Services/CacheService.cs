using DynamicLinks.Domain.Entity;
using StackExchange.Redis;
using System.Text.Json;


namespace DynamicLinks.Services
{
    public class CacheService : ICacheService<DynamicLinkEntity>
    {
        private readonly IDatabase _database;

        public CacheService()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            _database = redis.GetDatabase();
        }
        public object Del(string key)
        {
            var _exist = _database.KeyExists(key);
            if (_exist)
            {
                return _database.KeyDelete(key);
            }
            return false;
        }

        public DynamicLinkEntity Get(string key)
        {
           var value = _database.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<DynamicLinkEntity>(value);
            }
            return default;
        }

        public bool Set(string key, DynamicLinkEntity value)
        {
            //var exparyTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _database.StringSet(key, JsonSerializer.Serialize(value));
            return isSet;
        }
    }
}


