using GraphQL.Types;
using HeroesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.GraphQL.Types
{
    public class HeroInputType : InputObjectGraphType
    {
        public HeroInputType()
        {
            Name = "heroInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<StringGraphType>("city");
        }
    }
}
