using System.Linq;
using System.IO;
using System.Text;

namespace Kimbrary.Conditions
{
    public static class Conditions
    {
        public static bool IsTrueAny(params bool?[] items)
        {
            return items.Contains(true);
        }

        public static bool IsFalseAny(params bool?[] items)
        {
            return items.Contains(false);
        }

        public static bool IsNullOrTrueAny(params bool?[] items)
        {
            return items.Contains(null) || items.Contains(true);
        }

        public static bool IsNullOrFalseAny(params bool?[] items)
        {
            return items.Contains(null) || items.Contains(false);
        }

        public static bool IsNullAny<T>(params T?[] items)
        {
            foreach (var item in items)
            {
                if (item == null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}