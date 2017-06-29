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
            var localContext = new LibraryContext();
            var unitOfWork = new UnitOfWork<LibraryContext>(localContext);
            var repositoryBooks = new GenericRepository<Book>(localContext);
            var repositorySubscribers = new GenericRepository<Subscriber>(localContext);


            var books = repositoryBooks.GetByID(1).Title;
            repositoryBooks.Insert(new Book
            {
                ISBN = "123-100-100-134",
                Title = "Test Title 10",
                Author = "Test Author 10",
                NumberOfCopies = 11,
                CreatedDate = DateTime.Today
            });

            repositorySubscribers.Insert(new Subscriber
            {
                FirstName = "Gigi",
                LastName = "Ion",
                PhoneNumber = "0747599999",
                DateOfBirth = DateTime.Parse("11/03/1950"),
                CreatedDate = DateTime.Today
            });

            unitOfWork.Save();
           
            Console.ReadLine();

        }

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

