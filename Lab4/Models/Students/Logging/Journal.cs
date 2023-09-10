using Foundation;
using Lab4.Models.Students.Collection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Models.Students.Logging
{
    internal class Journal<TKey> where TKey : notnull
    {
        private List<JournalEntry> _entries = new();

        public void On(StudentCollection<TKey> sender, StudentsChangedEventArgs<TKey> args)
        {
            _entries.Add(new(sender.Name, args.Action, args.ChangedPropertyName, args.Key.ToString() ?? "null"));
        }

        public override string ToString() =>
            _entries.ToStringTabulated();
    }
}
