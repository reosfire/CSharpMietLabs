using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation
{
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);

    internal class TestCollection<TKey, TValue>
    {
        private List<TKey> _keys;
        private List<string> _a;

        public TestCollection(int count)
        {
            
        }
    }
}
