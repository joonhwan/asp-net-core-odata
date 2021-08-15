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
using Microsoft.AspNetCore.OData.Results;
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
        // [EnableQuery(PageSize = 2)] // --> Server Side Paging
        [EnableQuery]
        public IActionResult Get() 
        {
            return Ok(_dbContext.People);
        }
        
        // 아래 CreatedAtRoute() 에서 사용할 "Route" Name 으로 "GetPerson" 을 지정
        // [EnableQuery(MaxExpansionDepth = 3, MaxSkip = 11, MaxTop = 10)] // --> 개별 Action의 ODat 질의 제한 가능.
        [EnableQuery]
        [HttpGet] // "{personId}" 이런게 필요없다. EDM 모델에서 확인되니까. 대신, 반드시 인자명은 "key" 로 해야 함
        public IActionResult Get(int key)
        {
            // var found = _dbContext.People.FirstOrDefault(person => person.PersonId == key);
            // if (found == null)
            // {
            //     return NotFound();
            // }
            // return Ok(found);
            
            // 위 코드와 아래코드의 차이점.
            // --> $select 등 OData Query Parameter에 의해 실제 DB에 질의하는 질의문이 , 클라이언트측 
            //     요청에 의해 더 tuning된다. (예: 필요한 필드만 SELECT 한다)

            var found = _dbContext.People.Where(person => person.PersonId == key);
            if (!found.Any())
            {
                return NotFound();
            }

            return Ok(SingleResult.Create(found));
        }
        
        [EnableQuery]
        [HttpGet("odata/v1/People({key})/VinylRecords")]
        public IActionResult GetVinylRecordsOfPerson(int key)
        {
            // var found = _dbContext.People
            //         .Include(person => person.VinylRecords)
            //         .FirstOrDefault(person => person.PersonId == key)
            //     ;
            // if (found == null)
            // {
            //     return NotFound();
            // }
            // return Ok(found.VinylRecords);
            
            // ---- 
            // DB 쿼리 개선되도록 수정하면...
            // ----

            var found = _dbContext.People.Where(person => person.PersonId == key);
            if (!found.Any())
            {
                return NotFound();
            }
            return Ok(_dbContext.VinylRecords.Where(record => record.PersonId==key));
        }
        
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