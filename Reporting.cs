using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Assume that we have a list of books, patrons and their circulation history
            List<Book> books = new List<Book>();
            List<Patron> patrons = new List<Patron>();
            List<CirculationHistory> circulationHistory = new List<CirculationHistory>();

            // Generate report on book circulation
            Console.WriteLine("Book Circulation Report");
            Console.WriteLine("-----------------------");
            var bookCirculation = circulationHistory.GroupBy(h => h.Book)
                .Select(g => new { Book = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count);
            foreach (var item in bookCirculation)
            {
                Console.WriteLine($"{item.Book.Title} - {item.Count}");
            }
            Console.WriteLine();

            // Generate report on overdue books
            Console.WriteLine("Overdue Books Report");
            Console.WriteLine("--------------------");
            var overdueBooks = circulationHistory.Where(h => h.ReturnDate == null && h.DueDate < DateTime.Today)
                .Select(h => h.Book)
                .Distinct()
                .OrderBy(b => b.Title);
            foreach (var book in overdueBooks)
            {
                Console.WriteLine($"{book.Title}");
            }
            Console.WriteLine();

            // Generate report on patron history
            Console.WriteLine("Patron History Report");
            Console.WriteLine("---------------------");
            var patronHistory = circulationHistory.GroupBy(h => h.Patron)
                .Select(g => new { Patron = g.Key, Count = g.Count(), Books = string.Join(", ", g.Select(h => h.Book.Title)) })
                .OrderByDescending(g => g.Count);
            foreach (var item in patronHistory)
            {
                Console.WriteLine($"{item.Patron.Name} ({item.Count}) - {item.Books}");
            }
            Console.WriteLine();

            Console.ReadLine();
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        // other book properties
    }

    public class Patron
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // other patron properties
    }

    public class CirculationHistory
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public Patron Patron { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        // other circulation history properties
    }
}
