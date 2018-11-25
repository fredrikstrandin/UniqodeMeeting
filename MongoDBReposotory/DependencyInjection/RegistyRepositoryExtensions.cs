using HeroesWeb.Models;
using HeroesWeb.Repositorys;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HeroMongoDBRepository.DependencyInjection
{
    public static class RegistyRepositoryExtensions
    {
        public static IServiceCollection AddMongoDBRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbDatabaseSetting>(configuration.GetSection("MongoDBDatabaseSetting"));

            services.AddSingleton<IMongoDBContext, MongoDBContext>();
            services.AddSingleton<IHeroRepository, HeroMongodbRepository>();
            services.AddSingleton<IETagRepository, ETagRepository>();

            return services;
        }
    }
}
