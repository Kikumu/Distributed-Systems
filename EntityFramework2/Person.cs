using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework2
{
    class Person
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Address Address { get; set; }
        public Person() { }

    }
}
