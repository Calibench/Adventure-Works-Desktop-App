using System;

namespace Clicker.ClickerPages.Backend
{
    internal class NumberHandler
    {
        public NumberHandler()
        {
        }

        // Converts a double to a readable string with suffixes (K, M, B, T, etc.)
        public string FormatLargeNumber(double number)
        {
            string[] suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc" };
            int suffixIndex = 0;
            while (number >= 1000 && suffixIndex < suffixes.Length - 1)
            {
                number /= 1000;
                suffixIndex++;
            }
            return $"{number:0.###}{suffixes[suffixIndex]}";
        }

        // Adds two large numbers represented as doubles
        public double Add(double a, double b)
        {
            return a + b;
        }

        // Multiplies two large numbers represented as doubles
        public double Multiply(double a, double b)
        {
            return a * b;
        }

        // Compares two large numbers
        public int Compare(double a, double b)
        {
            if (a > b) return 1;
            if (a < b) return -1;
            return 0;
        }

        // Converts a formatted string back to a double (e.g., "1.5M" -> 1500000)
        public double ParseLargeNumber(string formatted)
        {
            string[] suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc" };
            formatted = formatted.Trim();
            for (int i = suffixes.Length - 1; i >= 0; i--)
            {
                if (formatted.EndsWith(suffixes[i]))
                {
                    string numberPart = formatted.Substring(0, formatted.Length - suffixes[i].Length);
                    if (double.TryParse(numberPart, out double value))
                    {
                        return value * Math.Pow(1000, i);
                    }
                }
            }
            double.TryParse(formatted, out double result);
            return result;
        }
    }
}
