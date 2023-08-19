﻿using Foundation;
using Lab2.Models;
using Lab2.Models.Student;

namespace Lab2
{
    internal class Program: LabBase
	{
		static void Main()
		{
			RunCommented("1. Equality and hashes", () =>
			{
                Student student1 = new Student();
                Student student2 = new Student();

                Console.WriteLine("Reference equals: {0}", ReferenceEquals(student1, student2));
                Console.WriteLine("Equals: {0}", student1 == student2);
                Console.WriteLine("First hash: {0}", student1.GetHashCode());
                Console.WriteLine("Second hash: {0}", student2.GetHashCode());
            });

            RunCommented("2. Fill student with exams and tests", () =>
            {
                Student student = new Student();

                student.AddExams(Mocker.MockArrayWith(() => Mocker.MockExam(), 2, 3));
                student.AddTests(Mocker.MockArrayWith(() => Mocker.MockTest(), 2, 3));

                Console.WriteLine(student);
            });

            RunCommented("3. Extract person from student", () =>
            {
                Student student = Mocker.MockStudent();

                Console.WriteLine(student.Person);
            });

            RunCommented("4. Deep copy test", () =>
            {
                Student student1 = Mocker.MockStudent();
                Student student2 = student1.DeepCopy();

                Console.WriteLine("Student 1:");
                Console.WriteLine(student1.ToString().Tabulate());

                Console.WriteLine("It's deep copy:");
                Console.WriteLine(student2.ToString().Tabulate());

                student2.Person = new Person("wtf", "wtf", DateTime.Now);
                student2.Date = DateTime.Now;
                student2.BirthYear = 2023;
                student2.Education = Education.Bachelor;
                student2.Group = 222;
                student2.AddExams(new Exam("C#", 10, DateTime.Now));
                student2.AddTests(new Test("Python", false));

                Console.WriteLine("Copy after changes:");
                Console.WriteLine(student2.ToString().Tabulate());

                Console.WriteLine("Student 1:");
                Console.WriteLine(student1.ToString().Tabulate());

            });

            RunCommented("5. Incorrect group", () =>
            {
                try
                {
                    new Student() 
                    { 
                        Group = 1,
                    };
                } 
                catch(Exception ex)
                {
                    Console.WriteLine("Exception ");
                    Console.WriteLine(ex.Message);
                }
            });

            RunCommented("6. All iterator", () =>
            {
                Student student = Mocker.MockStudent();
                Console.WriteLine(student);

                Console.WriteLine();
                Console.WriteLine("Iterator results:");
                foreach (var item in student.ExamsAndTests())
                {
                    Console.WriteLine(item.ToString().Tabulate());
                    Console.WriteLine();
                }
            });

            RunCommented("7. Exams iterator", () =>
            {
                Student student = Mocker.MockStudent();

                for (int i = 0; i < 10; i++)
                {
                    student.AddExams(new Exam(
                        Mocker.MockString("[mock subject] "),
                        Mocker.MockInt(6),
                        Mocker.MockDateTime()
                        ));
                }

                Console.WriteLine(student);

                Console.WriteLine();
                Console.WriteLine("Iterator results:");
                foreach (var item in student.ExamsWithGreaterMark(3))
                {
                    Console.WriteLine(item.ToString().Tabulate());
                    Console.WriteLine();
                }
            });



            Student student = Mocker.MockStudent();
            student.AddExams(
                new Exam("Subject 1", 3, DateTime.Now),
                new Exam("Subject 2", 2, DateTime.Now),
                new Exam("Subject 3", 4, DateTime.Now),
                new Exam("Subject 4", 5, DateTime.Now)
                );
            student.AddTests(
                new Test("Subject 1", false),
                new Test("Subject 3", true),
                new Test("Subject 10", true)
                );

            RunCommented("8. Itersecting subjects", () =>
            {
                foreach (string subject in student)
                {
                    Console.WriteLine($"{subject}");
                }
            });

            RunCommented("9. Passed exams and tests", () =>
            {
                foreach (var item in student.PassedExamsAndTests())
                {
                    Console.WriteLine(item.ToString().Tabulate());
                    Console.WriteLine();
                }
            });

            RunCommented("10. Subjects with passed test and exam", () =>
            {
                foreach (var item in student.PassedTestsWithPassedExams())
                {
                    Console.WriteLine(item.ToString().Tabulate());
                    Console.WriteLine();
                }
            });
        }
	}
}