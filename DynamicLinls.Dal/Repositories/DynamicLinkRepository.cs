using DynamicLinks.Dal.Repositories.Interfaces;
using DynamicLinks.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DynamicLinks.Dal.Repositories
{
    public class DynamicLinkRepository : IRepository<DynamicLinkEntity>
    {
        private readonly DynamicLinksDbContext _db;

        public DynamicLinkRepository(DynamicLinksDbContext db)
        {
            _db = db;
        }
        public async Task<DynamicLinkEntity> GetAsync(string shortLink)
        {
            //TODO обработка NULL из бд
            return await _db.DynamicLinks.FirstOrDefaultAsync(i => i.ShortLink == shortLink);
        }

        public async Task<bool> Create(DynamicLinkEntity link)
        {
            await _db.DynamicLinks.AddAsync(link);
            await _db.SaveChangesAsync();
            if (_db.DynamicLinks.Contains(link))
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<DynamicLinkEntity>> GetAll()
        {
            return await _db.DynamicLinks.ToListAsync();

        }
    }
}
