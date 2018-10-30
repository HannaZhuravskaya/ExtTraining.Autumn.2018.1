using System;
using System.Collections;
using System.Globalization;
using BookLibrary;
using NUnit.Framework;

namespace BookExtension.Tests
{
    [TestFixture]
    public class BookFormatExtensionTests
    {
        [TestCaseSource(typeof(DataSource), nameof(DataSource.StringWithFormatAndClientProvider))]
        public string ToString_ToStringWithFormatAndClientProvider_FormattedString(
            string title, 
            string author, 
            int year,
            string publishHouse,
            string edition, 
            int pages, 
            decimal price, 
            string format, 
            IFormatProvider formatProvider)
        {
            var book = new Book(title, author, year, publishHouse, edition, pages, price);
            return $"Book recorder: {book.ToString(format, formatProvider)}";
        }
    }

    class DataSource
    {
        public static IEnumerable StringWithFormatAndClientProvider
        {
            get
            {
                // Book format
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "F",
                        new BookFormatExtension(new CultureInfo("ru-RU")))
                    .Returns("Book recorder: C# in Depth, Jon Skeet, 2019, Manning, 4, 900, 40,00 ₽");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "F",
                        new BookFormatExtension(new CultureInfo("en-US")))
                    .Returns("Book recorder: C# in Depth, Jon Skeet, 2019, Manning, 4, 900, $40.00");

                // BookFormatExtension format
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "A",
                        new BookFormatExtension(new CultureInfo("en-GB")))
                    .Returns("Book recorder: Jon Skeet");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "a",
                        new BookFormatExtension(new CultureInfo("dd")))
                    .Returns("Book recorder: Jon Skeet");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "atpg",
                        new BookFormatExtension(new CultureInfo("zh-Hant")))
                    .Returns("Book recorder: Jon Skeet, C# in Depth, 900");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "ATPg",
                        new BookFormatExtension(new CultureInfo("zh-Hans")))
                    .Returns("Book recorder: Jon Skeet, C# in Depth, 900");
                yield return new TestCaseData("C# in Depth", "Jon Skeet", 2019, "Manning", "4", 900, 40m, "atPg",
                        new BookFormatExtension(CultureInfo.InvariantCulture))
                    .Returns("Book recorder: Jon Skeet, C# in Depth, 900");
            }
        }
    }
}
