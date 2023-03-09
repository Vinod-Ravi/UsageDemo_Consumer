using UsageDemo.Models;

namespace UsageDemo.Services.Interface
{
    public interface IClickHoueDataManager
    {
        public void SubmitUsageDataToClickHouse(Usage usage);

    }
}
