using System;
using System.Data.Entity;

namespace Library
{
    public class UnitOfWork<TContext> where TContext : DbContext, new()
    {
        private LibraryContext context = new LibraryContext();

        private GenericRepository<Book> genericRepository; //check

        public GenericRepository<Book> GenericRepository
        {
            get
            {
                if (this.genericRepository == null)
                {
                    this.genericRepository = new GenericRepository<Book>(context);
                }
                return genericRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
