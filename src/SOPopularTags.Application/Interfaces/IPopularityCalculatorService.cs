using System.Collections.Generic;
using SOPopularTags.Domain.Models;

namespace SOPopularTags.Application.Interfaces
{
    public interface IPopularityCalculatorService
    {
        List<SOTagRequestItem> CalculateTagsPopulatiryPercentage(List<SOTagRequestItem> tagRequestItems);
    }
}
