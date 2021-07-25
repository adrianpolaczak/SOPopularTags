using System.Linq;
using System.Threading.Tasks;
using SOPopularTags.Domain.Interfaces;
using SOPopularTags.Domain.Models;

namespace SOPopularTags.Infrastructure.Repositories
{
    public class SOTagRequestRepository : ISOTagRequestRepository
    {
        private readonly AppDbContext _appDbContext;

        public SOTagRequestRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IQueryable<SOTagRequest> GetSOTagRequests()
        {
            return _appDbContext.SOTagRequests.AsQueryable();
        }

        public async Task UpdateSOTagRequest(SOTagRequest sOTagRequest)
        {
            _appDbContext.SOTagRequests.Update(sOTagRequest);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
