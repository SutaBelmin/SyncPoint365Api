using System.Linq.Dynamic.Core;
using System.Text;

namespace SyncPoint365.Core.Helpers
{
    public static class LinqExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> src, string orderByString)
        {
            if (string.IsNullOrWhiteSpace(orderByString))
                return src;

            var orderParams = orderByString.Trim().Split(',');

            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams.Where(x => !string.IsNullOrEmpty(x)))
            {
                var trimmedParam = param.Trim();

                var propertyFromQueryName = trimmedParam.Split('|')[0];
                var direction = trimmedParam.EndsWith("desc", StringComparison.InvariantCultureIgnoreCase) ? "descending" : "ascending";

                orderQueryBuilder.Append($"{propertyFromQueryName} {direction},");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',');

            return !string.IsNullOrEmpty(orderQuery) ? src.OrderBy(orderQuery) : src;
        }
    }
}