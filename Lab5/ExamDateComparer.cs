using Lab5.Models;

namespace Lab5
{
    internal class ExamDateComparer : IComparer<Exam>
    {
        public int Compare(Exam? x, Exam? y)
        {
            if (x is null && y is null) return 0;
            if (x is null) return -1;
            if (y is null) return 1;
            return x.Date.CompareTo(y.Date);
        }
    }
}
