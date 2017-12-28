

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBUpPW
{
    public class DatabaseUpdate
    {

        private string JorunalSchema;
        private string JorunalTableName;

        public DatabaseUpdate(string journalSchema) : this(journalSchema, "dbUp") { }
        public DatabaseUpdate(string journalSchema, string dbupJournalTableName)
        {
            this.JorunalSchema = journalSchema;
            this.JorunalTableName = dbupJournalTableName;
        }

        public void CreateSqlCe(string path, string connectionString)
        {
            if (!File.Exists(path))
            {
                SqlCeEngine en = new SqlCeEngine(connectionString);
                en.CreateDatabase();
            }
        }

        public void UpdateDatabaseSqlCe(Assembly assembly, string updateDBConnectionString)
        {
            var upgrader = DbUp.DeployChanges.To
              .SqlCeDatabase(updateDBConnectionString)
              .WithScriptsEmbeddedInAssembly(assembly)
              .JournalToSqlTable(null, "DBUp")
              .LogToConsole()
              .Build();

            var result = upgrader.PerformUpgrade();
            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
            }
        }

        public void CreateSQLDB(string databaseName, string dataSourceConnectionString)
        {
            string query = string.Format(@"IF NOT EXISTS ( SELECT [Name] FROM sys.databases WHERE [name] = '{0}' )
                            BEGIN
                                CREATE DATABASE {0}
                            END
                            ", databaseName);

            InvokeQuery(dataSourceConnectionString, query);
        }

        [Obsolete]
        public void CreateAdmSchema(string initialCatalogConnectionString)
        {
            var query = string.Format(@"IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = '{0}')
                            BEGIN
                            EXEC('CREATE SCHEMA {0}')
                            END", JorunalSchema);
            InvokeQuery(initialCatalogConnectionString, query);
        }

        private static void InvokeQuery(string connectionString, string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void PerformUpdate(string dataSource, string databaseName, Assembly assembly)
        {
            var dataSourceCS = ConnectionStringHelper.ConnectionString.GetSQLDataSourceConnectionString(dataSource);
            CreateSQLDB(databaseName, dataSourceCS);

            CreateAdmSchema(ConnectionStringHelper.ConnectionString.GetSqlServerConnectionString(dataSource, databaseName));
            UpdateDatabase(assembly, ConnectionStringHelper.ConnectionString.GetSqlServerConnectionString(dataSource, databaseName));
        }

        [Obsolete]
        public void UpdateDatabase(Assembly assembly, string updateDBConnectionString)
        {
            var upgrader = DbUp.DeployChanges.To
              .SqlDatabase(updateDBConnectionString)
              .WithScriptsEmbeddedInAssembly(assembly)
              .JournalToSqlTable(JorunalSchema, JorunalTableName)
              .LogToConsole()
              .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                //Console.ReadLine();
#endif
            }
        }
    }
}
