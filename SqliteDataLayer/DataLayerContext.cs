using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDataLayer
{
     public class DataLayerContext:DbContext
    {
        public DataLayerContext() : base("name = SqliteConnStr") { }

        public DataLayerContext(string filename) : base(new SQLiteConnection()
        {
            ConnectionString =
                    new SQLiteConnectionStringBuilder()
                    { DataSource = filename, ForeignKeys = true }
                    .ConnectionString
        }, true)
        { }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
         
            //modelBuilder.Entity<Customer>().Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

        }

        public DbSet<PoliceStation> PoliceStations { get; set; }

        public DbSet<PoliceOfficer> PoliceOfficers { get; set; }

        public DbSet<TopicsAndArea> TopicsAndAreas { get; set; }

        public DbSet<LetterRecord> LetterRecords { get; set; }
        public  DbSet<Status> Status { get; set; }

    }
   
}
