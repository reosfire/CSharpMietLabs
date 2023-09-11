using Foundation;
using Lab5.Models.Students;

namespace Lab5.Models
{
    internal class Mocker : SharedMocker
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
            new(MockPerson(),
                MockEducation(),
                MockInt(101, 599),
                MockListWith(MockExam, 2, 4),
                MockListWith(MockTest, 2, 4));
    }
}
