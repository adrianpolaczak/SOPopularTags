using System;
using System.Threading.Tasks;
using SOPopularTags.Models;

namespace SOPopularTags.Interfaces
{
    public interface IHomeService
    {
        Task<HomeVM> GetPopularTags(int pageNumber);
    }
}
