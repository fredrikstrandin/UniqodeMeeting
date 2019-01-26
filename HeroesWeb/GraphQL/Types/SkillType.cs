using GraphQL.Types;
using HeroesServices.Models.Skills;
using HeroesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.GraphQL.Types
{
    public class SkillType : ObjectGraphType<Skill>
    {
        public SkillType()
        {
            Field(t => t.Name).Description("Name of the skill");            
        }
    }
}
