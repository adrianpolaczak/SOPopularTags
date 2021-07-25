using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SOPopularTags.Application.Interfaces;
using SOPopularTags.Application.ViewModels;
using SOPopularTags.Domain.Interfaces;
using SOPopularTags.Domain.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using System.Collections.Generic;

namespace SOPopularTags.Application.Services
{
    public class HomeService : IHomeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISOTagRequestRepository _SOTagRequestRepository;
        private readonly IMapper _mapper;
        private readonly IPopularityCalculatorService _popularityCalculatorService;

        public HomeService(
            IHttpClientFactory httpClientFactory,
            ISOTagRequestRepository sOTagRequestRepository,
            IMapper mapper,
            IPopularityCalculatorService popularityCalculatorService)
        {
            _httpClientFactory = httpClientFactory;
            _SOTagRequestRepository = sOTagRequestRepository;
            _mapper = mapper;
            _popularityCalculatorService = popularityCalculatorService;
        }

        public async Task ConsumePopularTags()
        {
            var httpClient = _httpClientFactory.CreateClient("SO");
            var temp = new SOTagRequest();
            var sOTagRequest = new SOTagRequest
            {
                Items = new List<SOTagRequestItem>()
            };
            for (int i = 1; i <= 10; i++)
            {
                var response = await httpClient.GetStringAsync($"https://api.stackexchange.com/2.3/tags?page={i}&pagesize=100&order=desc&sort=popular&site=stackoverflow");
                temp = JsonConvert.DeserializeObject<SOTagRequest>(response);
                sOTagRequest.Items.AddRange(temp.Items);
            }
            sOTagRequest.Id = 1;
            var items = sOTagRequest.Items.ToList();
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Id = i + 1;
            }
            sOTagRequest.Items = _popularityCalculatorService.CalculateTagsPopulatiryPercentage(items);
            await _SOTagRequestRepository.UpdateSOTagRequest(sOTagRequest);
        }

        public async Task<HomeVM> GetPopularTags()
        {
            var homeVM = await _SOTagRequestRepository.GetSOTagRequests()
                .Where(x => x.Id == 1)
                    .ProjectTo<HomeVM>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
            homeVM.Items = homeVM.Items.OrderByDescending(x => x.Count).ToList();
            return homeVM;
        }
    }
}
