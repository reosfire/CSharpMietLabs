using Foundation;
using System.Collections;

namespace Lab3.Models.Students
{
    internal class Student : Person, IEnumerable<string>
    {
        private Education _education;
        private int _group;
        private List<Exam> _exams;
        private List<Test> _tests;

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
                if (value <= 100 || value >= 600) throw new ArgumentOutOfRangeException(nameof(Group), "Value must be in (100; 600)");
                _group = value;
            }
        }
        public List<Exam> Exams
        {
            get => _exams;
            set => _exams = value;
        }
        public List<Test> Tests
        {
            get => _tests;
            set => _tests = value;
        }

        public double AverageMark => _exams.Sum(it => (long)it.Mark) / (double)_exams.Count;

        public bool this[Education expected] => _education == expected;

        public Student(
            Person person,
            Education education,
            int group,
            List<Exam> exams,
            List<Test> test
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
            _exams = new List<Exam>();
            _tests = new List<Test>();
        }

        public void AddExams(params Exam[] exams) => _exams.AddRange(exams);
        public void AddTests(params Test[] tests) => _tests.AddRange(tests);

        public override string ToString() =>
            $"{base.ToString()}\n" +
            $"{Education.ToStr(nameof(Education))}\n" +
            $"{Group.ToStr(nameof(Group))}\n" +
            $"{AverageMark.ToStr(nameof(AverageMark))}\n" +
            $"{Exams.ToStr(nameof(Exams))}\n" +
            $"{Tests.ToStr(nameof(Tests))}";
        
        public override string ToShortString() => 
            $"{base.ToString()}\n" +
            $"{Education.ToStr(nameof(Education))}\n" +
            $"{Group.ToStr(nameof(Group))}\n" +
            $"{AverageMark.ToStr(nameof(AverageMark))}\n" +
            $"{Exams.Count.ToStr(nameof(Exams))}\n" +
            $"{Tests.Count.ToStr(nameof(Tests))}";

        public new Student DeepCopy() =>
            new(this,
                Education,
                Group,
                _exams.Select(it => (Exam)it.DeepCopy()).ToList(),
                _tests.Select(it => (Test)it.DeepCopy()).ToList());

        public override bool Equals(object? obj)
        {
            if (obj is not Student other) return false;
            return base.Equals(obj) &&
                Education == other.Education &&
                Group == other.Group &&
                Exams.SequenceEqual(other.Exams) &&
                Tests.SequenceEqual(other.Tests);
        }
        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(),
                Education,
                Group,
                Exams.CombinedHash(),
                Tests.CombinedHash());

        public IEnumerable<object> ExamsAndTests() =>
            Exams.Concat<object>(Tests);
        public IEnumerable<Exam> ExamsWithGreaterMark(int bound) =>
            Exams.Where(it => it.Mark > bound);

        private IEnumerable<Exam> PassedExams() => ExamsWithGreaterMark(2);
        private IEnumerable<Test> PassedTests() => Tests.Where(it => it.Passed);

        public IEnumerable<object> PassedExamsAndTests() =>
            PassedExams().Concat<object>(PassedTests());
        public IEnumerable<Test> PassedTestsWithPassedExams() =>
            PassedTests().Where(test =>
                PassedExams().Any(exam => test.Subject == exam.Subject)
            );

        public IEnumerator<string> GetEnumerator() => new StudentEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void SortExamsBySubject() => Exams.Sort();
        public void SortExamsByMark() => Exams.Sort(new Exam());
        public void SortExamsByDate() => Exams.Sort(new ExamDateComparer());

        public static bool operator ==(Student? a, Student? b)
        {
            if (a is null) return b is null;
            return a.Equals(b);
        }
        public static bool operator !=(Student? a, Student? b) => !(a == b);
    }
}
