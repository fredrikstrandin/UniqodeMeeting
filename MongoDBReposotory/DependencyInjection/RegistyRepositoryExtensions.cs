using HeroesWeb.Repositorys;
using Microsoft.Extensions.DependencyInjection;

namespace HeroMongoDBRepository.DependencyInjection
{
    public static class RegistyRepositoryExtensions
    {
        public static IServiceCollection AddMongoDBRepository(this IServiceCollection services)
        {
            services.AddSingleton<IMongoDBContext, MongoDBContext>();
            services.AddScoped<IHeroRepository, HeroMongodbRepository>();

            services.AddScoped<IETagRepository, ETagRepository>();

            return services;
        }
    }
}
