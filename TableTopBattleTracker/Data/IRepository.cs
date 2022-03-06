namespace TableTopBattleTracker.Data
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T>? GetItems();

        IEnumerable<T>? GetByQuery(string query);

        T? GetItem(object id);

        void Create(T item);

        void Update(T item);

        void Delete(T item);

        void Save();

    }
}
