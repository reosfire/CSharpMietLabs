using Foundation;

namespace Lab4.Models
{
    internal class Exam: IDateAndCopy, IComparable<Exam>, IComparer<Exam>
    {
        public string Subject { get; set; }
        public int Mark { get; set; }
        public DateTime Date { get; set; }

        public Exam(string subject, int mark, DateTime date)
        {
            Subject = subject;
            Mark = mark;
            Date = date;
        }

        public Exam()
        {
            Subject = "";
            Mark = 0;
            Date = DateTime.MinValue;
        }

        public override string ToString() =>
            $"{Subject.ToStr(nameof(Subject))}\n" +
            $"{Mark.ToStr(nameof(Mark))}\n" +
            $"{Date.ToStr(nameof(Date))}";

        public virtual object DeepCopy() => new Exam(Subject, Mark, Date);

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Exam other) return false;
            return Subject == other.Subject && Mark == other.Mark && Date == other.Date;
        }
        public override int GetHashCode() => HashCode.Combine(Subject, Mark, Date);

        public int CompareTo(Exam? other)
        {
            if (other is null) return -1;
            return Subject.CompareTo(other.Subject);
        }

        public int Compare(Exam? x, Exam? y)
        {
            if (x is null && y is null) return 0;
            if (x is null) return -1;
            if (y is null) return 1;
            return x.Mark.CompareTo(y.Mark);
        }

        public static bool operator ==(Exam a, Exam b)
        {
            if (a is null) return b is null;
            return a.Equals(b);
        }
        public static bool operator !=(Exam a, Exam b) => !(a == b);
    }
}
