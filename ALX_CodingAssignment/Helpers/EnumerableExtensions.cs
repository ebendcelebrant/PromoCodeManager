using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALX_CodingAssignment.Domain.Validations;

namespace ALX_CodingAssignment.Helpers
{
    public static class EnumerableExtensions
    {
        public static void Do<S>(this IEnumerable<S> source, Action<S> action)
        {
            foreach (var entry in source)
            {
                action(entry);
            }
        }

        public static IEnumerable<T> ToEnumerable<T>(this T source)
        {
            Guard.IsNotNull(source, nameof(source));

            yield return source;
        }

        public static IOrderedEnumerable<T> Order<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, bool ascending)
        {
            if (ascending)
            {
                return source.OrderBy(selector);
            }
            else
            {
                return source.OrderByDescending(selector);
            }
        }
    }
}
