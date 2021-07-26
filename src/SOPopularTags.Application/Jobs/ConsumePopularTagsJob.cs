using System.Threading.Tasks;
using Quartz;
using SOPopularTags.Application.Interfaces;


namespace SOPopularTags.Application.Jobs
{
    [DisallowConcurrentExecution]
    public class ConsumePopularTagsJob : IJob
    {
        private readonly IConsumePopularTagsService _consumePopularTagsService;

        public ConsumePopularTagsJob(IConsumePopularTagsService consumePopularTagsService)
        {
            _consumePopularTagsService = consumePopularTagsService;
        }

        // This is a method that executes based on given conditions thanks to Quertz.NET library
        // Check DependencyInjection.cs class in SOPopularTags.Application project to see condition details
        public async Task Execute(IJobExecutionContext context)
        {
            await _consumePopularTagsService.ConsumePopularTags();
        }
    }
}
