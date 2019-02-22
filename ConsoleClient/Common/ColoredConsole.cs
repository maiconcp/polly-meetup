using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ConsoleClient
{
    public static class ColoredConsole
    {
        public static void WriteRed(string text)
        {
            WriteLine(text, ConsoleColor.Red);
        }

        public static void WriteGreen(string text)
        {
            WriteLine(text, ConsoleColor.Green);
        }

        public static void WriteBlue(string text)
        {
            WriteLine(text, ConsoleColor.Blue);
        }

        public static void WriteWhite(string text)
        {
            WriteLine(text, ConsoleColor.White);
        }

        public static void WriteLine(string text, ConsoleColor color)
        {
            var lastColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = lastColor;
        }
    }
}
