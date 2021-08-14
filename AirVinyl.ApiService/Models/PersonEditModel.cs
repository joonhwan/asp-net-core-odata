using System;
using AirVinyl.Entities;

namespace AirVinyl.ApiService.Controllers
{
    public class PersonEditModel
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }
        
        public void Update(Person found)
        {
            if (Email != null) found.Email = Email;
            if (FirstName != null) found.FirstName = FirstName;
            if (LastName != null) found.LastName = LastName;
            if (DateOfBirth != null) found.DateOfBirth = DateOfBirth.Value;
            if (Gender != null) found.Gender = this.Gender.Value;
        }
    }
}