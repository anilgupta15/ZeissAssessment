
namespace Products.Application.Interfaces
{
    public interface ICacheService
    {
        /// <summary>
        /// Retrieves a cached item by its key.
        /// </summary>
        /// <typeparam name="T">The type of the cached item.</typeparam>
        /// <param name="key">The key of the cached item.</param>
        /// <returns>The cached item if found; otherwise, default value of T.</returns>
        Task<T> GetCachedItemAsync<T>(string key);

        /// <summary>
        /// Sets a cached item with a specified key and expiration time.
        /// </summary>
        /// <typeparam name="T">The type of the item to cache.</typeparam>
        /// <param name="key">The key of the cached item.</param>
        /// <param name="value">The item to cache.</param>
        /// <param name="expirationTime">The expiration time of the cached item.</param>
        Task SetCachedItemAsync<T>(string key, T value, TimeSpan expirationTime);

        /// <summary>
        /// Removes a cached item by its key.
        /// </summary>
        /// <param name="key">The key of the cached item to remove.</param>
        Task RemoveCachedItemAsync(string key);
    }
}
