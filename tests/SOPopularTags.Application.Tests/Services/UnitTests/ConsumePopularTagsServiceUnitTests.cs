using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using SOPopularTags.Application.Interfaces;
using SOPopularTags.Application.Services;
using SOPopularTags.Domain.Interfaces;
using Xunit;
using SOPopularTags.Domain.Models;

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

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task ConsumePopularTags_WorksCorrectly(int id)
        {
            // Arrange
            var sOTagRequestItems = new List<SOTagRequestItem>
            {
                new SOTagRequestItem
                {
                    Name = "javascript",
                    Count = 2247654
                },
                new SOTagRequestItem
                {
                    Name = "java",
                    Count = 1789013
                }
            };

            var sOTagRequest = new SOTagRequest
            {
                Id = id
            };

            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""items"":[{""has_synonyms"":true,""is_moderator_only"":false,""is_required"":false,""count"":2247654,""name"":""javascript""},{""has_synonyms"":true,""is_moderator_only"":false,""is_required"":false,""count"":1789013,""name"":""java""}],""has_more"":true,""quota_max"":300,""quota_remaining"":73}"),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _popularityCalculatorService.Setup(s => s.CalculateTagsPopulatiryPercentage(It.IsAny<List<SOTagRequestItem>>())).Returns(sOTagRequestItems);

            _SOTagRequestRepository.Setup(s => s.GetSOTagRequest(It.IsAny<int>())).ReturnsAsync(sOTagRequest);

            // Act
            await _sut.ConsumePopularTags();

            // Assert
            if (id == 0)
            {
                _SOTagRequestRepository.Verify(s => s.AddSOTagRequest(It.IsAny<SOTagRequest>()), Times.Once);
            }
            if (id == 1)
            {
                _SOTagRequestRepository.Verify(s => s.UpdateSOTagRequest(It.IsAny<SOTagRequest>()), Times.Once);
            }
        }
    }
}
