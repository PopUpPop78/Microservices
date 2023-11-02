using Common.Data;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace PlatformService.Data
{
    public abstract class Repository<T, TContext> : IRepository<T>
        where T : class, IModel
        where TContext : DbContext
    {
        private readonly TContext _context;

        public Repository(TContext context)
        {
            _context = context;
        }

        public virtual void Create(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _context.Set<T>().Add(item);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public virtual T GetItemById(int id)
        {
            return (from x in _context.Set<T>() where x.Id == id select x).FirstOrDefault();
        }

        public virtual bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}