using IndexTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndexTest.Entity
{
    public class IndexTestDBContext : DbContext
    {

        public DbSet<Gender> Genders { get; set; }
        public DbSet<IndexUser> IndexUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Initial Catalog=IndexTestDB;User Id=sa;Password=DoNotTryToConnectToASQLDatabaseOnAFridayAfterNoon;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IndexUser>()
             .HasData(
              new IndexUser { Id = 1, Username = "username", Password = "password", FirstName = "firstname", LastName = "lastname" });


            modelBuilder.Entity<Gender>()
             .HasData(
              new Gender { Id = 1, Name = "Female" },
              new Gender { Id = 2, Name = "Male" }
              );
        }
    }
}
