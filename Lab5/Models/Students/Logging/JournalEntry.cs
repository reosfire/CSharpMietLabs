using Foundation;
using Action = Lab4.Models.Students.Collection.Action;

namespace Lab4.Models.Students.Logging
{
    internal class JournalEntry
    {
        public string CollectionName { get; }
        public Action Action { get; }
        public string ChangedProperty { get; }
        public string ChangedKey { get; }

        public JournalEntry(string collectionName, Action action, string changedProperty, string changedKey)
        {
            CollectionName = collectionName;
            Action = action;
            ChangedProperty = changedProperty;
            ChangedKey = changedKey;
        }

        public override string ToString() =>
            $"{CollectionName.ToStr(nameof(CollectionName))}\n" +
            $"{Action.ToStr(nameof(Action))}\n" +
            $"{ChangedProperty.ToStr(nameof(ChangedProperty))}\n" +
            $"{ChangedKey.ToStr(nameof(ChangedKey))}";
    }
}
