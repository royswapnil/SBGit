using System;
using System.Linq;
using System.Runtime.Caching;

namespace Nop.Core.Caching
{

    public partial class MemoryCacheManager : ICacheManager
    {
   
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }
        public virtual T Get<T>(string key)
        {
            return (T)Cache[key];
        }

      
        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime)
            };
            Cache.Add(new CacheItem(key, data), policy);
        }

        public virtual bool IsSet(string key)
        {
            return (Cache.Contains(key));
        }

    
        public virtual void Remove(string key)
        {
            Cache.Remove(key);
        }


        public virtual void RemoveByPattern(string pattern)
        {
            this.RemoveByPattern(pattern, Cache.Select(p => p.Key));
        }

        public virtual void RemoveByPatternwithoutregex(string pattern)
        {
            this.RemoveByPatternwithoutregex(pattern, Cache.Select(p => p.Key));
        }


        public virtual void Clear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }

        public virtual void Dispose()
        {
        }
    }
}