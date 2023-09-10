using Foundation;

namespace Lab2.Models
{
    internal class Exam: IDateAndCopy
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
        public static bool operator ==(Exam a, Exam b)
        {
            if (a is null) return b is null;
            return a.Equals(b);
        }
        public static bool operator !=(Exam a, Exam b) => !(a == b);
    }
}
