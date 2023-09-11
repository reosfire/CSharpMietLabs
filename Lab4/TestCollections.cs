using System.Text;
using Foundation;

namespace Lab4
{
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);

    internal class TestCollection<TKey, TValue> 
        where TKey : notnull
    {
        private readonly BenchmarkRunner _runner = new();

        private readonly List<TKey> _keysList = new();
        private readonly List<string> _stringsList = new();

        private readonly Dictionary<TKey, TValue> _keyedDictionary = new();
        private readonly Dictionary<string, TValue> _stringDictionary = new();

        private readonly (TKey, string)[] _searchedElements;

        public TestCollection(int count, GenerateElement<TKey, TValue> generator)
        {
            for (int i = 0; i < count; i++)
            {
                var generated = generator(i);

                _keysList.Add(generated.Key);
                _stringsList.Add(generated.Key.ToString()!);

                _keyedDictionary.Add(generated.Key, generated.Value);
                _stringDictionary.Add(generated.Key.ToString()!, generated.Value);
            }

            _searchedElements = new (TKey, string)[]
            {
                (generator(0).Key, "first element"),
                (generator(count / 2).Key, "center element"),
                (generator(count - 1).Key, "last element"),
                (generator(count).Key, "non-existed element")
            };
        }

        public void RunAllTests()
        {
            foreach (var(searchElement, searchElementLabel) in _searchedElements)
            {
                Console.WriteLine($"Tests for: {searchElementLabel}");

                StringBuilder elementSearchesResultBuilder = new StringBuilder();
                (TimeSpan, string)[] runResults = RunTestsFor(searchElement);
                for (int i = 0; i < runResults.Length; i++)
                {
                    TimeSpan runResult = runResults[i].Item1;
                    string runLabel = runResults[i].Item2;

                    elementSearchesResultBuilder.Append($"Time for {runLabel}: {runResult}\n");
                }

                Console.WriteLine(elementSearchesResultBuilder.ToString().Tabulate());
            }
        }

        private (TimeSpan, string)[] RunTestsFor(TKey key) => new []
        {
            (_runner.Run(() =>
            {
                _keysList.Contains(key);
            }), "item in list with keys"),
            (_runner.Run(() =>
            {
                _stringsList.Contains(key.ToString()!);
            }), "item in list with strings"),
            (_runner.Run(() =>
            {
                _keyedDictionary.ContainsKey(key);
            }), "key in dict with keys"),
            (_runner.Run(() =>
            {
                _stringDictionary.ContainsKey(key.ToString()!);
            }), "key in with string"),
            (_runner.Run(() =>
            {
                try
                {
                    _keyedDictionary.ContainsValue(_keyedDictionary[key]);
                }
                catch
                {
                    // ignored
                }
            }), "value in dict with keys"),
        };
    }
}
