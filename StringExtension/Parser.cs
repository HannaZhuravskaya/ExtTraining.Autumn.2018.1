using System;
using System.Collections.Generic;

namespace StringExtension
{
    /// <summary>
    /// Class contains parsing methods.
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Method transform number in string format to number in decimal format.
        /// </summary>
        /// <param name="source">
        /// String format number.
        /// </param>
        /// <param name="base">
        /// Number base.
        /// </param>
        /// <returns>
        /// Number in decimal format.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Wrong source number format.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Number is Null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Base should be between 2 and 16.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Number greater than int.MaxValue;
        /// </exception>
        public static int ToDecimal(this string source, int @base)
        {
            ToDecimalInputValidation(source, @base);

            var numberBits = new int[source.Length];

            var charToNumber = new Dictionary<char, int>()
            {
                {'0', 0},
                {'1', 1},
                {'2', 2},
                {'3', 3},
                {'4', 4},
                {'5', 5},
                {'6', 6},
                {'7', 7},
                {'8', 8},
                {'9', 9},
                {'A', 10},
                {'B', 11},
                {'C', 12},
                {'D', 13},
                {'E', 14},
                {'F', 15}
            };

            for (int i = 0; i < numberBits.Length; ++i)
            {
                if (!charToNumber.ContainsKey(char.ToUpper(source[i])))
                {
                    throw new ArgumentException();
                }

                numberBits[i] = charToNumber[char.ToUpper(source[i])];

                if (numberBits[i] >= @base)
                {
                    throw new ArgumentException();
                }
            }

            int resultNumber = 0;
            var currentBase = 1;
            for (int i = numberBits.Length - 1; i >= 0; --i)
            {
                CheckOverflowException(i, numberBits, @base, currentBase, resultNumber);

                resultNumber += numberBits[i] * currentBase;
                currentBase *= @base;
            }

            return resultNumber;
        }

        private static void ToDecimalInputValidation(string source, int @base)
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }

            if (@base < 2 || @base > 16)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private static void CheckOverflowException(
            int index, 
            int[] numberBits,
            int @base,
            int currentBase,
            int resultNumber)
        {
            if (index != numberBits.Length - 1)
            {
                if (
                    int.MaxValue / currentBase + 1 < @base ||
                    int.MaxValue - resultNumber < numberBits[index] * currentBase)
                {
                    throw new OverflowException();
                }
            }
        }
    }
}