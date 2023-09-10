using Foundation;
using Lab3.Models.Students;

namespace Lab3.Models
{
    internal class Mocker: SharedMocker
    {
        public Person MockPerson() => 
            new Person(
                MockString("[mock name] "),
                MockString("[mock surname] "),
                MockDateTime());

        public Exam MockExam() =>
            new Exam(
                MockString("[mock subject] "),
                MockInt(),
                MockDateTime());

        public Test MockTest() =>
            new Test(
                MockString("[mock subject] "),
                MockBool());

        public Education MockEducation() =>
            MockEnum<Education>();

        public Student MockStudent() =>
            new Student(
                MockPerson(),
                MockEducation(),
                MockInt(101, 599),
                MockListWith(MockExam, 2, 4),
                MockListWith(MockTest, 2, 4));
    }
}
