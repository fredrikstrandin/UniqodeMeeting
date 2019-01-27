using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesServices.Models.Feed
{
    public class FeedItem
    {
        public string Id { get; set; }
        public string FeedType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string GroupId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public List<ReactionItem> Reactions { get; set; }
    }
}
