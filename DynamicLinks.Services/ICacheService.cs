namespace DynamicLinks.Services
{
    public interface ICacheService<T> where T : class
    {
        T Get (string key);
        bool Set (string key, T value);
        object Del(string key);
    }
}
