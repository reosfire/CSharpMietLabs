using Foundation;
using System.ComponentModel;

namespace Lab4.Models.Students.Collection
{
    internal delegate TKey KeySelector<out TKey>(Student student);

    internal delegate void StudentsChangedHandler<TKey>(StudentCollection<TKey> source, StudentsChangedEventArgs<TKey> args)
        where TKey : notnull;

    internal class StudentCollection<TKey> where TKey : notnull
    {
        private Dictionary<TKey, Student> _data = new();
        private readonly KeySelector<TKey> _keySelector;

        public double MaxAverageMark
        {
            get
            {
                if (_data.Count == 0) return -1;
                return _data.Values.Max(it => it.AverageMark);
            }
        }

        public string Name { get; set; }
        public event StudentsChangedHandler<TKey>? StudentsChanged;
        private readonly Dictionary<TKey, PropertyChangedEventHandler> _propertyChangedHandlers = new();

        public IEnumerable<IGrouping<Education, KeyValuePair<TKey, Student>>> GroupByEducation =>
            _data.GroupBy(it => it.Value.Education);

        public StudentCollection(KeySelector<TKey> keySelector, string name)
        {
            _keySelector = keySelector;
            Name = name;
        }

        private void Add(Student student)
        {
            TKey key = _keySelector(student);
            _data.Add(key, student);
            StudentsChanged?.Invoke(this, new StudentsChangedEventArgs<TKey>(Action.Add, nameof(_data), key));

            void PropertyChangedHandler(object? sender, PropertyChangedEventArgs args)
            {
                StudentsChanged?.Invoke(this, new StudentsChangedEventArgs<TKey>(Action.Property, args.PropertyName!, key));
            }

            _propertyChangedHandlers[key] = PropertyChangedHandler;
            student.PropertyChanged += PropertyChangedHandler;
        }

        public void AddStudents(params Student[] students)
        {
            foreach (Student student in students)
                Add(student);
        }

        public void AddDefaults(int count, Func<Student> generator)
        {
            for (var i = 0; i < count; i++)
                Add(generator());
        }

        private bool Remove(TKey key, Student value)
        {
            if (_data.Remove(key))
            {
                StudentsChanged?.Invoke(this, new StudentsChangedEventArgs<TKey>(Action.Remove, nameof(_data), key));
                value.PropertyChanged -= _propertyChangedHandlers[key];
                _propertyChangedHandlers.Remove(key);
                return true;
            }
            return false;
        }
        public bool Remove(Student student)
        {
            foreach (var item in _data)
            {
                if (item.Value == student)
                {
                    return Remove(item.Key, item.Value);
                }
            }
            return false;
        }

        public IEnumerable<KeyValuePair<TKey, Student>> EducationForm(Education value) =>
            _data.Where(it => it.Value.Education == value);

        public override string ToString() =>
            $"{_data.ToStr("Students")}";
    }
}
