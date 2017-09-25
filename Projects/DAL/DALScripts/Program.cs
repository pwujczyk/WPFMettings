using DBUpPW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DALCompactScripts
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseUpdate databaseUpdate = new DatabaseUpdate();
            Assembly assembly = Assembly.GetExecutingAssembly();
            string dataSource = Consts.DataSource;
            string databasesName = Consts.DatabasesName;
            Console.WriteLine("SqlServer");
            var initConnectionString = ConnectionStringHelper.ConnectionString.GetSQLDataSourceInitial(dataSource);
            databaseUpdate.CreateSQLDB(databasesName, initConnectionString);
            var connectionstring = ConnectionStringHelper.ConnectionString.GetSqlConnectionString(dataSource, databasesName);
            databaseUpdate.CreateAdmSchema(connectionstring);
            databaseUpdate.UpdateDatabase(assembly, connectionstring);

            Console.WriteLine("SqlFile");
            string compactFilePath = Consts.CompactFilePath;
            string connectionString = ConnectionStringHelper.ConnectionString.GetSQLCompactConnectionString(compactFilePath);
            databaseUpdate.CreateSqlCe(Consts.CompactFilePath, connectionString);
            databaseUpdate.UpdateDatabaseSqlCe(assembly, connectionString);    
        }
    }
}
