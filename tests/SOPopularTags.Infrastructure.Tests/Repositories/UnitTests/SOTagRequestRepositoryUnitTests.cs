using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SOPopularTags.Domain.Models;
using SOPopularTags.Infrastructure.Repositories;
using Xunit;
using System.Linq;

namespace SOPopularTags.Infrastructure.Tests.Repositories.UnitTests
{
    public class SOTagRequestRepositoryUnitTests
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public SOTagRequestRepositoryUnitTests()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        }

        [Fact]
        public async Task AddSOTagRequest_AddsDataToDatabase()
        {
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

            using (var context = new AppDbContext(_options))
            {
                await context.Database.EnsureCreatedAsync();
                var sut = new SOTagRequestRepository(context);
                await sut.AddSOTagRequest(sOTagRequest);
                var result = await context.SOTagRequests.FindAsync(sOTagRequest.Id);
                Assert.NotNull(result);
                Assert.Equal(sOTagRequest, result);
            }
        }

        [Fact]
        public async Task GetSOTagRequest_ReturnsData()
        {
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

            using (var context = new AppDbContext(_options))
            {
                await context.Database.EnsureCreatedAsync();
                await context.SOTagRequests.AddAsync(sOTagRequest);
                await context.SaveChangesAsync();
                var sut = new SOTagRequestRepository(context);
                var result = await sut.GetSOTagRequest(sOTagRequest.Id);
                Assert.NotNull(result);
                Assert.Equal(sOTagRequest.Id, result.Id);
            }
        }

        [Fact]
        public async Task GetSOTagRequests_ReturnsQueryable()
        {
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
            var sOTagRequestQ = sOTagRequests.AsQueryable();
            
            using (var context = new AppDbContext(_options))
            {
                await context.Database.EnsureCreatedAsync();
                await context.SOTagRequests.AddAsync(sOTagRequest);
                await context.SaveChangesAsync();
                var sut = new SOTagRequestRepository(context);
                var result = sut.GetSOTagRequests();
                Assert.NotNull(result);
                Assert.Equal(sOTagRequestQ, result);
            }
        }

        [Fact]
        public async Task UpdateSOTagRequest_UpdatesData()
        {
            var sOTagRequest1 = new SOTagRequest
            {
                Id = 1,
                Items = new List<SOTagRequestItem>
                {
                    new SOTagRequestItem
                    {
                        Name = "mleko",
                        Count = 2
                    }
                }
            };
            var sOTagRequest2 = new SOTagRequest
            {
                Id = 1,
                Items = new List<SOTagRequestItem>
                {
                    new SOTagRequestItem
                    {
                        Name = "frytki",
                        Count = 24534567
                    }
                }
            };

            using (var context = new AppDbContext(_options))
            {
                await context.Database.EnsureCreatedAsync();
                await context.SOTagRequests.AddAsync(sOTagRequest1);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(_options))
            {
                await context.Database.EnsureCreatedAsync();
                var sut = new SOTagRequestRepository(context);
                await sut.UpdateSOTagRequest(sOTagRequest2);
                var result = await context.SOTagRequests.FindAsync(sOTagRequest2.Id);
                Assert.NotNull(result);
                Assert.Equal(sOTagRequest2, result);
            }
        }
    }
}
