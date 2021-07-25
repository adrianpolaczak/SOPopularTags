using System;
using System.Linq;
using System.Threading.Tasks;
using SOPopularTags.Domain.Models;

namespace SOPopularTags.Domain.Interfaces
{
    public interface ISOTagRequestRepository
    {
        IQueryable<SOTagRequest> GetSOTagRequests();
        Task UpdateSOTagRequest(SOTagRequest sOTagRequest);
    }
}
