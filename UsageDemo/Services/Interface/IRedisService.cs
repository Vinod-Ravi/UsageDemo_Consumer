using UsageDemo.Models;

namespace UsageDemo.Services.Interface
{
    public interface IRedisService
    {
        public Task<bool> SubmitUsageDataToRedisCache(Usage usage);

    }
}
