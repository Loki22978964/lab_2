
using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal static class Program
    {
        private static void Main()
        {
            // 1) 4 книги (дані згідно вимог)
            var b1 = new Book(1, "Clean Code", 2008, 500m, 10);
            var b2 = new Book(2, "The Pragmatic Programmer", 1999, 450m, 7);
            var b3 = new Book(3, "Design Patterns", 1994, 650m, 5);
            var b4 = new Book(4, "Refactoring", 1999, 550m, 8);

            // 2) Масив: додати (Resize), оновити, пошук, прохід
            Book[] arr = { b1, b2, b3 };
            arr[0] = new Book(1, "Clean Code (Updated)", 2008, 520m, 10); // update
            int idx = Array.FindIndex(arr, b => b.Name.Contains("Design Patterns", StringComparison.OrdinalIgnoreCase)); // search
            Array.Resize(ref arr, arr.Length + 1); // add
            arr[^1] = b4;
            Console.WriteLine("Array:");
            foreach (var b in arr) Console.WriteLine(b);

            // Generic List<T>: додати, видалити, оновити, пошук, прохід
            var list = new List<Book> { b1, b2 };
            list.Add(b3);
            list.Remove(b2);
            var f = list.Find(b => b.SerialNumber == 3); // search
            if (f is not null) f.IncreasePriceByPercent(10); // update
            Console.WriteLine("List<Book>:");
            foreach (var b in list) Console.WriteLine(b);

            // Non-generic ArrayList: додати, видалити, оновити, пошук, прохід
            var al = new ArrayList { b1, b2 };
            al.Add(b3);
            al.Remove(b2);
            al[0] = new Book(1, "Clean Code (AL)", 2008, 515m, 10); // update
            Book? found = null; // search (з приведенням)
            foreach (var it in al) { if (it is Book bk && bk.SerialNumber == 3) { found = bk; break; } }
            Console.WriteLine("ArrayList:");
            foreach (var it in al) Console.WriteLine(it);

            Console.WriteLine("--- Array: fixed size; List<T>: type-safe; ArrayList: requires casts ---");

            // 3) Бінарні дерева: за серійним номером (IComparable) і за ціною (IComparer)
            var bstSn = new BinarySearchTree<Book>();
            bstSn.Insert(b1); bstSn.Insert(b2); bstSn.Insert(b3); bstSn.Insert(b4);
            Console.WriteLine("BST postorder (by SerialNumber):");
            foreach (var b in bstSn) Console.WriteLine(b);

            var bstPrice = new BinarySearchTree<Book>(new BookPriceComparer());
            bstPrice.Insert(b1); bstPrice.Insert(b2); bstPrice.Insert(b3); bstPrice.Insert(b4);
            Console.WriteLine("BST postorder (by Price):");
            foreach (var b in bstPrice) Console.WriteLine(b);
        }
    }
}

