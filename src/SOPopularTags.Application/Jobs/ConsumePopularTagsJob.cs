using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Quartz;
using SOPopularTags.Application.Interfaces;
using SOPopularTags.Domain.Interfaces;
using SOPopularTags.Domain.Models;

namespace SOPopularTags.Application.Jobs
{
    public class ConsumePopularTagsJob : IJob
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISOTagRequestRepository _SOTagRequestRepository;
        private readonly IPopularityCalculatorService _popularityCalculatorService;

        public ConsumePopularTagsJob(
            IHttpClientFactory httpClientFactory,
            ISOTagRequestRepository sOTagRequestRepository,
            IPopularityCalculatorService popularityCalculatorService)
        {
            _httpClientFactory = httpClientFactory;
            _SOTagRequestRepository = sOTagRequestRepository;
            _popularityCalculatorService = popularityCalculatorService;
        }
        public async Task Execute(IJobExecutionContext context)
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
    }
}
