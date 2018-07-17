using System;
using System.Collections.Generic;
using System.Linq;

namespace SterlingBankLMS.Web.Utilities.Extensions
{
    public static class CollectionExtensions
    {
        public static bool HasMorePage<T>(this IEnumerable<T> source, int page, int? pageSize)
        {
            if (pageSize == null)
                return false;

            if (source == null || !source.Any())
                return false;

            float maxPage = (float) source.Count() / pageSize.Value;
            return page < Math.Ceiling(maxPage);
        }

        public static bool HasMorePage(int total, int page, int? pageSize)
        {
            if (pageSize == null)
                return false;

            if (total == 0 || total < pageSize)
                return false;

            float maxPage = (float) total / pageSize.Value;
            return page < Math.Ceiling(maxPage);
        }
    }
}