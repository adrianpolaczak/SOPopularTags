using System;
using Microsoft.EntityFrameworkCore;
using SOPopularTags.Domain.Models;

namespace SOPopularTags.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<SOTagRequest> SOTagRequests { get; set; }
        public DbSet<SOTagRequestItem> SOTagRequestItems { get; set; }
    }
}
