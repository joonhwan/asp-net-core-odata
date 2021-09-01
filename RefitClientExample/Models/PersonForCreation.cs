using System;

namespace RefitClientExample.Models
{
    public class PersonForCreation
    {
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
    }
}