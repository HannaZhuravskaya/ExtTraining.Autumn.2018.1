using System;
using System.Globalization;

namespace BookLibrary
{
    public class Book : IFormattable
    {
        public Book(string title, string author, int year, string publishHouse, string edition, int pages, decimal price)
        {
            Title = title;
            Author = author;
            Year = year;
            PublishingHouse = publishHouse;
            Edition = edition;
            Pages = pages;
            Price = price;
        }

        public string Title { get; private set; }

        public string Author { get; private set; }

        public int Year { get; private set; }

        public string PublishingHouse { get; private set; }

        public string Edition { get; private set; }

        public int Pages { get; private set; }

        public decimal Price { get; private set; }

        public override string ToString() => ToString("G", CultureInfo.InvariantCulture);

        public string ToString(string format) => ToString(format, CultureInfo.InvariantCulture);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (formatProvider != null)
            {
                ICustomFormatter formatter = formatProvider as ICustomFormatter;
                if (formatter != null)
                {
                    return formatter.Format(format, this, formatProvider);
                }
            }
            else
            {
                formatProvider = CultureInfo.InvariantCulture;
            }

            switch (format.ToUpper())
            {
                case "G":
                    return $"{Title}, {Author}, {Year}, {PublishingHouse}";
                case "TAP":
                    return $"{Title}, {Author}, {Price.ToString("C", formatProvider)}";
                case "TA":
                    return $"{Title}, {Author}";
                case "T":
                    return $"{Title}";
                case "TYPH":
                    return $"{Title}, {Year}, {PublishingHouse}";
                case "F":
                    return $"{Title}, {Author}, {Year}, {PublishingHouse}, {Edition}, {Pages.ToString()}, {Price.ToString("C", formatProvider)}";
                default:
                    throw new FormatException();
            }
        }
    }
}