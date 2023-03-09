using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using UsageDemo.Models;

namespace UsageDemo.Services.Interface
{
    public interface IPostgresDataManager
    {
        public Task<bool> SubmitUsageDataToPostgres(Usage usage);
    }
}
