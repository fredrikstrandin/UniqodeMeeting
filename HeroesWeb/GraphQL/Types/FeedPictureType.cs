using GraphQL.Types;
using HeroesWeb.GraphQL.Interface;

namespace HeroesServices.Models.Feed
{
    public class PictureFeedType : ObjectGraphType<PictureFeedItem>
    {
        public PictureFeedType()
        {
            Name = "PictureFeed";

            Field(t => t.Id);
            Field(t => t.FeedType);
            Field(t => t.GroupId);
            Field(t => t.CreatedBy);
            Field(t => t.CreatedOn);
            Field(t => t.Title);
            Field(t => t.Text);
            Field<IntGraphType>(
                "reactionCount",
                resolve: context => context.Source.Reactions.Count
            );
            Field(t => t.PictureId);

            Interface<FeedInterface>();

        }
    }
}
