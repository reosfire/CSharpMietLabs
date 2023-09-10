using Foundation;

namespace Lab1.Models
{
    internal class Exam
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
    }
}
