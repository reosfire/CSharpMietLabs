using Foundation;
using Lab4.Models.Students.Collection;

namespace Lab4.Models.Students.Logging
{
    internal class Journal<TKey> where TKey : notnull
    {
        private readonly List<JournalEntry> _entries = new();

        public void On(StudentCollection<TKey> sender, StudentsChangedEventArgs<TKey> args)
        {
            _entries.Add(new JournalEntry(sender.Name, args.Action, args.ChangedProperty, args.Key.ToString() ?? "null"));
        }

        public override string ToString() =>
            _entries.ToStr("Journal entries");
    }
}
