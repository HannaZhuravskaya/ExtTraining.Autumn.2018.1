using System;
using System.Globalization;
using BookLibrary;

namespace BookExtension
{
    public class BookFormatExtension : IFormatProvider, ICustomFormatter
    {
        private readonly IFormatProvider _parent;

        public BookFormatExtension() : this(CultureInfo.InvariantCulture)
        {
        }

        public BookFormatExtension(IFormatProvider formatProvider)
        {
            _parent = formatProvider;
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(BookFormatExtension))
            {
                return this;
            }

            return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            var str = string.Format(_parent, format, arg);

            Book book = arg as Book;

            if (book == null)
            {
                throw new ArgumentException();
            }

            switch (format.ToUpper())
            {
                case "A":
                    return $"{book.Author}";
                case "ATPG":
                    return $"{book.Author}, {book.Title}, {book.Pages.ToString()}";

                // if it is not our format string, defer to the parent provider.
                default:
                    return book.ToString(format, _parent);
            }
        }
    }
}