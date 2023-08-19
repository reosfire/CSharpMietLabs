using System.Collections;

namespace Foundation
{
    public static class ArrayListUtils
    {
        public static ArrayList ToArrayList<T>(this IEnumerable<T> collection)
        {
            ArrayList result = new ArrayList();
            foreach (var item in collection)
            {
                result.Add(item);
            }
            return result;
        }

        public static int CombinedHash(this ArrayList array)
        {
            if (array == null) return -1;
            if (array.Count == 0) return 0;

            int hash = array[0]?.GetHashCode() ?? -2;

            foreach (var item in array)
            {
                hash = HashCode.Combine(hash, item);
            }
            return hash;
        }
    }
}
