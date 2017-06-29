using System;
using System.Data.Entity;

namespace Library
{
    public class UnitOfWork<TContext> where TContext : DbContext, new()
    {
        private readonly TContext Context;

        public UnitOfWork(TContext context)
        {
            Context = context;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
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
