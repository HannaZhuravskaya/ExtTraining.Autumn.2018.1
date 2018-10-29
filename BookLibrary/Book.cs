using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public class Book
    {
        public readonly string Title;

        public readonly string Author;

        public readonly int Year;

        public readonly string PublishingHous;

        public readonly int Edition;

        public readonly int Pages;

        public readonly double Price;

        public Book(string title, string author, int year, string publishHous, int edition, int pages, double price)
        {
            Title = title;
            Author = author;
            Year = year;
            PublishingHous = publishHous;
            Edition = edition;
            Pages = pages;
            Price = price;
        }
    }
}
