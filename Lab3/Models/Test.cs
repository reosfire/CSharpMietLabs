using Foundation;

namespace Lab3.Models
{
    internal class Test
    {
        public string Subject { get; set; }
        public bool Passed { get; set; }

        public Test(string subject, bool passed)
        {
            Subject = subject;
            Passed = passed;
        }
        public Test()
        {
            Subject = "";
            Passed = false;
        }

        public override string ToString() =>
            $"{Subject.ToStr(nameof(Subject))}\n" +
            $"{Passed.ToStr(nameof(Passed))}";

        public object DeepCopy() => new Test(Subject, Passed);
    }
}
