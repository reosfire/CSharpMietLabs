using Foundation;

namespace Lab5.Models.Students;

internal static class StudentExtensions
{
    public static string ToStr(this Student value, string label) =>
        (value as object).ToStr(label);
}