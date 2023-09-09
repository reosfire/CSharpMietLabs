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

        protected static T ReadUserFriendly<T>(string retryMessage, Func<string?> reader, Func<string, T> parser)
        {
            while (true)
            {
                try
                {
                    return parser(reader()!);
                }
                catch (Exception)
                {
                    Console.WriteLine(retryMessage);
                }
            }
        }

        protected static T ReadUserFriendly<T>(string retryMessage, Func<string, T> parser) =>
            ReadUserFriendly<T>(retryMessage, Console.ReadLine, parser);

        protected static int ReadInt() => ReadUserFriendly("Enter an integer: ", int.Parse);
        protected static long ReadLong() => ReadUserFriendly("Enter an integer: ", long.Parse);
    }
}
