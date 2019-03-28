using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestConsoleApp
{
    public class TestDbContext : DbContext
    {
        public virtual DbSet<SecondsContainer> Seconds { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }
    }
}
