using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Common
{
    public static class EnumerableExtensions
    {

        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> src,Action<T> action)
        {
            if (src == null) return src;
            foreach (var item in src)
            {
                action(item);

            }
            return src;
        }
        public static void AddRange<T>(this ICollection<T> src ,IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                src.Add(item);
            }
        }
    }
}
