using Microsoft.Extensions.DependencyInjection;
using UsageDemo.DataAcessLayer;
using UsageDemo.Models;
using UsageDemo.Services.Interface;

namespace UsageDemo.Services.Class
{
    public class PostgresDataManager:IPostgresDataManager
    {
       // private readonly UsageDbContext _usageDbContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PostgresDataManager(IServiceScopeFactory serviceScopeFactory)
        {
           // _usageDbContext = usageDbContext;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task<bool> SubmitUsageDataToPostgres(Usage usage)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<UsageDbContext>();

                    await dbContext.UsageInformation.AddAsync(usage);
                    var simInfo = await dbContext.SaveChangesAsync();
                    if (simInfo.ToString() != null)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
