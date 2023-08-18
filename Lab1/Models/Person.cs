namespace Lab1.Models
{
    internal class Person
    {
        private string _name;
        private string _surname;
        private DateTime _birthday;

        public string Name => _name;
        public string Surname => _surname;
        public DateTime Birthday => _birthday;
        public int BirthYear
        {
            get { return _birthday.Year; }
            set
            {
                _birthday = new DateTime(value, Birthday.Month, Birthday.Day);
            }
        }

        public Person(string name, string surname, DateTime burthday)
        {
            _name = name;
            _surname = surname;
            _birthday = burthday;
        }

        public Person()
        {
            _name = "";
            _surname = "";
            _birthday = DateTime.MinValue;
        }

        public override string ToString() => $"Name: {_name}\nSurname: {_surname}\nBirthday: {_birthday}";

        public virtual string ToShortString() => $"{_name} {_surname}";
    }
}
