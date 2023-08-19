﻿using Lab1.Models;

namespace Lab1
{
	internal class Program
	{
        private static void RunCommented(string comment, Action action, int lenght = 100)
		{
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('=', lenght));
            string start = new string('=', lenght / 2 - (comment.Length / 2)) + comment;
            Console.WriteLine(start + new string('=', lenght - start.Length));
            Console.WriteLine(new string('=', lenght));
            Console.ForegroundColor = previousColor;

			action();
			Console.WriteLine();
		}

		static void Main()
		{
            Student student = new Student();

			RunCommented("1. Print default student", () =>
			{
                Console.WriteLine(student);
            });

            RunCommented("2. Indexer values", () =>
            {
                foreach (var education in Enum.GetValues<Education>())
                {
                    Console.WriteLine($"{education,-16}| {student[education]}");
                }
            });

            RunCommented("3. Fill student", () =>
            {
                student.PersonalData = Mocker.MockPerson();
                student.Education = Mocker.MockEducation();
                student.Group = Mocker.MockInt();
                student.PassedExams = Mocker.MockArrayWith(() => Mocker.MockExam(), 2, 3);

                Console.WriteLine(student);
            });

            RunCommented("4. AddExams", () =>
            {
                Exam[] examsToAdd = Mocker.MockArrayWith(() => Mocker.MockExam(), 2, 3);

                Console.WriteLine("ExamsToAdd: ");
                Console.WriteLine(examsToAdd.ToStringTabulated());
                Console.WriteLine();

                student.AddExams(examsToAdd);

                Console.WriteLine(student);
            });

            RunCommented("5. Benchmarks", () =>
            {
                int size = 100000;

                Exam[] oneDimension = Mocker.MockExamsOneDimensional(size);
                Exam[,] rectangular = Mocker.MockExamsRectangular(160*5, 625*2);
                Exam[][] twoDimensionGeneralized = Mocker.MockExamsTwoDimensionalGeneralized(size, (int)Math.Round(Math.Sqrt(size)));
                Console.WriteLine("Generation completed");

                BenchmarkRunner benchmarkRunner = new BenchmarkRunner();
                Console.WriteLine("One dimension: {0}", benchmarkRunner.Run(() =>
                {
                    for (int i = 0; i < oneDimension.Length; i++)
                    {
                        DoSomeWorkWithExam(oneDimension[i]);
                    }
                }));

                Console.WriteLine("Rectangular: {0}", benchmarkRunner.Run(() =>
                {
                    for (int i = 0; i < 160; i++)
                    {
                        for(int j = 0; j < 625; j++)
                        {
                            DoSomeWorkWithExam(rectangular[i, j]);
                        }
                    }
                }));

                Console.WriteLine("Two dimension generalized: {0}", benchmarkRunner.Run(() =>
                {
                    for (int i = 0; i < twoDimensionGeneralized.Length; i++)
                    {
                        for (int j = 0; j < twoDimensionGeneralized[i].Length; j++)
                        {
                            DoSomeWorkWithExam(twoDimensionGeneralized[i][j]);
                        }
                    }
                }));
            });
		}

        private static string DoSomeWorkWithExam(Exam exam)
        {
            return exam.ToString();
        }
	}
}