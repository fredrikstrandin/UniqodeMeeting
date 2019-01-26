using GraphQL.Types;
using HeroesWeb.GraphQL.Types;
using HeroesWeb.Models;
using HeroesWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.GraphQL
{
    public class HeroMutation :  ObjectGraphType
    {
        public HeroMutation(IHeroesService heroesService)
        {

            FieldAsync<HeroType>(
                "createHero",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<HeroInputType>> { Name = "hero" }),
                    resolve: async context =>
                    {
                        var hero = context.GetArgument<HeroItem>("hero");
                        return await context.TryAsyncResolve(
                            async c => await heroesService.CreateAsync(hero));
                    });
        }
    }
}
