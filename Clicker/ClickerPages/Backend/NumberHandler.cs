using System;
using System.Numerics;

namespace Clicker.ClickerPages.Backend
{
    internal class NumberHandler
    {
        public NumberHandler()
        {
        }

        // Converts a double to a readable string with suffixes (K, M, B, T, etc.)
        public string FormatLargeNumber(BigInteger number)
        {
            if (number == 0) return "0";

            string[] suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc" };
            int idx = 0;
            BigInteger n = number;
            while (n >= 1000 && idx < suffixes.Length - 1)
            {
                n /= 1000;
                idx++;
            }

            BigInteger divisor = BigInteger.Pow(1000, idx);
            BigInteger integerPart = number / divisor;
            BigInteger remainder = number % divisor;

            // First 3 decimal digits, truncated (change to rounded if preferred)
            BigInteger fracTimes1000 = (remainder * 1000) / divisor;

            if (fracTimes1000 == 0)
                return integerPart.ToString() + suffixes[idx];

            string frac = fracTimes1000.ToString().TrimEnd('0');
            return integerPart.ToString() + "." + frac + suffixes[idx];
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
