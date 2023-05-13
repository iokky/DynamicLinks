using DynamicLinks.Domain.Entity;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;


namespace DynamicLinks.Services
{
    //TODO все методы в async
    public class CacheService : ICacheService<DynamicLinkEntity>
    {
        private readonly IDatabase? _database;
        private bool _redisStatus;

        public CacheService()
        {
            //TODO обработать сценарий падения redis то что сейчас сильно тормозит процесс при упавшем redis
            try
            {
                var redis = ConnectionMultiplexer.Connect("localhost:6379");
                _database = redis.GetDatabase();
                _redisStatus = true;
            }
            catch (Exception ex)
            {
                _redisStatus = false;
            }
        }

        public bool GetRedisStatusOk() => _redisStatus;
        public object? Del(string key)
        {
            if (GetRedisStatusOk())
            {
                var _exist = _database!.KeyExists(key);
                if (_exist)
                {
                    return _database.KeyDelete(key);
                }
            }
            return default;
        }

        public async Task<DynamicLinkEntity?> Get(string key)
        {
            if (GetRedisStatusOk())
            {
                var value = await _database!.StringGetAsync(key);
                if (!string.IsNullOrEmpty(value))
                {
                    return JsonSerializer.Deserialize<DynamicLinkEntity>(value!);
                }
            }
            return default;
        }

        public bool? Set(string key, DynamicLinkEntity value)
        {
            if (GetRedisStatusOk())
            {
                //TODO разобраться в временем хранения кеша
                //var exparyTime = expirationTime.DateTime.Subtract(DateTime.Now);
                var isSet = _database!.StringSet(key, JsonSerializer.Serialize(value));
                return isSet;
            }
            return default;
        }

    }
}


