using System.Text;

namespace Lab2.Utils
{
    internal static class StringUtils
    {
        public static string Tabulate(this string s) => s.Tabulate(1);

        public static string Tabulate(this string s, int times) =>
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

        public static string ToStringTabulated<T>(this IEnumerable<T> collection) where T : notnull
        {
            return string.Join("\n\n", collection.Select(it => "   -" + it?.ToString()?.Tabulate(2)[4..]));
        }
    }
}
