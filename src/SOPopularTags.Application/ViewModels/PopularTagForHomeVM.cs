using System.ComponentModel.DataAnnotations;

namespace SOPopularTags.Application.ViewModels
{
    public class PopularTagForHomeVM
    {
        public int Count { get; set; }
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:D2}")]
        public double PopularityPercent { get; set; }
    }
}
