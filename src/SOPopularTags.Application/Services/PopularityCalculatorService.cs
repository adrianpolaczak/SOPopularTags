using System.Collections.Generic;
using SOPopularTags.Application.Interfaces;
using SOPopularTags.Domain.Models;

namespace SOPopularTags.Application.Services
{
    public class PopularityCalculatorService : IPopularityCalculatorService
    {
        // Simple method to calculate popularity percentage of items in a given SOTagRequestItem list
        public List<SOTagRequestItem> CalculateTagsPopulatiryPercentage(List<SOTagRequestItem> tagRequestItems)
        {
            int totalCount = 0;
            foreach (var tagRequestItem in tagRequestItems)
            {
                totalCount += tagRequestItem.Count;
            }
            for (int i = 0; i < tagRequestItems.Count; i++)
            {
                tagRequestItems[i].PopularityPercent = (double)tagRequestItems[i].Count / totalCount * 100;
            }
            return tagRequestItems;
        }
    }
}
