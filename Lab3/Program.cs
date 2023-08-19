using Foundation;
using Lab3.Models;
using Lab3.Models.Student;

namespace Lab3
{
    internal class Program: LabBase
	{
		static void Main()
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

            StudentCollection<string> studentCollection = new StudentCollection<string>(it => it.Name + it.Surname);

            RunCommented("2. StudentsCollection add", () =>
            {
                studentCollection.AddDefaults(4);

                Console.WriteLine(studentCollection);
            });

            RunCommented("3. StudentsCollection operations", () =>
            {
                Console.WriteLine("Max average: {0}", studentCollection.MaxAverageMark);

                Console.WriteLine("Filtered by education form: ");
                Console.WriteLine(studentCollection.EducationForm(Education.Bachelor).ToStringTabulated());

                Console.WriteLine("Groupped: ");
                foreach (var grouping in studentCollection.GroupByEducation)
                {
                    Console.WriteLine((grouping.Key + ":\n" + grouping.ToStringTabulated()).Tabulate());
                }
            });

            RunCommented("4. Test collection", () =>
            {
                int count = ReadInt();
                TestCollection<Person, Student> testCollection = new TestCollection<Person, Student>(count);
            });
        }
	}
}