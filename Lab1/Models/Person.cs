using Foundation;

namespace Lab1.Models
{
    internal class Person
    {
        private readonly string _name;
        private readonly string _surname;
        private DateTime _birthday;

        public string Name => _name;
        public string Surname => _surname;
        public DateTime Birthday => _birthday;
        public int BirthYear
        {
            get => _birthday.Year;
            set => _birthday = new DateTime(value, Birthday.Month, Birthday.Day);
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

        public override string ToString() => 
            $"{Name.ToStr(nameof(Name))}\n" +
            $"{Surname.ToStr(nameof(Surname))}\n" +
            $"{Birthday.ToStr(nameof(Birthday))}";

        public virtual string ToShortString() =>
            $"{_name} {_surname}";
    }
}
