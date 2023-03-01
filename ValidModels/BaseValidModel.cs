using System.Collections.Generic;
using System.Linq;

namespace Extension.ValidModel
{
    public static class BaseValidModel
    {
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> value)
        {
            return value != null || value.Any();
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return value != null || value != "";
        }

        public static bool IsNotNullOrEmptyOrWhiteSpace(this string value)
        {
            return !(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value));
        }

        public static bool IsNotNull(this object value)
        {
            return value != null;
        }

        public static bool IsNull(this object value)
        {
            return value == null;
        }
    }
}