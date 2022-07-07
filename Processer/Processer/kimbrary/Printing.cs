namespace Kimbrary.Printing
{
    public static class Print
    {
        public static void AsStriped(string text, ConsoleColor firstColor, ConsoleColor secondColor, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            ConsoleColor currentColor = firstColor;
            foreach (char character in text)
            {
                Console.ForegroundColor = currentColor;
                Console.Write(character);
                Console.ForegroundColor = resetColor;
                currentColor = (currentColor == firstColor ? secondColor : firstColor);
            }
        }

        public static void AsGreen(string text, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            WithColor(ConsoleColor.Green, text, resetColor);
        }

        public static void AsDarkGreen(string text, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            WithColor(ConsoleColor.DarkGreen, text, resetColor);
        }

        public static void AsYellow(string text, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            WithColor(ConsoleColor.Yellow, text, resetColor);
        }

        public static void AsDarkYellow(string text, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            WithColor(ConsoleColor.DarkYellow, text, resetColor);
        }

        public static void AsRed(string text, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            WithColor(ConsoleColor.Red, text, resetColor);
        }

        public static void AsDarkRed(string text, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            WithColor(ConsoleColor.DarkRed, text, resetColor);
        }

        public static void AsBlue(string text, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            WithColor(ConsoleColor.Blue, text, resetColor);
        }

        public static void AsDarkBlue(string text, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            WithColor(ConsoleColor.DarkBlue, text, resetColor);
        }

        public static void AsCyan(string text, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            WithColor(ConsoleColor.Cyan, text, resetColor);
        }

        public static void AsDarkCyan(string text, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            WithColor(ConsoleColor.DarkCyan, text, resetColor);
        }

        public static void AsMagenta(string text, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            WithColor(ConsoleColor.Magenta, text, resetColor);
        }

        public static void AsDarkMagenta(string text, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            WithColor(ConsoleColor.DarkMagenta, text, resetColor);
        }

        public static void AsWhite(string text, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            WithColor(ConsoleColor.White, text, resetColor);
        }

        public static void AsGray(string text, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            WithColor(ConsoleColor.Gray, text, resetColor);
        }

        private static void WithColor(ConsoleColor color, string text, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = resetColor;
        }
    }

    public static class Read
    {
        public static string AsGreen(string additionalText = "", ConsoleColor resetColor = ConsoleColor.Gray)
        {
            return WithColor(ConsoleColor.Green, additionalText, resetColor);
        }

        public static string AsDarkGreen(string additionalText = "", ConsoleColor resetColor = ConsoleColor.Gray)
        {
            return WithColor(ConsoleColor.DarkGreen, additionalText, resetColor);
        }

        public static string AsYellow(string additionalText = "", bool endLine = true, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            return WithColor(ConsoleColor.Yellow, additionalText, resetColor);
        }

        public static string AsDarkYellow(string additionalText = "", ConsoleColor resetColor = ConsoleColor.Gray)
        {
            return WithColor(ConsoleColor.DarkYellow, additionalText, resetColor);
        }

        public static string AsRed(string additionalText = "", ConsoleColor resetColor = ConsoleColor.Gray)
        {
            return WithColor(ConsoleColor.Red, additionalText, resetColor);
        }

        public static string AsDarkRed(string additionalText = "", ConsoleColor resetColor = ConsoleColor.Gray)
        {
            return WithColor(ConsoleColor.DarkRed, additionalText, resetColor);
        }

        public static string AsBlue(string additionalText = "", ConsoleColor resetColor = ConsoleColor.Gray)
        {
            return WithColor(ConsoleColor.Blue, additionalText, resetColor);
        }

        public static string AsDarkBlue(string additionalText = "", ConsoleColor resetColor = ConsoleColor.Gray)
        {
            return WithColor(ConsoleColor.DarkBlue, additionalText, resetColor);
        }

        public static string AsCyan(string additionalText = "", ConsoleColor resetColor = ConsoleColor.Gray)
        {
            return WithColor(ConsoleColor.Cyan, additionalText, resetColor);
        }

        public static string AsDarkCyan(string additionalText = "", ConsoleColor resetColor = ConsoleColor.Gray)
        {
            return WithColor(ConsoleColor.DarkCyan, additionalText, resetColor);
        }

        public static string AsMagenta(string additionalText = "", ConsoleColor resetColor = ConsoleColor.Gray)
        {
            return WithColor(ConsoleColor.Magenta, additionalText, resetColor);
        }

        public static string AsDarkMagenta(string additionalText = "", ConsoleColor resetColor = ConsoleColor.Gray)
        {
            return WithColor(ConsoleColor.DarkMagenta, additionalText, resetColor);
        }

        public static string AsWhite(string additionalText = "", ConsoleColor resetColor = ConsoleColor.Gray)
        {
            return WithColor(ConsoleColor.White, additionalText, resetColor);
        }

        public static string AsGray(string additionalText = "", ConsoleColor resetColor = ConsoleColor.Gray)
        {
            return WithColor(ConsoleColor.Gray, additionalText, resetColor);
        }

        private static string WithColor(ConsoleColor color, string additionalText, ConsoleColor resetColor = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            string readText = Console.ReadLine() ?? "";
            Console.Write(additionalText);
            Console.ForegroundColor = resetColor;
            return readText;
        }
    }
}
