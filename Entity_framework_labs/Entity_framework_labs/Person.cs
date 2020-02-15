using System;
using System.Collections.Generic;
using System.Text;

namespace Entity_framework_labs
{
    class Person
    {
        public int PersonID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public Address Address { get; set; }
        public DateTime DOB { get; set; }
        public Person() { }

    }
}
