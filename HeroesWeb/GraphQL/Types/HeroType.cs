using GraphQL.DataLoader;
using GraphQL.Types;
using HeroesServices.Models.Skills;
using HeroesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeroesWeb.GraphQL.Types
{
    public class HeroType : ObjectGraphType<HeroItem>
    {
        public HeroType(IDataLoaderContextAccessor dataLoader)
        {
            Field(t => t.Id);
            Field(t => t.EmpNo).Description("This is the employment Number");
            Field(t => t.Name);
            Field(t => t.City);
            Field<ListGraphType<SkillType>>(
            "skills",
            resolve: context =>
            {
                return context.Source.Skills;
            });
        }
    }
}
