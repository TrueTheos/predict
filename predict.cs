public class predict
{
    protected const ConsoleColor PREDICTED_COLOR = ConsoleColor.DarkGray;

    protected static string typed = "";

    protected static List<string> words = new()
    { 
        "test",
        "testing",
        "tester",
        "bump"
    };

    static void Main(string[] args)
    {
        while(true) 
        {
            Console.Write($"> ");
            CaptureInput();
        }
    }

    static void CaptureInput()
    {
        int lastAdditionLength = 0;
        string lastBestWord = "";
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }

            for (int i = 0; i < lastAdditionLength; i++) Console.Write("\b \b");

            if (keyInfo.Key == ConsoleKey.Backspace)
            {
                if (typed.Length > 0)
                {
                    typed = typed.Remove(typed.Length - 1);
                    Console.Write("\b \b");
                }
            }
            else
            {
                typed += keyInfo.KeyChar;
                Console.Write(keyInfo.KeyChar);
            }

            if (typed.Length < 1) continue;

            string bestWord = FindBestWord();
            if (!string.IsNullOrEmpty(bestWord))
            {
                lastBestWord = bestWord;
                string commonPrefix = new string(typed.TakeWhile((c, i) => i < bestWord.Length && c == bestWord[i]).ToArray());
                Console.ForegroundColor = PREDICTED_COLOR;
                string suffix = bestWord[commonPrefix.Length..];
                Console.Write(suffix);
                Console.ResetColor();
                lastAdditionLength = suffix.Length;
            }
            else
            {
                lastAdditionLength = 0;
            }
        }

        Console.WriteLine($"You typed: {lastBestWord}");
        typed = "";
    }

    static string FindBestWord()
    {
        return words.Where(x => x.StartsWith(typed, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Length)
                    .FirstOrDefault();
    }
}
