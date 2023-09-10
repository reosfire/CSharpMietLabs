using System.Collections;

namespace Lab3.Models.Students
{
    internal class StudentEnumerator : IEnumerator<string>
    {
        private readonly IEnumerator<string> _examsSubjects;
        private readonly IEnumerable<string> _testsSubjects;
        public StudentEnumerator(Student student)
        {
            _examsSubjects = student.Exams.Select(it => it.Subject).GetEnumerator();
            _testsSubjects = student.Tests.Select(it => it.Subject);
        }

        public string Current => _examsSubjects.Current;
        object IEnumerator.Current => Current;
        public void Dispose() => _examsSubjects.Dispose();

        public bool MoveNext()
        {
            bool lastMoveNext;
            while (lastMoveNext = _examsSubjects.MoveNext())
            {
                if (_testsSubjects.Contains(_examsSubjects.Current)) break;
            }

            return lastMoveNext;
        }

        public void Reset() => _examsSubjects.Reset();
    }
}
