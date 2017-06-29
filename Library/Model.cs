using System.Data.Entity;

namespace Library
{
    public class LibraryContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}
