using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLocal
{
    class MigrationHelper
    {
        public MigrationHelper()
        {
            Migrations = new Dictionary<int, IList>();
            MigrationVersion1();
            MigrationVersion2();
            MigrationVersion3();
        }

        public Dictionary<int, IList> Migrations { get; set; }

        private void MigrationVersion1()
        {
            IList steps = new List<string>();

            steps.Add("CREATE TABLE \"Meeting\" (\"MeetingId\" INTEGER  PRIMARY KEY AUTOINCREMENT NOT NULL, \"Notes\" TEXT)");

            Migrations.Add(1, steps);
        }

        private void MigrationVersion2()
        {
            IList steps = new List<string>();

            steps.Add("DROP TABLE \"Meeting\"");
            steps.Add("CREATE TABLE \"Meeting\" (\"MeetingId\" INTEGER  PRIMARY KEY AUTOINCREMENT NOT NULL, \"BeforeNotes\" TEXT,\"DuringNotes\" TEXT, \"AfterNotes\" TEXT, \"Date\" DATETIME)");

            Migrations.Add(2, steps);
        }

        private void MigrationVersion3()
        {
            IList steps = new List<string>();

            //steps.Add("CREATE TABLE \"Contact\" (\"ContactId\" INTEGER  PRIMARY KEY AUTOINCREMENT NOT NULL, \"FirstName\" TEXT,\"LastName\" TEXT, \"Email\" TEXT");
            //steps.Add("CREATE TABLE \"MeetingContact\" (\"MeetingContactId\" INTEGER  PRIMARY KEY AUTOINCREMENT NOT NULL, \"MeetingId\" TEXT,\"ContactId\" INT");

            Migrations.Add(3, steps);
        }
    }
}
