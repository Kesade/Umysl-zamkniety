using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Entities;

namespace Contexts.Contexts
{
    public class DiaryDbContext : DbContext
    {
        // Your context has been configured to use a 'DiaryDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CoolLife.Repositories.EntityFramework.DiaryDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DiaryDbContext' 
        // connection string in the application configuration file.
        public DiaryDbContext()
            : base(ConfigurationManager.AppSettings["connectionString"])
        {
        }

        public DbSet<Diaries> Diaries { get; set; }
        public DbSet<Entries> Entries { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Comments> Comments { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}