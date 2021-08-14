using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirVinyl.DataAccess;
using AirVinyl.DataAccess.Sqlite;
using AirVinyl.Entities;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace AirVinyl.ApiService.Controllers.OData.V1
{
    public class PeopleController : ODataController
    {
        private readonly AirVinylDbContextBase _dbContext;

        public PeopleController(AirVinylDbContextBase dbContext)
        {
            _dbContext = dbContext;
        }

        // [HttpGet()]  
        // public async Task<ActionResult<IList<Person>>> Get()
        // {
        //     var result = await _context.People.ToListAsync();
        //     return Ok(result);
        // }
        [HttpGet]
        [EnableQuery]
        public IActionResult Get() 
        {
            return Ok(_dbContext.People);
        }
        
        //
        //
        // // 아래 CreatedAtRoute() 에서 사용할 "Route" Name 으로 "GetPerson" 을 지정
        // [EnableQuery]
        // [HttpGet] // "{personId}" 이런게 필요없다. EDM 모델에서 확인되니까. 대신, 반드시 인자명은 "key" 로 해야 함
        // public IActionResult Get(int key)
        // {
        //     var found = _context.People.FirstOrDefault(person => person.PersonId == key);
        //     if (found == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(found);
        // }
        //
        // [HttpGet("odata/v1/People({key})/VinylRecords")]
        // public IActionResult GetVinylRecordsOfPerson(int key)
        // {
        //     var reqUrl = new Uri(HttpContext.Request.GetEncodedUrl());
        //
        //     var found = _context.People
        //             .Include(person => person.VinylRecords)
        //             .FirstOrDefault(person => person.PersonId == key)
        //         ;
        //     if (found == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(found.VinylRecords);
        // }
        
        // // 아래 CreatedAtRoute() 에서 사용할 "Route" Name 으로 "GetPerson" 을 지정
        // [HttpPost]
        // public async Task<ActionResult<PersonViewModel>> Post(PersonForCreation model)
        // {
        //     var entity = model.ToEntity();
        //     var result = await _context.People.AddAsync(entity);
        //     // Rest API 에서 Post로 생성된 것에 대해 통상 생성된 Resource에 접근할 수 있는 방법을 
        //     // 반환하는 ... 
        //     await _context.SaveChangesAsync();
        //     return CreatedAtRoute("GetPerson", new { result.Entity.PersonId }, result.Entity);
        // }
        //
        // [HttpPut("{personId}")]
        // public async Task<ActionResult> Put(int personId, PersonEditModel person)
        // {
        //     var found = await _context.People.FirstOrDefaultAsync(person => person.PersonId == personId);
        //     if (found == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     person.Update(found);
        //     _context.People.Update(found);
        //     await  _context.SaveChangesAsync();
        //
        //     return NoContent();
        // }
        //
        // [HttpDelete("{personId}")]
        // public async Task<ActionResult> Delete(int personId)
        // {
        //     var found = await _context.People.FirstOrDefaultAsync(person => person.PersonId == personId);
        //     if (found == null)
        //     {
        //         return NotFound( new {
        //             Message = $"No Person of Id(={personId}) !" 
        //         });
        //     }
        //
        //     try
        //     {
        //         _context.Remove(found);
        //         await _context.SaveChangesAsync();
        //         return NoContent();
        //     }
        //     catch (Exception)
        //     {
        //         // no-op
        //     }
        //
        //     return NoContent();
        // }

    }
}