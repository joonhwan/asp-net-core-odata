using System;
using System.Collections.Generic;

namespace RefitClientExample.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public int NumberOfRecordsOnWishList { get; set; }

        public decimal AmountOfCashToSpend { get; set; }

        // public ICollection<VinylRecord> VinylRecords { get; set; } = new List<VinylRecord>();

        public override string ToString()
        {
            return
                $"{nameof(PersonId)}: {PersonId}, {nameof(Email)}: {Email}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(DateOfBirth)}: {DateOfBirth}, {nameof(Gender)}: {Gender}, {nameof(NumberOfRecordsOnWishList)}: {NumberOfRecordsOnWishList}, {nameof(AmountOfCashToSpend)}: {AmountOfCashToSpend}";
        }
    }
}
