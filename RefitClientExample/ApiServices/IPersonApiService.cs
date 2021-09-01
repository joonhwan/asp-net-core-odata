using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using RefitClientExample.Models;

namespace RefitClientExample.ApiServices
{
    
    public interface IPersonApiService
    {
        [Get("/people")]
        Task<IList<Person>> GetPeople();

        [Get("/people/{personId}")]
        Task<Person> FindPersonById(int personId);

        [Post("/people")]
        Task<Person> AddPerson([Body(buffered:true)] PersonForCreation person);

        [Delete("/people/{personId}")]
        Task Delete(int personId);
    }
}