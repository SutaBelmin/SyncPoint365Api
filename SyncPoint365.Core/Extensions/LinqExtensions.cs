using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace SyncPoint365.Core.Helpers
{
    public static class LinqExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> src, string orderByString)
        {
            var orderParams = orderByString.Trim().Split(',');

            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams.Where(x => !string.IsNullOrEmpty(x)))
            {
                var trimmedParam = param.Trim();

                var propertyFromQueryName = trimmedParam.Split("|")[0];

                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith("desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name} {direction},");
            }

            var orderQuery = orderQueryBuilder.ToString().Trim(',', ' ');

            return src.OrderBy(orderQuery);
        }
    }
}