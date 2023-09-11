using Foundation;
using Lab4.Models;
using Lab4.Models.Students;
using Lab4.Models.Students.Collection;
using Lab4.Models.Students.Logging;

namespace Lab4
{
    internal class Program: LabBase<Mocker>
    {
        // Summary:
        // Events for logging
        private static void Main() => new Program().Run();

        private void Run()
        {
            StudentCollection<string> firstCollection = new(it => it.Name + it.Surname, "First collection");
            StudentCollection<string> secondCollection = new(it => it.Name + it.Surname, "Second collection");


            Journal<string> journal = new();
            firstCollection.StudentsChanged += journal.On;
            secondCollection.StudentsChanged += journal.On;


            Student[] students = Mocker.MockArrayWith(Mocker.MockStudent, 4, 4);
            for (int i = 0; i < students.Length; i++)
            {
                students[i].Person = new Person($"name: {i}", $"surname: {i}", DateTime.Now);
            }
            firstCollection.AddStudents(students[..(students.Length / 2)]);
            secondCollection.AddStudents(students[(students.Length / 2)..]);

            foreach (Student student in students)
                UpdateEducation(student);
            foreach (Student student in students)
                UpdateGroup(student);

            firstCollection.Remove(students[0]);

            UpdateEducation(students[0]);
            UpdateGroup(students[0]);

            Console.WriteLine(journal.ToString());
        }

        private static void UpdateEducation(Student student) =>
            student.Education = student.Education == Education.Bachelor ? Education.Specialist : Education.Bachelor;
        
        private static void UpdateGroup(Student student)
        {
            if (student.Group >= 599) student.Group = 101;
            else student.Group++;
        }
    }
}