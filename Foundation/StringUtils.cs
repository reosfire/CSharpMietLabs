using System.Text;

namespace Foundation
{
    public static class StringUtils
    {
        public static string Tabulate(this string s, int times = 1) => 
            string.Join('\n', s.Split('\n').Select(it => new string(' ', times * 3) + it));

        public static string Detabulate(this string s)
        {
            string[] lines = s.Split('\n');

            StringBuilder resultBuilder = new StringBuilder();

            foreach (var line in lines)
            {
                resultBuilder.Append(line.AssertStartsWithTab()[3..]);
            }
            return resultBuilder.ToString();
        }

        private static string AssertStartsWithTab(this string s)
        {
            if (s.Length < 3) throw new ArgumentException("Line shoudld be at least 3 characters long");
            if (s[0..2].Any(it => it != ' ')) throw new ArgumentException("Line shoudld starts wint 3 spaces");
            return s;
        }

        public static string ToStringTabulated<T>(this IEnumerable<T> collection) where T : notnull =>
            string.Join("\n\n", collection.Select(it => "   -" + it?.ToString()?.Tabulate(2)[4..]));

        public static string ToStringTabulated<K, V>(this IEnumerable<KeyValuePair<K, V>> collection) =>
            string.Join("\n\n", collection.Select(it => "   " + it.Key + ":\n" + it.Value?.ToString()?.Tabulate(2)));



        public static string ToStr(this string value, string label) =>
            $"{label}: {value}";
        public static string ToStr(this int value, string label) =>
            $"{label}: {value}";
        public static string ToStr(this long value, string label) =>
            $"{label}: {value}";
        public static string ToStr(this double value, string label) =>
            $"{label}: {value}";
        public static string ToStr(this float value, string label) =>
            $"{label}: {value}";
        public static string ToStr(this uint value, string label) =>
            $"{label}: {value}";
        public static string ToStr(this ulong value, string label) =>
            $"{label}: {value}";
        public static string ToStr(this bool value, string label) =>
            $"{label}: {value}";
        public static string ToStr(this Enum value, string label) =>
            $"{label}: {value}";
        public static string ToStr(this DateTime value, string label) =>
            $"{label}: {value}";

        public static string ToStr<T>(this IEnumerable<T> collection, string label) where T : notnull
        {
            string valueString = collection.ToStringTabulated();
            if (string.IsNullOrWhiteSpace(valueString)) return $"{label}: [ ]";
            return $"{label}:\n{valueString}";
        }
        public static string ToStr<K, V>(this IEnumerable<KeyValuePair<K, V>> collection, string label)
        {
            string valueString = collection.ToStringTabulated();
            if (string.IsNullOrWhiteSpace(valueString)) return $"{label}: {{ }}";
            return $"{label}:\n{valueString}";
        }

        public static string ToStr(this object? value, string label) =>
            $"{label}: \n{value?.ToString()?.Tabulate() ?? "null"}";
    }
}
