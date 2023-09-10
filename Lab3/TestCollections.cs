using Foundation;
using System.Text;

namespace Lab3
{
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);

    internal class TestCollection<TKey, TValue>
    {
        private readonly BenchmarkRunner _runner = new();

        private List<TKey> _keysList = new();
        private List<string> _stringsList = new();

        private Dictionary<TKey, TValue> _keyedDictionary = new();
        private Dictionary<string, TValue> _stringDictionary = new();

        private (TKey, string)[] _searchedElements;

        public TestCollection(int count, GenerateElement<TKey, TValue> generatror)
        {
            for (int i = 0; i < count; i++)
            {
                var generated = generatror(i);

                _keysList.Add(generated.Key);
                _stringsList.Add(generated.Key!.ToString()!);

                _keyedDictionary.Add(generated.Key, generated.Value);
                _stringDictionary.Add(generated.Key.ToString()!, generated.Value);
            }

            _searchedElements = new (TKey, string)[]
            {
                (generatror(0).Key, "first element"),
                (generatror(count / 2).Key, "center element"),
                (generatror(count - 1).Key, "last element"),
                (generatror(count).Key, "unexisted element")
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

        private (TimeSpan, string)[] RunTestsFor(TKey key) => new (TimeSpan, string)[]
        {
            (_runner.Run(() =>
            {
                _keysList.Contains(key);
            }), "item in list with keys"),
            (_runner.Run(() =>
            {
                _stringsList.Contains(key!.ToString()!);
            }), "item in list with strings"),
            (_runner.Run(() =>
            {
                _keyedDictionary.ContainsKey(key);
            }), "key in dict with keys"),
            (_runner.Run(() =>
            {
                _stringDictionary.ContainsKey(key!.ToString()!);
            }), "key in with string"),
            (_runner.Run(() =>
            {
                try
                {
                    _keyedDictionary.ContainsValue(_keyedDictionary[key]);
                } 
                catch 
                {

                }
            }), "value in dict with keys"),
        };
    }
}
