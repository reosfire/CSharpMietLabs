namespace Lab3.Models
{
    internal class Person: IDateAndCopy
    {
        protected string _name;
        protected string _surname;
        protected DateTime _birthday;

        public string Name => _name;
        public string Surname => _surname;
        public DateTime Birthday => _birthday;
        public DateTime Date
        {
            get => _birthday;
            set => _birthday = value;
        }
        public int BirthYear
        {
            get { return _birthday.Year; }
            set
            {
                _birthday = new DateTime(value, Birthday.Month, Birthday.Day);
            }
        }

        public Person(string name, string surname, DateTime birthday)
        {
            _name = name;
            _surname = surname;
            _birthday = birthday;
        }

        public Person()
        {
            _name = "";
            _surname = "";
            _birthday = DateTime.MinValue;
        }

        public override string ToString() => $"Name: {_name}\nSurname: {_surname}\nBirthday: {_birthday}";

        public virtual string ToShortString() => $"{_name} {_surname}";

        public virtual object DeepCopy() => new Person(Name, Surname, Birthday);

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Person other) return false;
            return Name == other.Name && Surname == other.Surname && Birthday == other.Birthday;
        }
        public override int GetHashCode() => HashCode.Combine(Name, Surname, Birthday);
        public static bool operator ==(Person a, Person b)
        {
            if (a is null) return b is null;
            return a.Equals(b);
        }
        public static bool operator !=(Person a, Person b) => !(a == b);
    }
}
