using System;
using System.Collections.Generic;

namespace SOPopularTags.Models
{
    public class HomeVM
    {
        public List<PopularTagForHomeVM> Items { get; set; }
        public bool Has_more { get; set; }
        public int Quota_max { get; set; }
        public int Quota_remaining { get; set; }
    }
}
