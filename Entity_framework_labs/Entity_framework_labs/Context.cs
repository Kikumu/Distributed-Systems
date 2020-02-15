using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Entity_framework_labs
{
    class Context:DbContext
    {
        public Context() : base() { }
        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server = (localdb)\mssqllocaldb; Database = EFLab;Trusted_Connection=True;",
               options => options.EnableRetryOnFailure());
            base.OnConfiguring(optionsBuilder);
           
        }
    }
}
//DESKTOP-882USAQ\scowt