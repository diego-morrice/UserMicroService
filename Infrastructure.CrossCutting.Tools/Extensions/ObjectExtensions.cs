using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.CrossCutting.Tools.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        public static bool NotEquals(this object obj, object value)
        {
            return !obj.Equals(value);
        }

        public static bool In<T>(this T obj, params T[] values)
        {
            return values.IsNotNull() && values.Any(v => v.Equals(obj));
        }

        public static bool In<T>(this T obj, IEnumerable<T> values)
        {
            return values.IsNotNull() && values.Any(v => v.Equals(obj));
        }

        public static bool NotIn<T>(this T obj, params T[] values)
        {
            return values.IsNotNull() && (!values.Any(v => v.Equals(obj)));
        }

        public static bool NotIn<T>(this T obj, IEnumerable<T> values)
        {
            return values.IsNotNull() && (!values.Any(v => v.Equals(obj)));
        }
    }
}