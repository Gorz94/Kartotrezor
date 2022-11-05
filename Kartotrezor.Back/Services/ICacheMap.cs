using Kartotrezor.Back.Model;
using Microsoft.Extensions.Caching.Memory;

namespace Kartotrezor.Back.Services
{
    public interface ICacheMap
    {
        public MapExecution Get(string key);

        public void Remove(string key);
        public string Add(MapExecution execution);
    }

    public class MapCacher : ICacheMap
    {
        private readonly IMemoryCache _memoryCache;
        private static readonly object _locker = new object();

        public MapCacher(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public string Add(MapExecution execution)
        {
            lock (_locker)
            {
                var id = Guid.NewGuid().ToString();
                _memoryCache.Set(id, execution, TimeSpan.FromHours(1));
                return id;
            }
        }

        public MapExecution Get(string key)
        {
            lock (_locker)
            {
                return _memoryCache.Get<MapExecution>(key);
            }
        }

        public void Remove(string key)
        {
            lock (_locker)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
