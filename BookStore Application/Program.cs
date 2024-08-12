using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Lifetime;
using System;

namespace BookStore_Application
{
    class Program
    {
        // Method to input an integer number within a specified range
        public static int InputValue(int min, int max) 
        {
            int value;
            bool isValidInput = false;

            do
            {
                Console.Write($"Enter a number between {min} and {max}: ");
                string input = Console.ReadLine();

                isValidInput = int.TryParse(input, out value);

                if (!isValidInput || value < min || value > max)
                    Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");

            } while (!isValidInput || value < min || value > max);

            return value;
        }

        // Method to check if a string is a valid book ID
        public static bool IsValid(string id)
        {
            if (id.Length != 5)
                return false;

            if (!char.IsUpper(id[0]) || !char.IsUpper(id[1]))
                return false;

            if (!char.IsDigit(id[2]) || !char.IsDigit(id[3]) || !char.IsDigit(id[4]))
                return false;

            return true;
        }
//--------------------------------------------------------------------------------------------------- Part A
        class Book
        {
            public static string[] categoryCodes = { "CS", "IS", "SE", "SO", "MI" };
            public static string[] categoryNames = { "Computer Science", "Information System", "Security", "Society", "Miscellaneous" };
            // Two public static arrays
            private string bookId;
            private string categoryNameOfBook;

            public string BookId
            {
                get { return bookId; }
                set
                {
                    bookId = value;
                    if (Array.IndexOf(categoryCodes, value.Substring(0, 2)) != -1)
                        categoryNameOfBook = categoryNames[Array.IndexOf(categoryCodes, value.Substring(0, 2))];
                    else
                        categoryNameOfBook = categoryNames[Array.IndexOf(categoryCodes, "MI")];
                }
            }

            public string CategoryName
            {
                get { return categoryNameOfBook; }
            }

            public string BookTitle { get; set; }
            public int NumOfPages { get; set; }
            public double Price { get; set; }

            public Book() //one wth no parameter
            {
            }

            public Book(string bookId, string bookTitle, int numPages, double price) //One with a parameter for all data fields
            {
                BookId = bookId;
                BookTitle = bookTitle;
                NumOfPages = numPages;
                Price = price;
            }

            public override string ToString()//ToString method
            {
                return $"Book ID: {BookId}\nTitle: {BookTitle}\nNumber of Pages: {NumOfPages}\nPrice: {Price:C}\n";
            }// screenhot of "Information of all Books
        }
//--------------------------------------------------------------------------------------------------------------------------------
        private static void GetBookData(int num, Book[] books) // method for Book Data
        {
            
            Console.WriteLine("Enter book details:");

            for (int i = 0; i < num; i++)
            {
                Console.WriteLine($"Book {i + 1}:");
                books[i] = new Book();

                bool isValidId = false;
                do
                {
                    Console.Write("Enter book ID: ");
                    string bookId = Console.ReadLine();
                    isValidId = IsValid(bookId);

                    if (!isValidId)
                        Console.WriteLine("Invalid book ID. Please try again.");

                    books[i].BookId = bookId;
                } while (!isValidId);

                Console.Write("Enter book title: ");
                books[i].BookTitle = Console.ReadLine();

                books[i].NumOfPages = InputValue(1, int.MaxValue);

                Console.Write("Enter book price: $");
                books[i].Price = double.Parse(Console.ReadLine());
            }
        }


        private static void DisplayAllBooks(Book[] books) // Method to display all books
        {
            Console.WriteLine("\nInformation of all Books:");
            foreach (Book book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        private static void GetLists(int num, Book[] books) // method to display the valid book categories and all the information in the category
        {
            Console.WriteLine("\nValid Book Categories:");
            for (int i = 0; i < Book.categoryCodes.Length; i++)
            {
                Console.WriteLine($"{Book.categoryCodes[i]}: {Book.categoryNames[i]}");
            }

            while (true)
            {
                Console.Write("\nEnter a category code (or 'exit' to quit): ");
                string categoryCode = Console.ReadLine().ToUpper();

                if (categoryCode == "EXIT")
                    break;

                int categoryIndex = Array.IndexOf(Book.categoryCodes, categoryCode);
                if (categoryIndex != -1)
                {
                    int count = 0;
                    Console.WriteLine($"\nBooks in the category '{Book.categoryNames[categoryIndex]}':");
                    foreach (Book book in books)
                    {
                        if (book.BookId.StartsWith(categoryCode))
                        {
                            Console.WriteLine(book.ToString());
                            count++;
                        }
                    }
                    Console.WriteLine($"Total books in this category: {count}");
                }
                else
                {
                    Console.WriteLine("Invalid category code. Please try again.");
                }
            }
        }

        static void Main()
        {
            int numBooks = InputValue(1, 30); // number of books between 1 to 30
            Book[] books = new Book[numBooks];

            GetBookData(numBooks, books);
            DisplayAllBooks(books);
            GetLists(numBooks, books);
        }
    }
}


