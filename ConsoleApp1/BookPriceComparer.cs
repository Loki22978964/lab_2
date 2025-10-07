using System.Collections.Generic;

namespace ConsoleApp1
{
    public class BookPriceComparer : IComparer<Book>
    {
        public int Compare(Book? x, Book? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x is null) return -1;
            if (y is null) return 1;
            int priceCmp = x.Price.CompareTo(y.Price);
            if (priceCmp != 0) return priceCmp;
            return x.SerialNumber.CompareTo(y.SerialNumber);
        }
    }
}


