using GraphQL;
using GraphQL.Server;
using HeroesServices.Interface;
using HeroesServices.Services;
using HeroesWeb.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HeroesWeb.GraphQL.DependencyInjection
{
    public static class RegistyServicesExtensions
    {
        public static IServiceCollection AddGraphQL(this IServiceCollection services)
        {
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(
                s.GetRequiredService));

            services.AddScoped<HeroSchema>();
            services.AddScoped<HeroMutation>();

            services.AddGraphQL(o => { o.ExposeExceptions = true; })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddUserContextBuilder(httpContext => httpContext.User)
                .AddDataLoader();

            return services;
        }
    }
}
