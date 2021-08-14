using System;
using System.ComponentModel.DataAnnotations;
using AirVinyl.Entities;

namespace AirVinyl.ApiService.Controllers
{
    public class PersonViewModel
    {
        public int PersonId { get; set; }
        
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public int NumberOfRecordsOnWishList { get; set; }

        public decimal AmountOfCashToSpend { get; set; }
        
        public static PersonViewModel From(Person entity)
        {
            return new PersonViewModel
            {
                PersonId = entity.PersonId,
                Email = entity.Email,
                Gender = entity.Gender,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
                AmountOfCashToSpend = entity.AmountOfCashToSpend,
                NumberOfRecordsOnWishList = entity.NumberOfRecordsOnWishList
            };
        }
    }
}