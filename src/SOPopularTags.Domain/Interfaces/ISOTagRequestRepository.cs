using System.Linq;
using System.Threading.Tasks;
using SOPopularTags.Domain.Models;

namespace SOPopularTags.Domain.Interfaces
{
    public interface ISOTagRequestRepository
    {
        Task AddSOTagRequest(SOTagRequest sOTagRequest);
        Task<SOTagRequest> GetSOTagRequest(int id);
        IQueryable<SOTagRequest> GetSOTagRequests();
        Task UpdateSOTagRequest(SOTagRequest sOTagRequest);
    }
}
