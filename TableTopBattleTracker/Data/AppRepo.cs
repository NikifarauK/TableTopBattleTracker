using Microsoft.EntityFrameworkCore;
using TableTopBattleTracker.Model;

namespace TableTopBattleTracker.Data
{
    public class AppRepo<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext? _dbContext = null;
        private readonly DbSet<T>? _table = null;

        public AppRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<T>();
        }
        
        public void Create(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            _table?.Add(item);
        }

        public void Delete(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            _table?.Remove(item);
        }

        public T? GetItem(object id)
        {
            return _table?.Find(id);
        }

        public IEnumerable<T>? GetItems()
        {
            return _table?.ToList();
        }

        public void Save()
        {
            _dbContext?.SaveChanges();
        }

        public void Update(T item)
        {
            _ = _table?.Update(item);
        }
    }
}
