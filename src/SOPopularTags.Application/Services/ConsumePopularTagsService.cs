using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SOPopularTags.Application.Interfaces;
using SOPopularTags.Domain.Interfaces;
using SOPopularTags.Domain.Models;

namespace SOPopularTags.Application.Services
{
    public class ConsumePopularTagsService : IConsumePopularTagsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISOTagRequestRepository _SOTagRequestRepository;
        private readonly IPopularityCalculatorService _popularityCalculatorService;

        public ConsumePopularTagsService(
            IHttpClientFactory httpClientFactory,
            ISOTagRequestRepository sOTagRequestRepository,
            IPopularityCalculatorService popularityCalculatorService)
        {
            _httpClientFactory = httpClientFactory;
            _SOTagRequestRepository = sOTagRequestRepository;
            _popularityCalculatorService = popularityCalculatorService;
        }

        public async Task ConsumePopularTags()
        {
            // Make http client factory to conusme api
            var httpClient = _httpClientFactory.CreateClient("SO");
            //var temp = new SOTagRequest();
            var sOTagRequest = new SOTagRequest { Items = new List<SOTagRequestItem>() };
            // Make 10 requests since SO api only permits to fetch 100 records in one request
            for (int i = 1; i <= 10; i++)
            {
                var response = await httpClient.GetAsync($"https://api.stackexchange.com/2.3/tags?page={i}&pagesize=100&order=desc&sort=popular&site=stackoverflow");
                var body = await response.Content.ReadAsStringAsync();
                var temp = JsonConvert.DeserializeObject<SOTagRequest>(body);
                sOTagRequest.Items.AddRange(temp.Items);
            }
            // Calculate popularity percentage of each element in given sample
            sOTagRequest.Items = _popularityCalculatorService.CalculateTagsPopulatiryPercentage(sOTagRequest.Items);
            // Check if record with id 1 already exists in database
            var req = await _SOTagRequestRepository.GetSOTagRequest(1);
            // If not, add new record
            if (req == null || req.Id != 1)
            {
                await _SOTagRequestRepository.AddSOTagRequest(sOTagRequest);
            }
            // Otherwise update existing record
            else
            {
                sOTagRequest.Id = 1;
                var items = sOTagRequest.Items.ToList();
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].Id = i + 1;
                }
                await _SOTagRequestRepository.UpdateSOTagRequest(sOTagRequest);
            }
        }
    }
}
