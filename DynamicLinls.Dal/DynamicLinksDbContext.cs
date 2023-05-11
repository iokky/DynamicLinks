using DynamicLinks.Domain;
using DynamicLinks.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DynamicLinks.Dal
{
    public class DynamicLinksDbContext: DbContext
    {
        public DbSet<DynamicLinkEntity> DynamicLinks { get; set; }
        public DynamicLinksDbContext(DbContextOptions<DynamicLinksDbContext> options): base(options)
        {
            
        }
    }
}