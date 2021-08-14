using System;
using AirVinyl.Entities;

namespace AirVinyl.ApiService.Controllers
{
    public class PersonForCreation
    {
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }

        public Person ToEntity()
        {
            return new()
            {
                Email = Email,
                Gender = Gender,
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth
            };
        }
    }
}