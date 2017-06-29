using Library.Base;
using System;

namespace Library
{
    public class Subscriber : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set;}
        public DateTime DateOfBirth { get; set; }
    }
}
