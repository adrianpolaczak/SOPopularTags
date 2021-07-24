using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SOPopularTags.Models;
using Newtonsoft.Json;
using SOPopularTags.Interfaces;

namespace SOPopularTags.Services
{
    public class HomeService : IHomeService
    {
        public async Task<HomeVM> GetPopularTags(int pageNumber)
        {
            var homeVM = new HomeVM();
            var clientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            using (var httpClient = new HttpClient(clientHandler))
            {
                var response = await httpClient.GetStringAsync($"https://api.stackexchange.com/2.3/tags?page={pageNumber}&pagesize=10&order=desc&sort=popular&site=stackoverflow");
                homeVM = JsonConvert.DeserializeObject<HomeVM>(response);
            }
            return homeVM;
        }
    }
}
