using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework2
{
    class Address
    {
        public int AddressID { get; set; }
        public string Street { get; set; }
        public string Postcode { get; set; }
        public ICollection<Person> People{get; set;}
        public Address() { }
    }
}
