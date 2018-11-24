using HeroesWeb.Repositorys;
using HeroMemoryRepository.Interfaces;
using HeroMemoryRepository.ontext;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroMemoryRepository.DependencyInjection
{
    public static class RegistyRepositoryExtensions
    {
        public static IServiceCollection AddMemoryRepository(this IServiceCollection services)
        {
            services.AddSingleton<IMemoryContext, MemoryContext>();
            services.AddScoped<IHeroRepository, HeroesMemoryRepository>();
            
            return services;
        }
    }
}
