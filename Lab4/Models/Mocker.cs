using Lab4.Models.Students;
using System.Collections;

namespace Lab4.Models
{
    internal static class Mocker
    {
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

        public static Student MockStudent() =>
            new Student(
                MockPerson(),
                MockEducation(),
                MockInt(101, 599),
                MockListWith(MockExam, 2, 4),
                MockListWith(MockTest, 2, 4)
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
