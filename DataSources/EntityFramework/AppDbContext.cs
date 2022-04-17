using LibraryExample.DataSources.Json;
using LibraryExample.Domain.Entities;
using System.Data.Entity;

namespace LibraryExample.DataSources.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Rack> Racks { get; set; }

        public AppDbContext(string connectionString) : base(connectionString)
        {
            Database.CreateIfNotExists();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JsonBook>()
                .Ignore(x => x.AuthorId)
                .Ignore(x => x.RackId)
                .HasRequired(x => x.Author);

            modelBuilder.Entity<JsonRack>()
                .Ignore(x => x.BookIds);

            modelBuilder.Entity<JsonAuthor>()
                .Ignore(x => x.BookIds);

            modelBuilder.Entity<Author>().ToTable("Авторы");
            modelBuilder.Entity<Book>().ToTable("Книги");
            modelBuilder.Entity<Rack>().ToTable("Стеллажи");

            modelBuilder.Entity<Author>().HasKey(x => x.Id);
            modelBuilder.Entity<Book>().HasKey(x => x.Id);
            modelBuilder.Entity<Rack>().HasKey(x => x.Id);
        }
    }
}
