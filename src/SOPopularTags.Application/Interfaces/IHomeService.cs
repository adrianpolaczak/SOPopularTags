using System.Threading.Tasks;
using SOPopularTags.Application.ViewModels;

namespace SOPopularTags.Application.Interfaces
{
    public interface IHomeService
    {
        Task<HomeVM> GetPopularTagsAsync();
    }
}
