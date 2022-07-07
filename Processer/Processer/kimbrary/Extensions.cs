using System.Security.Cryptography;

namespace Kimbrary.Extensions
{
    public static class Extensions
    {
        public static int Next(this RandomNumberGenerator randomNumberGenerator, int minValue, int maxValue)
        {
            maxValue -= 1;

            byte[] randomBytes = new byte[sizeof(int)];
            randomNumberGenerator.GetNonZeroBytes(randomBytes);
            int randomInt = BitConverter.ToInt32(randomBytes);

            return ((randomInt - minValue) % (maxValue - minValue + 1) + (maxValue - minValue + 1)) % (maxValue - minValue + 1) + minValue;
        }
        
        public static int IndexOfAny(this string value, params string[] items)
        {
            foreach (var item in items)
            {
                if (value.Contains(item))
                {
                    return value.IndexOf(item);
                }
            }

            return -1;
        }

        public static T[] GetLatestElements<T>(this T[] array, int maxLimitOfElements)
        {
            List<T> latestElements = new();

            if (maxLimitOfElements >= 0)
            {
                for (int i = (array.Length > maxLimitOfElements ? array.Length - maxLimitOfElements : 0); i < array.Length; i++)
                {
                    latestElements.Add(array[i]);
                }
            }

            return latestElements.ToArray();
        }

        public static List<T> GetLatestElements<T>(this List<T>list, int maxLimitOfElements)
        {
            List<T> latestElements = new();

            if (maxLimitOfElements >= 0)
            {
                for (int i = (list.Count > maxLimitOfElements ? list.Count - maxLimitOfElements : 0); i < list.Count; i++)
                {
                    latestElements.Add(list[i]);
                }
            }

            return latestElements;
        }

        public static T[] GetOldestElements<T>(this T[] array, int maxLimitOfElements)
        {
            List<T> oldestElements = new();

            if (maxLimitOfElements >= 0)
            {
                for (int i = 0; i < (array.Length > maxLimitOfElements ? maxLimitOfElements : array.Length); i++)
                {
                    oldestElements.Add(array[i]);
                }
            }

            return oldestElements.ToArray();
        }

        public static List<T> GetOldestElements<T>(this List<T> list, int maxLimitOfElements)
        {
            List<T> oldestElements = new();

            if (maxLimitOfElements >= 0)
            {
                for (int i = 0; i < (list.Count > maxLimitOfElements ? maxLimitOfElements : list.Count); i++)
                {
                    oldestElements.Add(list[i]);
                }
            }

            return oldestElements;
        }

        public static string? GetPart(this string value, string item)
        {
            try
            {
                return value.Substring(value.IndexOf(item), item.Length);
            }
            catch
            {
                return null;
            }
        }
        
        public static bool ContainsAny(this string value, params string[] items)
        {
            foreach (string item in items)
            {
                if (value.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsAll(this string value, params string[] items)
        {
            if (items.Length > 0)
            {
                int equalItems = 0;

                foreach (string item in items)
                {
                    if (value.Contains(item))
                    {
                        equalItems++;
                    }
                }

                if (equalItems == items.Length)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsAny(this int value, params int[] items)
        {
            foreach (int item in items)
            {
                if (value.ToString().Contains(item.ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsAll(this int value, params int[] items)
        {
            if (items.Length > 0)
            {
                int equalItems = 0;

                foreach (int item in items)
                {
                    if (value.ToString().Contains(item.ToString()))
                    {
                        equalItems++;
                    }
                }

                if (equalItems == items.Length)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool EqualsAny<T>(this T value, params T[] items)
        {
            foreach (T item in items)
            {
                if (value.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool EqualsAny<T>(this T[] array, params T[] items)
        {
            foreach (T element in array)
            {
                foreach (T item in items)
                {
                    if (element.Equals(item))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool EqualsAll<T>(this T[] array, params T[] items)
        {
            if (items.Length > 0)
            {
                int equalItems = 0;

                foreach (T element in array)
                {
                    foreach (T item in items)
                    {
                        if (element != null && element.Equals(item))
                        {
                            equalItems++;
                        }
                    }
                }

                if (equalItems == items.Length)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
