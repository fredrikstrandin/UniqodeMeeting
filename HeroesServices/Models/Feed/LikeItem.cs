using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesServices.Models.Feed
{
    public class ReactionItem
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}
