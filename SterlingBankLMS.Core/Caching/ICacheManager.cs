using System;

namespace Nop.Core.Caching
{
    public interface ICacheManager : IDisposable
    {
        T Get<T>(string key);

        void Set(string key, object data, int cacheTime);

        bool IsSet(string key);

        void Remove(string key);

        void RemoveByPattern(string pattern);

        void RemoveByPatternwithoutregex(string pattern);

        void Clear();
    }
}