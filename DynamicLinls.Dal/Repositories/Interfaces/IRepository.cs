
namespace DynamicLinks.Dal.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<T> GetAsync(string shortLink);
        public Task<bool> Create(T obj);

        public Task<IEnumerable<T>> GetAll();

    }
}
