using Foundation;
using Lab1.Models;

namespace Lab1
{
    internal class Program: LabBase<Mocker>
    {
        // Summary:
        // classes, enums
        // Override toString
        // Create custom indexer
        // Use arrays
        // Benchmark arrays with different configurations
        private static void Main() => new Program().Run();

        private void Run()
        {
            Student student = new();

            RunCommented("1. Print default student", () =>
            {
                Console.WriteLine(student);
            });

            RunCommented("2. Indexer values", () =>
            {
                foreach (Education education in Enum.GetValues<Education>())
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

                Console.WriteLine(examsToAdd.ToStr("ExamsToAdd"));
                Console.WriteLine();

                student.AddExams(examsToAdd);

                Console.WriteLine(student);
            });

            RunCommented("5. Benchmarks", () =>
            {
                string DoSomeWorkWithExam(Exam exam) => exam.ToString();

                Console.Write("Введите длину массивов например 1000000: ");
                int size = int.Parse(Console.ReadLine()!);
                
                Console.WriteLine("Введите размеры прямоугольного массива в формате {nRows};{mColumns} например 800;1250");
                int[] sizes = Console.ReadLine()!.Split(';').Select(int.Parse).ToArray();

                Console.WriteLine("Generation started");
                Exam[] oneDimension = Mocker.MockExamsOneDimensional(size);
                Exam[,] rectangular = Mocker.MockExamsRectangular(sizes[0], sizes[1]);
                Exam[][] twoDimensionGeneralized = Mocker.MockExamsTwoDimensionalGeneralized(size, (int)Math.Round(Math.Sqrt(size)));
                Console.WriteLine("Generation completed");

                BenchmarkRunner benchmarkRunner = new();
                Console.WriteLine("One dimension: {0}", benchmarkRunner.Run(() =>
                {
                    foreach (Exam exam in oneDimension)
                    {
                        DoSomeWorkWithExam(exam);
                    }
                }));

                Console.WriteLine("Rectangular: {0}", benchmarkRunner.Run(() =>
                {
                    foreach (Exam exam in rectangular)
                    {
                        DoSomeWorkWithExam(exam);
                    }
                }));

                Console.WriteLine("Two dimension generalized: {0}", benchmarkRunner.Run(() =>
                {
                    foreach (Exam[] line in twoDimensionGeneralized)
                    {
                        foreach (Exam exam in line)
                        {
                            DoSomeWorkWithExam(exam);
                        }
                    }
                }));
            });
        }
    }
}