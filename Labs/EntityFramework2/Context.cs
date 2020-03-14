using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace EntityFramework2
{
    class Context:DbContext
    {
        public Context() : base() { }
        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
             "Server = (localdb)\\mssqllocaldb; Database = EntityLab;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
