using Foundation;
using Lab2.Models.Students;

namespace Lab2.Models
{
    internal class Mocker: SharedMocker
    {
        public Person MockPerson() => 
            new(MockString("[mock name] "), MockString("[mock surname] "), MockDateTime());

        public Exam MockExam() =>
            new(MockString("[mock subject] "), MockInt(), MockDateTime());

        public Test MockTest() =>
            new(MockString("[mock subject] "), MockBool());

        public Education MockEducation() =>
            MockEnum<Education>();

        public Student MockStudent() =>
            new (MockPerson(), MockEducation(), MockInt(101, 599), 
                MockArrayListWith(MockExam, 2, 3), MockArrayListWith(MockTest, 2, 3));
    }
}
