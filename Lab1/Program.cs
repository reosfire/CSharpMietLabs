using Foundation;
using Lab1.Models;

namespace Lab1
{
    internal class Program: LabBase<Mocker>
    {
        static void Main() => new Program().Run();

        private void Run()
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
                student.PersonalData = mocker.MockPerson();
                student.Education = mocker.MockEducation();
                student.Group = mocker.MockInt();
                student.PassedExams = mocker.MockArrayWith(() => mocker.MockExam(), 2, 3);

                Console.WriteLine(student);
            });

            RunCommented("4. AddExams", () =>
            {
                Exam[] examsToAdd = mocker.MockArrayWith(() => mocker.MockExam(), 2, 3);

                Console.WriteLine(examsToAdd.ToStr("ExamsToAdd"));
                Console.WriteLine();

                student.AddExams(examsToAdd);

                Console.WriteLine(student);
            });

            RunCommented("5. Benchmarks", () =>
            {
                string DoSomeWorkWithExam(Exam exam) => exam.ToString();

                int size = 100000;

                Exam[] oneDimension = mocker.MockExamsOneDimensional(size);
                Exam[,] rectangular = mocker.MockExamsRectangular(160 * 5, 625 * 2);
                Exam[][] twoDimensionGeneralized = mocker.MockExamsTwoDimensionalGeneralized(size, (int)Math.Round(Math.Sqrt(size)));
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
                        for (int j = 0; j < 625; j++)
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
    }
}