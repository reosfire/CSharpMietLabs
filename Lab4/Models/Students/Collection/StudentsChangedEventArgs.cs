using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Models.Students.Collection
{
    internal class StudentsChangedEventArgs<TKey> : EventArgs
    {
        public Action Action { get; set; }
        public string ChangedPropertyName { get; set; }
        public TKey Key { get; set; }

        public StudentsChangedEventArgs(Action action, string changedPropertyName, TKey key)
        {
            Action = action;
            ChangedPropertyName = changedPropertyName;
            Key = key;
        }

        public override string ToString() =>
            $"Action: {Action}\n" +
            $"ChangedProperty: {ChangedPropertyName}\n" +
            $"Key: \n{Key?.ToString()?.Tabulate() ?? "null"}";
    }
}
