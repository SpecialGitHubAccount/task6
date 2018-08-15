using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task6
{
    class Program
    {
        static void Main()
        {
            bool isExit = false;

            string title = string.Empty;
            string note = string.Empty;
            string author = string.Empty;
            int sheetsQuantity = 0;
            int publicationYear = 0;
            string publisherName = string.Empty;
            string publicationPlace = string.Empty;
            string isbn = string.Empty;
            string issn = string.Empty;
            int id = 0;
            string inventor = string.Empty;
            int registrationNumber = 0;
            string country = string.Empty;
            DateTime applicationSubmissionDate;
            DateTime publicationDate;
            DateTime date;

            while (!isExit)
            {
                Console.WriteLine("1 - read");
                Console.WriteLine("2 - write new book");
                Console.WriteLine("3 - write new newspaper");
                Console.WriteLine("4 - write new patent");
                Console.WriteLine("5 - exit");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        using (LibResRW rwLib = new LibResRW(File.Open("catalog.xml", FileMode.OpenOrCreate)))
                        {
                            IEnumerable<LibraryResource> libResources = rwLib.Read();
                            foreach (var item in libResources)
                            {
                                Console.WriteLine(item.ToString());
                                Console.WriteLine();
                            }
                        }
                        break;
                    case '2':
                        Console.WriteLine("input isbn");
                        InputValidIsbn13(out isbn);
                        Console.WriteLine("input author");
                        InputString(out author);
                        Console.WriteLine("input note");
                        InputString(out note);
                        Console.WriteLine("input publication Place");
                        InputString(out publicationPlace);
                        Console.WriteLine("input publisher Name");
                        InputString(out publisherName);
                        Console.WriteLine("input title");
                        InputString(out title);
                        Console.WriteLine("input publication Year");
                        InputPositiveInt(out publicationYear);
                        Console.WriteLine("input sheets Quantity");
                        InputPositiveInt(out sheetsQuantity);
                        Book book = new Book
                        {
                            ISBN = isbn,
                            Author = author,
                            Note = note,
                            PublicationPlace = publicationPlace,
                            PublisherName = publisherName,
                            PublishingYear = publicationYear,
                            SheetsQuantity = sheetsQuantity,
                            Title = title
                        };
                        using (LibResRW rwLib = new LibResRW(File.Open("catalog.xml", FileMode.OpenOrCreate)))
                        {
                            rwLib.Write(book);
                        }
                        break;
                    case '3':
                        Console.WriteLine("input issn");
                        InputValidIssn(out issn);
                        Console.WriteLine("input id");
                        InputPositiveInt(out id);
                        Console.WriteLine("input note");
                        InputString(out note);
                        Console.WriteLine("input publication Place");
                        InputString(out publicationPlace);
                        Console.WriteLine("input publisher Name");
                        InputString(out publisherName);
                        Console.WriteLine("input title");
                        InputString(out title);
                        Console.WriteLine("input publication Year");
                        InputPositiveInt(out publicationYear);
                        Console.WriteLine("input sheets Quantity");
                        InputPositiveInt(out sheetsQuantity);
                        Console.WriteLine("date");
                        InputValidDateTime(out date);
                        Newspaper newspaper = new Newspaper
                        {
                            ISSN = issn,
                            Id = id,
                            Note = note,
                            PublicationPlace = publicationPlace,
                            PublisherName = publisherName,
                            PublishingYear = publicationYear,
                            SheetsQuantity = sheetsQuantity,
                            Title = title,
                            Date = date
                        };
                        using (LibResRW rwLib = new LibResRW(File.Open("catalog.xml", FileMode.OpenOrCreate)))
                        {
                            rwLib.Write(newspaper);
                        }
                        break;
                    case '4':
                        Console.WriteLine("input title");
                        InputString(out title);
                        Console.WriteLine("input note");
                        InputString(out note);
                        Console.WriteLine("input sheets Quantity");
                        InputPositiveInt(out sheetsQuantity);
                        Console.WriteLine("input application Submission Date");
                        InputValidDateTime(out applicationSubmissionDate);
                        Console.WriteLine("input country");
                        InputString(out country);
                        Console.WriteLine("input inventor");
                        InputString(out inventor);
                        Console.WriteLine("input publication Date");
                        InputValidDateTime(out publicationDate);
                        Console.WriteLine("input registration Number");
                        InputPositiveInt(out registrationNumber);
                        Patent patent = new Patent
                        {
                            Title = title,
                            Note = note,
                            SheetsQuantity = sheetsQuantity,
                            ApplicationSubmissionDate = applicationSubmissionDate,
                            Country = country,
                            Inventor = inventor,
                            PublicationDate = publicationDate,
                            RegistrationNumber = registrationNumber
                        };
                        using (LibResRW rwLib = new LibResRW(File.Open("catalog.xml", FileMode.OpenOrCreate)))
                        {
                            rwLib.Write(patent);
                        }
                        break;
                    case '5':
                        isExit = true;
                        break;
                }
            }
        }

        private static void InputValidDateTime(out DateTime datetime)
        {
            bool isValid = false;
            do
            {
                Console.WriteLine("input datetime mm/dd/yyyy");
                if (DateTime.TryParse(Console.ReadLine(), out datetime))
                {
                    isValid = true;
                }
            }
            while (!isValid);
        }

        private static void InputValidIsbn13(out string isbn)
        {
            do
            {
                Console.WriteLine("input isbn like 'ISBN-13: ISBN 978-1-4028-9462-6'");
                isbn = Console.ReadLine();
            }
            // пример: ISBN-13: ISBN 978-1-4028-9462-6
            while (!Regex.IsMatch(isbn, @"^ISBN \d{3}-\d{1}-\d{4}-\d{4}-\d{1}$"));
        }

        private static void InputValidIssn(out string issn)
        {
            do
            {
                Console.WriteLine("input issn like 'ISSN 1234-5678'");
                issn = Console.ReadLine();
            }
            while (!Regex.IsMatch(issn, @"ISSN \d{4}-\d{4}"));
        }

        private static void InputString(out string input)
        {
            do
            {
                input = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(input));
        }

        private static void InputPositiveInt(out int input)
        {
            bool isValid = false;
            do
            {
                if (int.TryParse(Console.ReadLine(), out input) && input > 0)
                {
                    isValid = true;
                }
            }
            while (!isValid);
        }
    }
}
