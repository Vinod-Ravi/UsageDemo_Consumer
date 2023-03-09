using Newtonsoft.Json;
using StackExchange.Redis;
using UsageDemo.Models;
using UsageDemo.Services.Interface;

namespace UsageDemo.Services.Class
{
    public class RedisService:IRedisService
    {
        private readonly IConnectionMultiplexer _redis;
        public RedisService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        public async Task<bool> SubmitUsageDataToRedisCache(Usage usage)
        {
            try
            {
                var key = "UsageData" + "_" + usage.Id;
                var redisConnection = _redis.GetDatabase();
                return await redisConnection.StringSetAsync(key, JsonConvert.SerializeObject(usage));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
