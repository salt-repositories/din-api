using System.Collections.Generic;
using System.Linq;

namespace Din.Domain.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IList<T>> DivideByNumberOfThreads<T>(this IEnumerable<T> collection, int numberOfThreads)
        {
            var source = collection.ToList();

            var rangeSize = source.Count / numberOfThreads;
            var additionalItems = source.Count % numberOfThreads;
            var index = 0;

            while (index < source.Count)
            {   
                var currentRangeSize = rangeSize + ((additionalItems > 0) ? 1 : 0);
                yield return source.GetRange(index, currentRangeSize);
                index += currentRangeSize;
                additionalItems--;
            }
        }
    }
}
