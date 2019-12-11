using System;
using System.Linq;

namespace Naming.Task6
{
    public static class Formatter
    {
        private const string _plus = "+";
        private const string _pipe = "|";
        private const string _minus = "-";
        private const string _underscore = " _ ";

        public static void Main(string[] args)
        {
            Console.WriteLine(FormatKyeValue("enable", "true"));
            Console.WriteLine(FormatKyeValue("name", "Bob"));

            Console.Write("Press key...");
            Console.ReadKey();
        }

        private static string FormatKyeValue(string key, string value)
        {
            string content = key + _underscore + value;
            string minuses = Repeat(_minus, content.Length);
            return _plus + minuses + _plus + "\n" +
                   _pipe + content + _pipe + "\n" +
                   _plus + minuses + _plus + "\n";
        }

        private static string Repeat(string symbol, int times)
        {
            return string.Join(string.Empty, Enumerable.Repeat(symbol, times));
        }
    }
}
