using System;
using System.Collections.Generic;
using System.Text;

namespace Entity_framework_labs
{
    class Address
    {
        public int addressID { get; set; }
        public string House_number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public ICollection<Person> People{ get; set; }
        public Address() { }
       
    }
}
