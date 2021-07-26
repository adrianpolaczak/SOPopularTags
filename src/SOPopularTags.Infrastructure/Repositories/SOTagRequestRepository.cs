using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddSOTagRequest(SOTagRequest sOTagRequest)
        {
            await _appDbContext.SOTagRequests.AddAsync(sOTagRequest);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<SOTagRequest> GetSOTagRequest(int id)
        {
            return await _appDbContext.SOTagRequests.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
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
