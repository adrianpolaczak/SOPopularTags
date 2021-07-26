using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MockQueryable.Moq;
using Moq;
using SOPopularTags.Application.Services;
using SOPopularTags.Application.ViewModels;
using SOPopularTags.Domain.Interfaces;
using SOPopularTags.Domain.Models;
using Xunit;

namespace SOPopularTags.Application.Tests.Services.UnitTests
{
    public class HomeServiceUnitTests
    {
        private readonly HomeService _sut;
        private readonly Mock<ISOTagRequestRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        public HomeServiceUnitTests()
        {
            _sut = new HomeService(
                _repositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task GetPopularTags_ReturnsHomeVM()
        {
            // Ararnge
            var sOTagRequest = new SOTagRequest
            {
                Id = 1,
                Items = new List<SOTagRequestItem>
                {
                    new SOTagRequestItem
                    {
                        Name = "mleko",
                        Count = 2
                    },
                    new SOTagRequestItem
                    {
                        Name = "frytki",
                        Count = 10000000
                    }
                }
            };
            var sOTagRequests = new List<SOTagRequest> { sOTagRequest };
            var sOTagRequestQ = sOTagRequests.AsQueryable().BuildMock();

            var homeVM = new HomeVM
            {
                Items = new List<PopularTagForHomeVM>
                {
                    new PopularTagForHomeVM
                    {
                        Name = "mleko",
                        Count = 2
                    },
                    new PopularTagForHomeVM
                    {
                        Name = "frytki",
                        Count = 10000000
                    }
                }
            };

            _repositoryMock.Setup(s => s.GetSOTagRequests()).Returns(sOTagRequestQ.Object);

            _mapperMock.Setup(x => x.ConfigurationProvider).Returns(
                () => new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SOTagRequest, HomeVM>()
                    .ForMember(x => x.Items, y => y.MapFrom(src => src.Items));
                    cfg.CreateMap<SOTagRequestItem, PopularTagForHomeVM>();
                }));

            // Act
            var result = await _sut.GetPopularTagsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(sOTagRequest.Items[0].Name, homeVM.Items[0].Name);
            Assert.Equal(sOTagRequest.Items[1].Count, homeVM.Items[1].Count);
            Assert.Equal(sOTagRequest.Items.Count, homeVM.Items.Count);
        }
    }
}
