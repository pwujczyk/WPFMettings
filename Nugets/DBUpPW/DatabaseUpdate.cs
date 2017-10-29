

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
        
        private string JournalSchema = "adm";
        private string DbUpjournalTableName = "DBup";

        public DatabaseUpdate()
        {

        }
        public DatabaseUpdate(string journalSchema, string dbupJournalTableName):this()
        {
            this.JournalSchema = journalSchema;
            this.DbUpjournalTableName = dbupJournalTableName;
        }

        public void CreateSqlCe(string path,string connectionString)
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
              .JournalToSqlTable(null,"DBUp")
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

        public void CreateSQLDB(string databaseName, string setupDBConnectionString)
        {
            string query = string.Format(@"IF NOT EXISTS ( SELECT [Name] FROM sys.databases WHERE [name] = '{0}' )
                            BEGIN
                                CREATE DATABASE {0}
                            END
                            ", databaseName);

            InvokeQuery(setupDBConnectionString, query);
        }

        public void CreateAdmSchema(string updateDBConnectionString)
        {
           var query = @"IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'adm')
                            BEGIN
                            EXEC('CREATE SCHEMA adm')
                            END";
            InvokeQuery(updateDBConnectionString, query);
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

        public void UpdateDatabase(Assembly assembly, string updateDBConnectionString)
        {
            var upgrader = DbUp.DeployChanges.To
              .SqlDatabase(updateDBConnectionString)
              .WithScriptsEmbeddedInAssembly(assembly)
              .JournalToSqlTable("adm", "DBUp")
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
