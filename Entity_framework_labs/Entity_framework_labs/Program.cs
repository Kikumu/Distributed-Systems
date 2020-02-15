using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entity_framework_labs
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            try {
                using (var ctx = new Context())
                {
                    Address adr = new Address()
                    {
                        House_number = "1429",
                        Street = "Yeet",
                        City = "XD",
                        County = "Foo",
                        Postcode = "Bar",
                        People = new List<Person>()
                    };
                    Person prsn = new Person()
                    {
                        First_Name = "John",
                        Last_Name = "Diskson",
                        Address = adr,
                        DOB = new DateTime(2010, 02, 02)
                    };

                    ctx.Addresses.Add(adr);
                    ctx.People.Add(prsn);
                    ctx.SaveChanges();
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex);
            }
         
        }
    }
}
