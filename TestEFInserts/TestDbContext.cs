using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestEFInserts.Entities;

namespace TestEFInserts
{
    public class TestDbContext : DbContext
    {
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<Son> Sons { get; set; }
        public virtual DbSet<SonWithAutoParent> SonWithAutoParent { get; set; }
        public virtual DbSet<Toy> Toys { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }

        public TestDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local);Database=TestApp;Trusted_Connection=True;");
        }
    }
}
