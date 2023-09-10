using System.Collections;

namespace Foundation
{
    public class SharedMocker
    {
        public string MockString(string prefix) => prefix + Guid.NewGuid().ToString();
        public int MockInt() => Random.Shared.Next();
        public int MockInt(int maxValue) => Random.Shared.Next(maxValue);
        public int MockInt(int minValue, int maxValue) => Random.Shared.Next(minValue, maxValue);
        public bool MockBool() => Random.Shared.Next(2) == 1;

        public DateTime MockDateTime() =>
            new DateTime(Random.Shared.NextInt64(DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks));

        public TEnum MockEnum<TEnum>() where TEnum : struct, Enum
        {
            TEnum[] values = Enum.GetValues<TEnum>();
            return values[Random.Shared.Next(values.Length)];
        }

        public T[] MockArrayWith<T>(Func<T> generator, int size)
        {
            T[] generated = new T[size];
            for (int i = 0; i < generated.Length; i++)
            {
                generated[i] = generator();
            }
            return generated;
        }
        public T[] MockArrayWith<T>(Func<T> generator, int minSize = 5, int maxSize = 10) =>
            MockArrayWith(generator, MockInt(minSize, maxSize));

        public ArrayList MockArrayListWith(Func<object> generator, int size)
        {
            ArrayList generated = new ArrayList(size);
            for (int i = 0; i < size; i++)
            {
                generated.Add(generator());
            }
            return generated;
        }
        public ArrayList MockArrayListWith(Func<object> generator, int minSize = 5, int maxSize = 10) =>
            MockArrayListWith(generator, MockInt(minSize, maxSize));


        public List<T> MockListWith<T>(Func<T> generator, int size)
        {
            List<T> generated = new List<T>(size);
            for (int i = 0; i < size; i++)
            {
                generated.Add(generator());
            }
            return generated;
        }
        public List<T> MockListWith<T>(Func<T> generator, int minSize = 5, int maxSize = 10) =>
            MockListWith(generator, MockInt(maxSize, minSize));
    }
}
