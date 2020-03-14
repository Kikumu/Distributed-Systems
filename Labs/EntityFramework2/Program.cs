using System;
using System.Collections.Generic;
namespace EntityFramework2
{
    class Program
    {
        static void Main(string[] args)
        {
           using (var ctx = new Context())
            {
                Address adr = new Address()
                {
                    Street = "Yisss",
                    Postcode = "420",
                    People = new List<Person>()
                };
                Person prsn = new Person()
                {
                    FirstName = "jj",
                    LastName = "kk",
                    Address = adr,
                };

                ctx.Addresses.Add(adr);
                ctx.People.Add(prsn);
                ctx.SaveChanges();
            }
        }
    }
}
