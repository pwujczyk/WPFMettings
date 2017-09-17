using DALLocal.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLocal
{
    public class LocalDBContext : DbContext
    {
        public static int RequiredDatabaseVersion = 3;
        public DbSet<Schema> Schema { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        public LocalDBContext(string connectionString) : base(new SQLiteConnection() { ConnectionString = connectionString }, true) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public void Initialize(string connectionstring)
        {
            using (LocalDBContext emptyContext = new LocalDBContext(connectionstring))
            {
                int currentVersion = 0;
                if (emptyContext.Schema.Count() > 0)
                {
                    currentVersion = emptyContext.Schema.Max(x => x.Version);
                }
                MigrationHelper mmSqliteHelper = new MigrationHelper();
                while (currentVersion < RequiredDatabaseVersion)
                {
                    currentVersion++;
                    foreach (string migration in mmSqliteHelper.Migrations[currentVersion])
                    {
                        emptyContext.Database.ExecuteSqlCommand(migration);
                    }
                    emptyContext.Schema.Add(new Schema() { Version = currentVersion });
                    emptyContext.SaveChanges();
                }
            }
        }

        public List<Meeting> GetMeetings()
        {
            List<Meeting> list =this.Meetings.ToList();
            return list;
        }

        public void AddMeeting(Meeting m)
        {
            if (m.MeetingId!=-1)
            {
                var db=this.Meetings.Single(x => x.MeetingId == m.MeetingId);
                db.AfterNotes = m.AfterNotes;
                db.BeforeNotes = m.BeforeNotes;
                db.Date = m.Date;
                db.DuringNotes = m.DuringNotes;
            }
            else
            {
                this.Meetings.Add(m);
            }
            this.SaveChanges();
        }
    }
}
