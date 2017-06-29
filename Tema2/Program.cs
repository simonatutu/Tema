using Library;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2
{
    class Program
    {
        static void Main(string[] args)
        {

           UnitOfWork<LibraryContext> unitOfWork = new UnitOfWork<LibraryContext>();
            var books = unitOfWork.GenericRepository.GetByID(1).Title;
            unitOfWork.GenericRepository.Insert(new Book
            {
                ISBN = "123-100-100-134",
                Title = "Test Title 10",
                Author = "Test Author 10",
                NumberOfCopies = 11,
                CreatedDate = DateTime.Today
            });
            unitOfWork.Save();
            unitOfWork.GenericRepository.Delete(2);
            Console.ReadLine();

            //    AddBook("123-800-000-991", "Test Title 9", "Test Author 9", 8);
            //    AddBook("123-100-100-134", "Test Title 10", "Test Author 10", 11);


            //AddBook(new Book {
            //       ISBN= "123-100-100-134",
            //       Title="Test Title 10",
            //       Author = "Test Author 10",
            //       NumberOfCopies= 11,
            //      CreatedDate = DateTime.Today});

            //    EditBook(8, "123-100-999-110", "Updated title 11", "Updated author 11", 5);
            //    DeleteBook(6);


            //    List<string> books = new List<string>();
            //    books = ListAllBooks();
            //    foreach (var b in books)
            //    {
            //        Console.WriteLine(b);
            //    }

            //    AddSubscriber("Ion", "Popescu", "0721212121", DateTime.Parse("12/03/1981"));
            //    AddSubscriber("Vasile", "Popescu", "0721212120", DateTime.Parse("12/03/1980"));
            //    EditSubscriber(6, "Costica", "Gigescu", "0721212120", DateTime.Parse("12/03/1970"));
            //    DeleteSubscriber(4);

            //    List<string> subs = new List<string>();
            //    subs = ListAllSubscribers();
            //    foreach (var b in subs)
            //    {
            //        Console.WriteLine(b);
            //    }

            //    BorrowBook(1, 1);
            //    BorrowBook(8, 2);
            //    ReturnBook(8);

        }

        #region CRUDMethods
        /*
        public static void AddBook(Book input)
        {
            using (var db = new LibraryContext())
            {
                db.Books.Add(input);
                db.SaveChanges();
            }
        }

        public static void EditBook(Book book)
        {
            // Get book from the db
            using (var db = new LibraryContext())
            {
                book = db.Books.Where(p => p.Id == book.Id).FirstOrDefault<Book>();


                // change book info TODO: verify
                if (book != null)
                {
                    book.ISBN = book.ISBN;
                    book.Title = book.Title;
                    book.Author = book.Author;
                    book.NumberOfCopies = book.NumberOfCopies;
                }

                // Mark book as modified
                db.Entry(book).State = System.Data.Entity.EntityState.Modified;

                //call SaveChanges
                db.SaveChanges();
            }
        }

        public static void DeleteBook(int bookId)
        {
            Book book;
            using (var db = new LibraryContext())
            {
                book = db.Books.Where(p => p.Id == bookId).FirstOrDefault<Book>();
                var results = (from p in db.Loans
                               where p.Book.Id == bookId
                               select p).ToList();

                foreach (Loan p in results)
                {
                    p.Book = null;
                }
                db.Books.Attach(book);
                db.Books.Remove(book);

                db.SaveChanges();
            }
        }

        public static List<string> ListAllBooks()
        {
            using (var db = new LibraryContext())
            {
                var list = db.Books.Select(b => b.Title).ToList();
                return list;
            }
        }

        public static void AddSubscriber(Subscriber sub)
        {
            using (var db = new LibraryContext())
            {
                db.Subscribers.Add(sub);
                db.SaveChanges();
            }
        }

        public static void EditSubscriber(int subId, string firstName, string lastName, string phone, DateTime dateOfBirth)
        {
            Subscriber sub;
            using (var db = new LibraryContext())
            {
                sub = db.Subscribers.Where(p => p.Id == subId).FirstOrDefault<Subscriber>();


                if (sub != null)
                {
                    sub.FirstName = firstName;
                    sub.LastName = lastName;
                    sub.PhoneNumber = phone;
                    sub.DateOfBirth = dateOfBirth;
                }
                db.SaveChanges();
            }

        }

        public static void DeleteSubscriber(int subId)
        {
            Subscriber sub;
            using (var db = new LibraryContext())
            {
                sub = db.Subscribers.Where(p => p.Id == subId).FirstOrDefault<Subscriber>();
                var results = (from p in db.Loans
                               where p.Subscriber.Id == subId
                               select p).ToList();
                foreach (Loan p in results)
                {
                    p.Subscriber = null;
                }
                db.Subscribers.Attach(sub);
                db.Subscribers.Remove(sub);
                db.SaveChanges();
            }
        }

        public static List<string> ListAllSubscribers()
        {
            using (var db = new LibraryContext())
            {
                var list = db.Subscribers.Select(p => p.FirstName + " " + p.LastName).ToList();
                return list;
            }
        }
        */
        #endregion

        public static void BorrowBook(int bookId, int subId)
        {
            Book book;
            Subscriber sub;
            using (var db = new LibraryContext())
            {
                //find book
                book = db.Books.Where(p => p.Id == bookId).FirstOrDefault<Book>();

                //find subscriber
                sub = db.Subscribers.Where(p => p.Id == subId).FirstOrDefault<Subscriber>();

                if (book != null && book.NumberOfCopies != 0)
                {
                    db.Loans.Add(new Loan
                    {
                        BorrowedDate = DateTime.Today,
                        ReturnDate = DateTime.Today.AddMonths(1),
                        Returned = false,
                        Book = book,
                        Subscriber = sub
                    });
                    book.NumberOfCopies -= 1;
                    db.SaveChanges();
                }
            }
        }

        public static void ReturnBook(int loanId)
        {
            Loan loan;
            using (var db = new LibraryContext())
            {
                loan = db.Loans.Where(p => p.LoanID == loanId).FirstOrDefault<Loan>();
                if (loan != null && loan.Book != null)
                {
                    loan.Returned = true;
                    loan.Book.NumberOfCopies += 1;
                    loan.ReturnDate = DateTime.Today;
                    db.SaveChanges();
                }
            }
        }

    }
}

