using Lab4.Models.Students.Collection;
using Action = Lab4.Models.Students.Collection.Action;

namespace Lab4.Models.Students.Logging
{
    internal class JournalEntry
    {
        public string CollectionName { get; set; }
        public Action Action { get; set; }
        public string ChangedProperty { get; set; }
        public string ChangedKey { get; set; }

        public JournalEntry(string collectionName, Action action, string changedProperty, string changedKey)
        {
            CollectionName = collectionName;
            Action = action;
            ChangedProperty = changedProperty;
            ChangedKey = changedKey;
        }

        public override string ToString() =>
            $"CollectionName: {CollectionName}\n" +
            $"Action: {Action}\n" +
            $"ChangedProperty: {ChangedProperty}\n" +
            $"ChangedKey: {ChangedKey}";
    }
}
