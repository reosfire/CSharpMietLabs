namespace Foundation
{
    public class LabBase
    {
        protected static void RunCommented(string comment, Action action, int lenght = 100)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('=', lenght));
            string start = new string('=', lenght / 2 - (comment.Length / 2)) + comment;
            Console.WriteLine(start + new string('=', lenght - start.Length));
            Console.WriteLine(new string('=', lenght));
            Console.ForegroundColor = previousColor;

            action();
            Console.WriteLine();
        }
    }
}
