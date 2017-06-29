using Library.Base;
using System;

namespace Library
{
    public class Loan : EntityBase
    {
        public int LoanID { get; set; }

        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool Returned { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int SubscriberId { get; set; }
        public virtual Subscriber Subscriber { get; set; }
    }
}
