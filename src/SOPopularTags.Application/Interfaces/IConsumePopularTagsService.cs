using System;
using System.Threading.Tasks;

namespace SOPopularTags.Application.Interfaces
{
    public interface IConsumePopularTagsService
    {
        Task ConsumePopularTags();
    }
}
