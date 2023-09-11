using Foundation;

namespace Lab4.Models.Students.Collection
{
    internal class StudentsChangedEventArgs<TKey> : EventArgs
    {
        public Action Action { get; init; }
        public string ChangedProperty { get; init; }
        public TKey Key { get; init; }

        public StudentsChangedEventArgs(Action action, string changedProperty, TKey key)
        {
            Action = action;
            ChangedProperty = changedProperty;
            Key = key;
        }

        public override string ToString() =>
            $"{Action.ToStr(nameof(Action))}\n" +
            $"{ChangedProperty.ToStr(nameof(ChangedProperty))}\n" +
            $"{Key.ToStr(nameof(Key))}";
    }
}
