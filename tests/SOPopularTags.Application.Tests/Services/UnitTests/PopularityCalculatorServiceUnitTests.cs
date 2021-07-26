using System.Collections.Generic;
using SOPopularTags.Application.Services;
using SOPopularTags.Domain.Models;
using Xunit;

namespace SOPopularTags.Application.Tests.Services.UnitTests
{
    public class PopularityCalculatorServiceUnitTests
    {
        private readonly PopularityCalculatorService _sut;

        public PopularityCalculatorServiceUnitTests()
        {
            _sut = new PopularityCalculatorService();
        }

        [Fact]
        public void CalculateTagsPopulatiryPercentage_ReturnsListOfSOTagRequestItemWithCorrectlyCalculatedPopularityPercentages()
        {
            // Ararnge
            var sOTagRequestItems = new List<SOTagRequestItem>
            {
                new SOTagRequestItem
                {
                    Name = "mleko",
                    Count = 1
                },
                new SOTagRequestItem
                {
                    Name = "frytki",
                    Count = 3
                },
                new SOTagRequestItem
                {
                    Name = "kartofle",
                    Count = 6
                }
            };

            // Act
            var result = _sut.CalculateTagsPopulatiryPercentage(sOTagRequestItems);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10d, result[0].PopularityPercent);
            Assert.Equal(30d, result[1].PopularityPercent);
            Assert.Equal(60d, result[2].PopularityPercent);
        }
    }
}
