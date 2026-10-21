using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Newtonsoft.Json;

namespace Core.Common.NoSqlDataAccess.DataAccess
{
    public class DapperNoSqlEncrypted<T> : INoSqlDb<T> 
    {
        private readonly string _connectionString;
        private const string CreateTableCommand = "if not exists (select * from sysobjects where name='{0}' and xtype='U') create table {0}(Id varchar(64) not null, Value VARCHAR(MAX)) ";
        private const string CreateTableCommandUnicode = "if not exists (select * from sysobjects where name='{0}' and xtype='U') create table {0}(Id varchar(64) not null, Value NVARCHAR(MAX)) ";
        private const string InsertOrUpdateCommand = @"if not exists (select * from {0} where Id=@Key) " +
                                              " begin" +
                                              "	Insert Into {0} VALUES (@Key,@SerializedJson);" +
                                              " end " +
                                              " else " +
                                              " begin " +
                                              "	Update {0} set Value = @SerializedJson Where  Id=@Key" +
                                              " end";
        private const string GetByKeyCommand = "Select * from {0} where Id=@Key";
        private const string DeleteByKeyCommand = "Delete from {0} where Id=@Key";
        private const string DeleteTableCommand = "DROP TABLE {0}";

        public DapperNoSqlEncrypted(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddOrUpdate(string tableName, string key, T entity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string serializedJson = JsonConvert.SerializeObject(entity, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
                });
                conn.Execute(string.Format(InsertOrUpdateCommand,tableName), new 
                {
                    Key = key,
                    SerializedJson = SecurityHelper.EncryptString(serializedJson)
                });
            }
        }

        public void CreateTableIfNotExists(string tableName, bool unicode = false)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                if (unicode)
                {
                    conn.Execute(string.Format(CreateTableCommandUnicode, tableName));
                }
                else
                {
                    conn.Execute(string.Format(CreateTableCommand, tableName));
                }
            }
        }

        public void Delete(string tableName, string key)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(string.Format(DeleteByKeyCommand,tableName), new
                {
                    Key = key
                });
            }
        }

        public void DeleteTable(string tableName)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(string.Format(DeleteTableCommand, tableName));
            }
        }

        public T GetByKey(string tableName, string key)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var row = conn.Query<TableRow>(string.Format(GetByKeyCommand,tableName), new
                    {
                        Key = key
                    }
                ).FirstOrDefault();
                if (row == null || string.IsNullOrEmpty(row.Value))
                {
                    return default(T);
                }
               return JsonConvert.DeserializeObject<T>(SecurityHelper.DecryptString(row.Value), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
               });
            }
        }
    }
}
