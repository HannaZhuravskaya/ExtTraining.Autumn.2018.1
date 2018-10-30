using System;

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
        /// source is null, empty or whitespace string.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// base should be between 2 and 16.
        /// </exception>
        public static int ToDecimal(this string source, int @base)
        {
            ToDecimalInputValidation(source, @base);

            var charToNumber = "0123456789ABCDEF".Substring(0, @base);

            int resultNumber = 0;
            var currentBase = 1;
            try
            {
                for (int i = source.Length - 1; i >= 0; --i)
                {
                    var currentDigit = charToNumber.IndexOf(char.ToUpper(source[i]));

                    if (currentDigit == -1)
                    {
                        throw new ArgumentException();
                    }

                    checked
                    {
                        resultNumber += currentDigit * currentBase;
                        if (i != 0)
                        {
                            currentBase *= @base;
                        } 
                    }
                }
            }
            catch (OverflowException)
            {
                throw new ArgumentException();
            }

            return resultNumber;
        }

        private static void ToDecimalInputValidation(string source, int @base)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentNullException();
            }

            if (@base < 2 || @base > 16)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}