using Foundation;

namespace Lab1.Models
{
    internal class Student
    {
        private Person _personalData;
        private Education _education;
        private int _group;
        private Exam[] _passedExams;

        public Person PersonalData
        {
            get => _personalData;
            set => _personalData = value;
        }
        public Education Education
        {
            get => _education;
            set => _education = value;
        }
        public int Group
        {
            get => _group;
            set => _group = value;
        }
        public Exam[] PassedExams
        {
            get => _passedExams;
            set => _passedExams = value;
        }

        public double AverageMark => _passedExams.Sum(it => (long)it.Mark) / (double)_passedExams.Length;

        public bool this[Education expected] => _education == expected;

        public Student(Person personalData, Education education, int group, Exam[] passedExams)
        {
            _personalData = personalData;
            _education = education;
            _group = group;
            _passedExams = passedExams;
        }

        public Student()
        {
            _personalData = new Person();
            _education = Education.Bachelor;
            _group = 0;
            _passedExams = Array.Empty<Exam>();
        }

        public void AddExams(Exam[] exams)
        {
            if (exams.Length == 0) return;

            Exam[] newExamsArray = new Exam[_passedExams.Length + exams.Length];

            _passedExams.CopyTo(newExamsArray, 0);
            exams.CopyTo(newExamsArray, _passedExams.Length);

            _passedExams = newExamsArray;
        }

        public override string ToString() => $"PersonalData: \n{PersonalData.ToString().Tabulate()}\n" +
            $"Education: {Education}\n" +
            $"Group: {Group}\n" +
            $"PassedExams: {(_passedExams.Length > 0 ? "\n" + _passedExams.ToStringTabulated() : "[ ]")}";

        public string ToShortString() => $"PersonalData: \n{PersonalData.ToString().Tabulate()}\n" +
            $"Education: {Education}\n" +
            $"Group: {Group}\n" +
            $"AverageMark: {AverageMark}";
    }
}
