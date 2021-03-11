using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared
{
    public static class IEnumerableExtensions
    {
        public static TimeSpan Average<TSource>(this IEnumerable<TSource> source, Func<TSource, TimeSpan> selector)
        {
            var avgMs = source.Average(s => selector(s).TotalMilliseconds);
            return TimeSpan.FromMilliseconds(avgMs);
        }

        public static double StdDev<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            double ret = 0;
            int count = source.Count();
            if (count > 1)
            {
                //Compute the Average
                double avg = source.Average(selector);

                //Perform the Sum of (value-avg)^2
                double sum = source.Sum(d => (selector(d) - avg) * (selector(d) - avg));

                //Put it all together
                ret = Math.Sqrt(sum / count);
            }
            return ret;
        }

        public static TimeSpan StdDev<TSource>(this IEnumerable<TSource> source, Func<TSource, TimeSpan> selector)
        {
            var stdDevMs = source.StdDev(s => selector(s).TotalMilliseconds);
            return TimeSpan.FromMilliseconds(stdDevMs);
        }
    }
}
