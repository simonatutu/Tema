using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Library
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;
        void Save();
        ValidationResult Validate();
        DbContext Context { get; }
    }
}