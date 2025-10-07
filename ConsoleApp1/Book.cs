using System;

namespace ConsoleApp1
{
    public class Book : IComparable<Book>
    {
        public int SerialNumber { get; }
        public string Name { get; }
        public int Year { get; }
        public decimal Price { get; private set; }
        public int Copies { get; private set; }

        public Book(int serialNumber, string name, int year, decimal price, int copies)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            if (year <= 0)
                throw new ArgumentOutOfRangeException(nameof(year));
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price));
            if (copies < 0)
                throw new ArgumentOutOfRangeException(nameof(copies));

            SerialNumber = serialNumber;
            Name = name;
            Year = year;
            Price = price;
            Copies = copies;
        }

        public void IncreasePriceByPercent(decimal percent)
        {
            if (percent < -100)
                throw new ArgumentOutOfRangeException(nameof(percent));
            Price = decimal.Round(Price * (1m + percent / 100m), 2, MidpointRounding.AwayFromZero);
        }

        public decimal GetTotalPrintRunCost()
        {
            return Price * Copies;
        }

        public void UpdateCopies(int newCopies)
        {
            if (newCopies < 0)
                throw new ArgumentOutOfRangeException(nameof(newCopies));
            Copies = newCopies;
        }

        public int CompareTo(Book? other)
        {
            if (other is null) return 1;
            // Default ordering by SerialNumber
            return SerialNumber.CompareTo(other.SerialNumber);
        }

        public override string ToString()
        {
            return $"SN={SerialNumber}, Name=\"{Name}\", Year={Year}, Price={Price:F2} тугріки, Copies={Copies}, Total={GetTotalPrintRunCost():F2} тугріки";
        }
    }   
}


