namespace TableTopBattleTracker.Data
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetItems();
        T GetItem(string id);


    }
}
