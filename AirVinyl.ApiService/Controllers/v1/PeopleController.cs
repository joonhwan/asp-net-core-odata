using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirVinyl.DataAccess;
using AirVinyl.DataAccess.Sqlite;
using AirVinyl.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirVinyl.ApiService.Controllers
{
    [ApiController]
    [Route("/api/v1/people")]
    public class PeopleController : ControllerBase
    {
        private readonly AirVinylDbContext _context;

        public PeopleController(AirVinylDbContext context)
        {
            _context = context;
        }

        [HttpGet()]  
        public async Task<ActionResult<IList<Person>>> Get()
        {
            var result = await _context.People.ToListAsync();
            return Ok(result);
        }

        // 아래 CreatedAtRoute() 에서 사용할 "Route" Name 으로 "GetPerson" 을 지정
        [HttpGet("{personId}",Name="GetPerson")]
        public async Task<ActionResult<Person>> Get(int personId)
        {
            var found = await _context.People.FirstOrDefaultAsync(person => person.PersonId == personId);
            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }

        // 아래 CreatedAtRoute() 에서 사용할 "Route" Name 으로 "GetPerson" 을 지정
        [HttpPost]
        public async Task<ActionResult<PersonViewModel>> Post(PersonForCreation model)
        {
            var entity = model.ToEntity();
            var result = await _context.People.AddAsync(entity);
            // Rest API 에서 Post로 생성된 것에 대해 통상 생성된 Resource에 접근할 수 있는 방법을 
            // 반환하는 ... 
            await _context.SaveChangesAsync();
            return CreatedAtRoute("GetPerson", new { result.Entity.PersonId }, result.Entity);
        }

        [HttpPut("{personId}")]
        public async Task<ActionResult> Put(int personId, PersonEditModel person)
        {
            var found = await _context.People.FirstOrDefaultAsync(person => person.PersonId == personId);
            if (found == null)
            {
                return NotFound();
            }

            person.Update(found);
            _context.People.Update(found);
            await  _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{personId}")]
        public async Task<ActionResult> Delete(int personId)
        {
            var found = await _context.People.FirstOrDefaultAsync(person => person.PersonId == personId);
            if (found == null)
            {
                return NotFound( new {
                    Message = $"No Person of Id(={personId}) !" 
                });
            }

            try
            {
                _context.Remove(found);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                // no-op
            }

            return NoContent();
        }
    }

}