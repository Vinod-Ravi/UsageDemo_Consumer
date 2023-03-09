using ClickHouse.Ado;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using UsageDemo.Models;
using UsageDemo.Services.Interface;

namespace UsageDemo.Services.Class
{
    public class ClickHouseDataManager : IClickHoueDataManager
    {
        private readonly IConfiguration _config;
        public ClickHouseDataManager(IConfiguration config)
        {
            _config = config;
        }
        public void SubmitUsageDataToClickHouse(Usage usage)
        {
            try
            {
                using var conn = new ClickHouseConnection(_config.GetConnectionString("UsageDevClickHouseConnectionString"));
                conn.Open();
                string dtFormat=usage.UsageDate.ToString("yyyy-MM-dd");
                string command = $"INSERT INTO Usage_Database.usage_info (usageId, usageName, usageKey, usageServices,usageCharge,usageDate) VALUES " + $"('{usage.Id}', '{usage.UsageName}','{usage.UsageKey}','{usage.UsageServices}','{usage.UsageCharge}','{dtFormat}')";
                using var cmd = conn.CreateCommand(command);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
