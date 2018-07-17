using System;
using System.Collections.Generic;
using System.Linq;

namespace SterlingBankLMS.Core.Helper
{
    public static class CollectionHelper
    {
        private static readonly object _locker = new object();

        public static IEnumerable<T> PickRandomElements<T>(this IList<T> collection, int? pickCount=0)
        {
            var cachedCount = 0;
            if (collection == null || cachedCount >= (cachedCount = collection.Count())) {
                return new List<T>();
            }

            if (cachedCount <= pickCount) {
                return collection.AsEnumerable();
            }

            var newItemsList = new List<T> { };
            var _random = new Random();
            int index = 0;

            for (; ; )
            {
                lock (_locker) {
                    index = _random.Next(1, cachedCount);
                }

                var item = collection[index];

                if (!newItemsList.Contains(item))
                    newItemsList.Add(item);

                if (newItemsList.Count == pickCount)
                    return newItemsList;
            }
        }
    }
}