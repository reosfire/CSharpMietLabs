using Foundation;
using Lab3.Models;
using Lab3.Models.Students;

namespace Lab3
{
    internal delegate TKey KeySelector<out TKey>(Student student);

    internal class StudentCollection<TKey> where TKey : notnull
    {
        private readonly Dictionary<TKey, Student> _data = new();
        private readonly KeySelector<TKey> _keySelector;

        public double MaxAverageMark
        {
            get
            {
                if (_data.Count == 0) return -1;
                return _data.Values.Max(it => it.AverageMark);
            }
        }

        public IEnumerable<IGrouping<Education, KeyValuePair<TKey, Student>>> GroupByEducation =>
            _data.GroupBy(it => it.Value.Education);

        public StudentCollection(KeySelector<TKey> keySelector)
        {
            _keySelector = keySelector;
        }

        private void Add(Student student) => _data.Add(_keySelector(student), student);

        public void AddStudents(params Student[] students)
        {
            foreach (Student student in students) 
                Add(student);
        }

        public void AddDefaults(int count, Func<Student> generator)
        {
            for (int i = 0; i < count; i++)
                Add(generator());
        }

        public IEnumerable<KeyValuePair<TKey, Student>> EducationForm(Education value) =>
            _data.Where(it => it.Value.Education == value);

        public override string ToString() =>
            $"{_data.ToStr("Students")}";
    }
}
