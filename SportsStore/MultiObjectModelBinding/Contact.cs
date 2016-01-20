using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.MultiObjectModelBinding
{
    public class Contact
    {
        public string Name { get; set; }
        public Address Address { get; set; }
    }
    public class Address
    {
        public string City { get; set; }
    }
}