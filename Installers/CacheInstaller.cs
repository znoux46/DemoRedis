using DemoRedis.Configurations;
using DemoRedis.Services;
using StackExchange.Redis;

namespace DemoRedis.Installers
{
    public class CacheInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection service, IConfiguration configuration)
        {
            var redisConfiguration = new RedisConfiguration();
            configuration.GetSection("RedisConfiguration").Bind(redisConfiguration);

            service.AddSingleton(redisConfiguration);

            if (!redisConfiguration.Enable)
                return;

            service.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConfiguration.ConnectionString));
            service.AddStackExchangeRedisCache(option => option.Configuration = redisConfiguration.ConnectionString);
            service.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}