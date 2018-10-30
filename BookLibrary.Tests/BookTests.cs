using System;
using System.Collections;
using System.Globalization;
using NUnit.Framework;

namespace BookLibrary.Tests
{
    [TestFixture]
    public class BookTests
    {
        [TestCaseSource(typeof(DataSource), nameof(DataSource.StringWithoutParameters))]
        public string ToString_ToStringWithoutParameters_FormattedString(string title, string author, int year, string publishHouse,
            string edition, int pages, decimal price)
        {
            var book = new Book(title, author, year, publishHouse, edition, pages, price);
            return $"Book recorder: {book.ToString()}";
        }

        [TestCaseSource(typeof(DataSource), nameof(DataSource.StringWithFormat))]
        public string ToString_ToStringWithFormat_FormattedString(string title, string author, int year, string publishHouse,
            string edition, int pages, decimal price, string format)
        {
            var book = new Book(title, author, year, publishHouse, edition, pages, price);
            return $"Book recorder: {book.ToString(format)}";
        }

        [TestCaseSource(typeof(DataSource), nameof(DataSource.StringWithNotSupportedFormat))]
        public void ToString_ToStringWithNotSupportedFormat_ExpectedFormatException(string title, string author, int year, string publishHouse,
            string edition, int pages, decimal price, string format)
        {
            var book = new Book(title, author, year, publishHouse, edition, pages, price);
            Assert.Throws<FormatException>(() => book.ToString(format));
        }

        [TestCaseSource(typeof(DataSource), nameof(DataSource.StringWithFormat))]
        public string ToString_ToStringWithFormatAndNullProvider_FormattedString(string title, string author, int year,
            string publishHouse,
            string edition, int pages, decimal price, string format)
        {
            var book = new Book(title, author, year, publishHouse, edition, pages, price);
            return $"Book recorder: {book.ToString(format, null)}";
        }

        [TestCaseSource(typeof(DataSource), nameof(DataSource.StringWithFormatAndProvider))]
        public string ToString_ToStringWithFormatAndProvider_FormattedString(string title, string author, int year,
            string publishHouse,
            string edition, int pages, decimal price, string format, IFormatProvider formatProvider)
        { 
            var book = new Book(title, author, year, publishHouse, edition, pages, price);
            return $"Book recorder: {book.ToString(format, formatProvider)}";
        }
    }

    class DataSource
    {
        public static IEnumerable StringWithoutParameters
        {
            get
            {
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m)
                    .Returns("Book recorder: C# in Depth, Jon Skeet, 2019, Manning");
            }
        }

        public static IEnumerable StringWithFormat
        {
            get
            {
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "G")
                    .Returns("Book recorder: C# in Depth, Jon Skeet, 2019, Manning");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "g")
                    .Returns("Book recorder: C# in Depth, Jon Skeet, 2019, Manning");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "TAP")
                    .Returns("Book recorder: C# in Depth, Jon Skeet, ¤40.00");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "Tap")
                    .Returns("Book recorder: C# in Depth, Jon Skeet, ¤40.00");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "ta")
                    .Returns("Book recorder: C# in Depth, Jon Skeet");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "t")
                    .Returns("Book recorder: C# in Depth");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "TYPh")
                    .Returns("Book recorder: C# in Depth, 2019, Manning");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "F")
                    .Returns("Book recorder: C# in Depth, Jon Skeet, 2019, Manning, 4, 900, ¤40.00");
            }
        }

        public static IEnumerable StringWithFormatAndProvider
        {
            get
            {
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "F",
                        new CultureInfo("ru-RU"))
                    .Returns("Book recorder: C# in Depth, Jon Skeet, 2019, Manning, 4, 900, 40,00 ₽");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "F",
                        new CultureInfo("en-US"))
                    .Returns("Book recorder: C# in Depth, Jon Skeet, 2019, Manning, 4, 900, $40.00");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "F",
                        new CultureInfo("en-GB"))
                    .Returns("Book recorder: C# in Depth, Jon Skeet, 2019, Manning, 4, 900, £40.00");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "F",
                        new CultureInfo("dd"))
                    .Returns("Book recorder: C# in Depth, Jon Skeet, 2019, Manning, 4, 900, ¤40.00");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "F",
                        new CultureInfo("zh-Hant"))
                    .Returns("Book recorder: C# in Depth, Jon Skeet, 2019, Manning, 4, 900, HK$40.00");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "F",
                        new CultureInfo("zh-Hans"))
                    .Returns("Book recorder: C# in Depth, Jon Skeet, 2019, Manning, 4, 900, ¥40.00");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "F",
                        CultureInfo.InvariantCulture)
                    .Returns("Book recorder: C# in Depth, Jon Skeet, 2019, Manning, 4, 900, ¤40.00");
            }
        }

        public static IEnumerable StringWithNotSupportedFormat
        {
            get
            {
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "A");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "ATPG");
            }
        }

        public static IEnumerable Books
        {
            get { yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m); }
        }
    }
}