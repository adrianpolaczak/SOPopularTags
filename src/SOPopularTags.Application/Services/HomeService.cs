using System.Linq;
using System.Threading.Tasks;
using SOPopularTags.Application.Interfaces;
using SOPopularTags.Application.ViewModels;
using SOPopularTags.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace SOPopularTags.Application.Services
{
    public class HomeService : IHomeService
    {
        private readonly ISOTagRequestRepository _SOTagRequestRepository;
        private readonly IMapper _mapper;

        public HomeService(
            ISOTagRequestRepository sOTagRequestRepository,
            IMapper mapper)
        {
            _SOTagRequestRepository = sOTagRequestRepository;
            _mapper = mapper;
        }

        public async Task<HomeVM> GetPopularTags()
        {
            // Get a record from database and map it to dto
            var homeVM = await _SOTagRequestRepository.GetSOTagRequests()
                .Where(x => x.Id == 1)
                    .ProjectTo<HomeVM>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
            // Order items here since they are not ordered when retrieved from api and saved to database
            homeVM.Items = homeVM.Items.OrderByDescending(x => x.Count).ToList();
            return homeVM;
        }
    }
}
