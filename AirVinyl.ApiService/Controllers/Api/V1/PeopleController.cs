using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirVinyl.DataAccess;
using AirVinyl.DataAccess.Sqlite;
using AirVinyl.Entities;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AirVinyl.ApiService.Controllers.Api.V1
{
    [ApiController]
    [Route("api/v1/people")]
    public class PeopleController : ControllerBase
    {
        private readonly AirVinylDbContextBase _context;
        private readonly ILogger<PeopleController> _logger;

        public PeopleController(AirVinylDbContextBase context, ILogger<PeopleController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // @ OData 희한하게 Action 함수 이름을 OData 규약과 동일하게 하면, **종종**
        //         route 정보가 다른데도, 호출되는 버그가 있다. (2021/8/15. 항상그런것도 아니고, 어느시점에..) 
        [HttpGet]  
        public async Task<ActionResult<IList<Person>>> FindAll() 
        {
            _logger.LogInformation("called {url}", HttpContext.Request.GetDisplayUrl());
            var result = await _context.People.ToListAsync();
            return Ok(result);
        }

        // 아래 CreatedAtRoute() 에서 사용할 "Route" Name 으로 "GetPerson" 을 지정
        [HttpGet("{personId}",Name="GetPerson")]
        public async Task<ActionResult<Person>> Find(int personId)
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
        public async Task<ActionResult<PersonViewModel>> Add(PersonForCreation model)
        {
            var entity = model.ToEntity();
            var result = await _context.People.AddAsync(entity);
            // Rest API 에서 Post로 생성된 것에 대해 통상 생성된 Resource에 접근할 수 있는 방법을 
            // 반환하는 ... 
            await _context.SaveChangesAsync();
            return CreatedAtRoute("GetPerson", new { result.Entity.PersonId }, result.Entity);
        }

        [HttpPut("{personId}")]
        public async Task<ActionResult> Update(int personId, PersonEditModel person)
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
        public async Task<ActionResult> Remove(int personId)
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