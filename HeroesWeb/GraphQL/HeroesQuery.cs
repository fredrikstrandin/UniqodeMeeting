
using GraphQL.Types;
using HeroesWeb.GraphQL.Types;
using HeroesWeb.Models;
using HeroesWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HeroesWeb.GraphQL
{
    public class HeroesQuery : ObjectGraphType
    {
        public HeroesQuery(IHeroesService heroesService)
        {
            FieldAsync<ListGraphType<HeroType>>(
                "heroes",
                resolve: async context => await heroesService.GetHerosAsync()
                );

            FieldAsync<HeroType>(
                "hero",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                    { Name = "empno"}),    
                resolve: async context =>
                {
                    var user = (ClaimsPrincipal)context.UserContext;

                    var id = context.GetArgument<int>("empno");
                    return await heroesService.GetHeroEmpNoAsync(id);
                });
        }
    }
}
