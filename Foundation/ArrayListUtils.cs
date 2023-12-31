﻿using System.Collections;

namespace Foundation
{
    public static class ArrayListUtils
    {
        public static ArrayList ToArrayList<T>(this IEnumerable<T> collection)
        {
            ArrayList result = new();
            foreach (T item in collection)
            {
                result.Add(item);
            }
            return result;
        }

        public static int CombinedHash(this ArrayList array)
        {
            if (array.Count == 0) return 0;

            int hash = array[0]?.GetHashCode() ?? -2;

            foreach (var item in array)
            {
                hash = HashCode.Combine(hash, item);
            }
            return hash;
        }
        public static int CombinedHash<T>(this ICollection<T> collection)
        {
            if (collection.Count == 0) return 0;

            int hash = collection.First()?.GetHashCode() ?? -2;

            foreach (T item in collection)
            {
                hash = HashCode.Combine(hash, item);
            }
            return hash;
        }
    }
}
