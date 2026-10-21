namespace Core.Common.NoSqlDataAccess.DataAccess
{
    public interface INoSqlDb<T>
    {
        void CreateTableIfNotExists(string tableName, bool unicode = false);
        void DeleteTable(string tableName);
        void AddOrUpdate(string tableName, string key, T entity);
        T GetByKey(string tableName, string key);
        void Delete(string tableName, string key);
    }
}
