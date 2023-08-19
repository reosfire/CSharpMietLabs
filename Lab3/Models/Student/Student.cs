using Foundation;
using System.Collections;

namespace Lab3.Models.Student
{
    internal class Student : Person, IEnumerable<string>
    {
        private Education _education;
        private int _group;
        private ArrayList _exams;
        private ArrayList _tests;

        public Person Person
        {
            get => (Person)base.DeepCopy();
            set
            {
                _name = value.Name;
                _surname = value.Surname;
                _birthday = value.Birthday;
            }
        }
        public Education Education
        {
            get => _education;
            set => _education = value;
        }
        public int Group
        {
            get => _group;
            set
            {
                if (value <= 100 || value >= 600) throw new ArgumentOutOfRangeException("group must be in (100; 600)");
                _group = value;
            }
        }
        public ArrayList Exams
        {
            get => _exams;
            set => _exams = value;
        }
        public ArrayList Tests
        {
            get => _tests;
            set => _tests = value;
        }

        public double AverageMark => _exams.Cast<Exam>().Sum(it => (long)it.Mark) / (double)_exams.Count;

        public bool this[Education expected] => _education == expected;

        public Student(
            Person person,
            Education education,
            int group,
            ArrayList exams,
            ArrayList test
        ) : base(person.Name, person.Surname, person.Birthday)
        {
            _education = education;
            _group = group;
            _exams = exams;
            _tests = test;
        }

        public Student()
        {
            _education = Education.Bachelor;
            _group = 0;
            _exams = new ArrayList();
            _tests = new ArrayList();
        }

        public void AddExams(params Exam[] exams) => _exams.AddRange(exams);
        public void AddTests(params Test[] tests) => _tests.AddRange(tests);

        public override string ToString() => base.ToString() + "\n" +
            $"Education: {Education}\n" +
            $"Group: {Group}\n" +
            $"Exams: {(_exams.Count > 0 ? "\n" + _exams.ToArray().Cast<Exam>().ToStringTabulated() : "[ ]")}\n" +
            $"Tests: {(_tests.Count > 0 ? "\n" + _tests.ToArray().Cast<Test>().ToStringTabulated() : "[ ]")}";

        public override string ToShortString() => base.ToString() + "\n" +
            $"Education: {Education}\n" +
            $"Group: {Group}\n" +
            $"AverageMark: {AverageMark}";

        public new Student DeepCopy() =>
            new Student(this,
                Education,
                Group,
                _exams.Cast<Exam>().Select(it => it.DeepCopy()).ToArrayList(),
                _tests.Cast<Test>().Select(it => it.DeepCopy()).ToArrayList()
                );

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Student other) return false;
            return base.Equals(obj) &&
                Education == other.Education &&
                Group == other.Group &&
                Exams.Cast<Exam>().SequenceEqual(other.Exams.Cast<Exam>()) &&
                Tests.Cast<Exam>().SequenceEqual(other.Tests.Cast<Exam>());
        }
        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(),
                Education,
                Group,
                Exams.CombinedHash(),
                Tests.CombinedHash()
                );

        public IEnumerable<object> ExamsAndTests() =>
            Exams.Cast<object>().Concat(Tests.Cast<object>());
        public IEnumerable<Exam> ExamsWithGreaterMark(int bound) =>
            Exams.Cast<Exam>().Where(it => it.Mark > bound);

        private IEnumerable<object> PassedExams() => ExamsWithGreaterMark(2);
        private IEnumerable<object> PassedTests() =>
            Tests.Cast<Test>().Where(it => it.Passed);

        public IEnumerable<object> PassedExamsAndTests() =>
            PassedExams().Concat(PassedTests());
        public IEnumerable<Test> PassedTestsWithPassedExams() =>
            PassedTests().Cast<Test>().Where(test =>
                PassedExams().Cast<Exam>().Any(exam => test.Subject == exam.Subject)
            );

        public IEnumerator<string> GetEnumerator() => new StudentEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static bool operator ==(Student a, Student b)
        {
            if (a is null) return b is null;
            return a.Equals(b);
        }
        public static bool operator !=(Student a, Student b) => !(a == b);
    }
}
