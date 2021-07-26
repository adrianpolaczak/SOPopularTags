using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using SOPopularTags.Application.Interfaces;
using SOPopularTags.Application.Jobs;
using SOPopularTags.Application.Services;
using SOPopularTags.Domain.Interfaces;
using Xunit;

namespace SOPopularTags.Application.Tests.Jobs.UnitTests
{
    public class ConsumePopularTagsServiceUnitTests
    {
        private readonly ConsumePopularTagsService _sut;
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new();
        private readonly Mock<ISOTagRequestRepository> _SOTagRequestRepository = new();
        private readonly Mock<IPopularityCalculatorService> _popularityCalculatorService = new();

        public ConsumePopularTagsServiceUnitTests()
        {
            _sut = new ConsumePopularTagsService(
                _httpClientFactory.Object,
                _SOTagRequestRepository.Object,
                _popularityCalculatorService.Object
            );
        }

        [Fact]
        public async Task Execute_WorksCorrectly()
        {
            // Arrange

            // Act 

            // Assert
        }
    }
}
