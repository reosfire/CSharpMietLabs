using Foundation;

namespace Lab1.Models
{
    internal class Mocker: SharedMocker
    {
        public Person MockPerson() => 
            new(MockString("[mock name] "), MockString("[mock surname] "), MockDateTime());

        public Exam MockExam() =>
            new(MockString("[mock subject] "), MockInt(), MockDateTime());

        public Education MockEducation() =>
            MockEnum<Education>();

        public Exam[] MockExamsOneDimensional(int count)
        {
            Exam[] oneDimension = new Exam[count];
            for (int i = 0; i < count; i++)
            {
                oneDimension[i] = MockExam();
            }

            return oneDimension;
        }
        public Exam[,] MockExamsRectangular(int n, int m)
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
        public Exam[][] MockExamsTwoDimensionalGeneralized(int totalCount, int subarraysCount)
        {
            Exam[][] twoDimensionGeneralized = new Exam[subarraysCount][];
            SortedSet<int> dividers = new SortedSet<int>();
            while (dividers.Count < subarraysCount - 1)
            {
                dividers.Add(MockInt(totalCount));
            }

            int k = 0;
            int previous = 0; 
            foreach (int divider in dividers)
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
