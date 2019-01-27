using GraphQL.Types;
using HeroesServices.Models.Feed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.GraphQL.Interface
{
    public class FeedInterface : InterfaceGraphType<FeedItem>
    {
        public FeedInterface()
        {
            Name = "Feed";
            Field(t => t.Id);
            Field(t => t.FeedType);
            Field(t => t.GroupId);
            Field(t => t.CreatedBy);
            Field(t => t.CreatedOn);
            Field(t => t.Title);
            Field(t => t.Text);
            Field<IntGraphType>("reactionCount");

        }
    }
}
