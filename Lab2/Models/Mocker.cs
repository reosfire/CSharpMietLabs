using System.Collections;
using Lab2.Models.Student;

namespace Lab2.Models
{
    internal static class Mocker
    {
        public static string MockString(string prefix) => prefix + Guid.NewGuid().ToString();
        public static int MockInt() => Random.Shared.Next();
        public static int MockInt(int maxValue) => Random.Shared.Next(maxValue);
        public static int MockInt(int minValue, int maxValue) => Random.Shared.Next(minValue, maxValue);
        public static bool MockBool() => Random.Shared.Next(2) == 1;
        public static DateTime MockDateTime() => 
            new DateTime(Random.Shared.NextInt64(DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks));

        public static Person MockPerson() => 
            new Person(
                MockString("[mock name] "),
                MockString("[mock surname] "),
                MockDateTime()
                );

        public static Exam MockExam() =>
            new Exam(
                MockString("[mock subject] "),
                MockInt(),
                MockDateTime()
                );

        public static Test MockTest() =>
            new Test(
                MockString("[mock subject] "),
                MockBool()
                );

        public static Education MockEducation()
        {
            Education[] educations = Enum.GetValues<Education>();
            return educations[Random.Shared.Next(educations.Length)];
        }

        public static T[] MockArrayWith<T>(Func<T> generator, int minSize = 5, int maxSize = 10)
        {
            T[] generated = new T[Random.Shared.Next(minSize, maxSize)];
            for (int i = 0; i < generated.Length; i++)
            {
                generated[i] = generator();
            }
            return generated;
        }

        public static ArrayList MockArrayListWith(Func<object> generator, int minSize = 5, int maxSize = 10)
        {
            int count = Random.Shared.Next(minSize, maxSize);
            ArrayList generated = new ArrayList(count);
            for (int i = 0; i < count; i++)
            {
                generated.Add(generator());
            }
            return generated;
        }

        public static Student.Student MockStudent() =>
            new Student.Student(
                MockPerson(),
                MockEducation(),
                MockInt(101, 599),
                MockArrayListWith(() => MockExam(), 2, 3),
                MockArrayListWith(() => MockTest(), 2, 3)
                );


        public static Exam[] MockExamsOneDimensional(int count)
        {
            Exam[] oneDimension = new Exam[count];
            for (int i = 0; i < count; i++)
            {
                oneDimension[i] = MockExam();
            }

            return oneDimension;
        }
        public static Exam[,] MockExamsRectangular(int n, int m)
        {
            Exam[,] rectangular = new Exam[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    rectangular[i, j] = MockExam();
                }
            }
            return rectangular;
        }

        public static Exam[][] MockExamsTwoDimensionalGeneralized(int totalCount, int subarraysCount)
        {
            Exam[][] twoDimensionGeneralized = new Exam[subarraysCount][];
            SortedSet<int> dividers = new SortedSet<int>();
            while (dividers.Count < subarraysCount - 1)
            {
                dividers.Add(MockInt(totalCount));
            }

            int k = 0;
            int previous = 0; 
            foreach (var divider in dividers)
            {
                twoDimensionGeneralized[k] = new Exam[divider - previous];
                previous = divider;
                k++;
            }
            twoDimensionGeneralized[k] = new Exam[totalCount - previous];

            for (int i = 0; i < twoDimensionGeneralized.Length; i++)
            {
                for(int j = 0; j < twoDimensionGeneralized[i].Length; j++)
                {
                    twoDimensionGeneralized[i][j] = MockExam();
                }
            }

            return twoDimensionGeneralized;
        }
    }
}
