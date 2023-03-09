using Confluent.Kafka;
using Newtonsoft.Json;
using UsageDemo.DataAcessLayer;
using UsageDemo.Models;
using UsageDemo.Services.Interface;

namespace UsageDemo.Services.Class
{
    public class ApacheKafkaConsumerService : IHostedService
    {
        private ConsumerConfig _configuration;
        private readonly IConfiguration _config;
        private readonly IRedisService _redisService;
        private readonly IClickHoueDataManager _clickHouseDataManager;
        private readonly IPostgresDataManager _postgresService;
        public ApacheKafkaConsumerService(ConsumerConfig configuration, IConfiguration config, IRedisService redisService, IClickHoueDataManager clickHouseDataManager, IPostgresDataManager postgresService)
        {
            _configuration = configuration;
            _config = config;
            _redisService = redisService;
            _clickHouseDataManager = clickHouseDataManager;
            _postgresService = postgresService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = _configuration.GroupId,
                BootstrapServers = _configuration.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            try
            {
                using (var consumerBuilder = new ConsumerBuilder
                <Ignore, string>(config).Build())
                {
                    consumerBuilder.Subscribe(_config.GetSection("TopicName").Value);
                    var cancelToken = new CancellationTokenSource();

                    try
                    {
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume
                               (cancelToken.Token);
                            var usageRequest = JsonConvert.DeserializeObject<Usage>(consumer.Message.Value);
                            if(usageRequest != null)
                            {
                                _redisService.SubmitUsageDataToRedisCache(usageRequest);
                                _clickHouseDataManager.SubmitUsageDataToClickHouse(usageRequest);                              
                                _postgresService.SubmitUsageDataToPostgres(usageRequest);
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumerBuilder.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
