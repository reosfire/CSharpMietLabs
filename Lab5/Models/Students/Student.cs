﻿using System.Collections;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Foundation;

namespace Lab5.Models.Students
{
    [Serializable]
    internal class Student : Person, IEnumerable<string>, INotifyPropertyChanged
    {
        private Education _education;
        private int _group;
        private List<Exam> _exams;
        private List<Test> _tests;

        public event PropertyChangedEventHandler? PropertyChanged;

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
            set
            {
                if (_education != value)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Education)));
                    _education = value;
                }
            }
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
            _group = 101;
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

        public new Student DeepCopy()
        {
            IFormatter formatter = new BinaryFormatter();
            
            using MemoryStream memory = new();
            formatter.Serialize(memory, this);
            
            memory.Seek(0, SeekOrigin.Begin);
            return (Student)formatter.Deserialize(memory);
        }

        public bool Save(string fileName)
        {
            IFormatter formatter = new BinaryFormatter();

            FileStream? fileStream = null;
            try
            {
                fileStream = File.OpenWrite(fileName);
                formatter.Serialize(fileStream, this);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                fileStream?.Dispose();
            }
        }

        public bool Load(string fileName)
        {
            IFormatter formatter = new BinaryFormatter();
            
            FileStream? fileStream = null;
            try
            {
                fileStream = File.OpenRead(fileName);
                Student deserialized = (Student)formatter.Deserialize(fileStream);
                
                _name = deserialized._name;
                _surname = deserialized._surname;
                _birthday = deserialized._birthday;
                _education = deserialized._education;
                _group = deserialized._group;
                _exams = deserialized._exams;
                _tests = deserialized._tests;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                fileStream?.Dispose();
            }
        }

        public bool AddFromConsole()
        {
            Console.WriteLine("Enter exam data in following format: {subject};{mark};{date}");
            Console.WriteLine("Example: Calculus;5;20.06.2023");
            try
            {
                string input = Console.ReadLine()!;
                string[] tokens = input.Split(';');
                if (tokens.Length != 3) throw new Exception("There are must be 3 tokens");

                string subject = tokens[0];
                int mark = int.Parse(tokens[1]);
                DateTime date = DateTime.Parse(tokens[2]);
                
                AddExams(new Exam(subject, mark, date));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
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
