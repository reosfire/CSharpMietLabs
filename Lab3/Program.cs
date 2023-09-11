using Foundation;
using Lab3.Models;
using Lab3.Models.Students;

namespace Lab3
{
    internal class Program: LabBase<Mocker>
    {
        // Summary:
        // Different sorting. IComparable IComparer
        // Use of List finally!!
        // Advanced Linq grouping
        // Collection to benchmark insertion to Dictionary and List
        private static void Main() => new Program().Run();

        private void Run()
        {
            RunCommented("1. Sorting", () =>
            {
                Student student = Mocker.MockStudent();
                student.AddExams(Mocker.MockArrayWith(Mocker.MockExam));

                Console.WriteLine("Initial: \n{0}", student.Exams.ToStringTabulated());

                student.SortExamsBySubject();
                Console.WriteLine("Sorted by subject: \n{0}", student.Exams.ToStringTabulated());

                student.SortExamsByMark();
                Console.WriteLine("Sorted By mark: \n{0}", student.Exams.ToStringTabulated());

                student.SortExamsByDate();
                Console.WriteLine("Sorted By Date: \n{0}", student.Exams.ToStringTabulated());
            });

            StudentCollection<string> studentCollection = new(it => it.Name + it.Surname);

            RunCommented("2. StudentsCollection add", () =>
            {
                studentCollection.AddDefaults(4, Mocker.MockStudent);

                Console.WriteLine(studentCollection);
            });

            RunCommented("3. StudentsCollection operations", () =>
            {
                Console.WriteLine("Max average: {0}", studentCollection.MaxAverageMark);

                Console.WriteLine("Filtered by education form: ");
                Console.WriteLine(studentCollection.EducationForm(Education.Bachelor).ToStringTabulated());

                Console.WriteLine("Grouped: ");
                foreach (var grouping in studentCollection.GroupByEducation)
                {
                    Console.WriteLine((grouping.Key + ":\n" + grouping.ToStringTabulated()).Tabulate());
                }
            });

            RunCommented("4. Test collection", () =>
            {
                int count;
                while (true)
                {
                    Console.Write("Enter number of elements in collections to test: ");
                    count = ReadInt();
                    if (count > 0) break;
                    Console.WriteLine("Please enter number greater than zero!!");
                }

                KeyValuePair<Person, Student> Generator(int i)
                {
                    Person person = new($"name: {i}", $"surname: {i}", DateTime.MinValue);
                    return new KeyValuePair<Person, Student>(person, Mocker.MockStudent());
                }
                
                TestCollection<Person, Student> testCollection = new(count, Generator);

                testCollection.RunAllTests();
            });
        }
    }
}