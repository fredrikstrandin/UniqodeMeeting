using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.GraphQL
{
    public class HeroSchema : Schema
    {
        public HeroSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<HeroesQuery>();
            Mutation = resolver.Resolve<HeroMutation>();
        }
    }
}
