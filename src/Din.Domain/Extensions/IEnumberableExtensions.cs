using System.Collections.Generic;
using System.Linq;

namespace Din.Domain.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<ICollection<T>> Split<T>(this IEnumerable<T> collection, int numberOfCollections)
        {
            var source = collection.ToList();

            var rangeSize = source.Count / numberOfCollections;
            var additionalItems = source.Count % numberOfCollections;
            var index = 0;

            while (index < source.Count)
            {   
                var currentRangeSize = rangeSize + (additionalItems > 0 ? 1 : 0);
                yield return source.GetRange(index, currentRangeSize);
                index += currentRangeSize;
                additionalItems--;
            }
        }
    }
}
