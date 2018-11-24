using HeroesServices.Interface;
using HeroesServices.Services;
using HeroesWeb.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HeroMongoDBRepository.DependencyInjection
{
    public static class RegistyServicesExtensions
    {
        public static IServiceCollection AddHeroesServices(this IServiceCollection services)
        {
            services.AddScoped<IHeroesService, HeroesService>();
            services.AddScoped<IETagService, ETagService>();

            return services;
        }
    }
}
