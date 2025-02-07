using Microsoft.Extensions.Caching.Memory;
using Products.Application.Interfaces;

namespace Products.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Retrieves a cached item by its key.
        /// </summary>
        /// <typeparam name="T">The type of the cached item.</typeparam>
        /// <param name="key">The key of the cached item.</param>
        /// <returns>The cached item if found; otherwise, default value of T.</returns>
        public Task<T> GetCachedItemAsync<T>(string key)
        {
            _cache.TryGetValue(key, out T value);
            return Task.FromResult(value);
        }

        /// <summary>
        /// Sets a cached item with a specified key and expiration time.
        /// </summary>
        /// <typeparam name="T">The type of the item to cache.</typeparam>
        /// <param name="key">The key of the cached item.</param>
        /// <param name="value">The item to cache.</param>
        /// <param name="expirationTime">The expiration time of the cached item.</param>
        public Task SetCachedItemAsync<T>(string key, T value, TimeSpan expirationTime)
        {
            _cache.Set(key, value, expirationTime);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Removes a cached item by its key.
        /// </summary>
        /// <param name="key">The key of the cached item to remove.</param>
        public Task RemoveCachedItemAsync(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }
    }
}
