using System.Linq;
using System.Threading.Tasks;
using AirVinyl.DataAccess;
using AirVinyl.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Attributes;
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

            return Ok(SingleResult.Create(found)); // ODataController가 제공하는 Response함수
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // add the person to the People collection
            _dbContext.People.Add(person);
            await _dbContext.SaveChangesAsync();

            // return the created person 
            return Created(person); // ODataController가 제공하는 Response함수
        }
        
        [HttpPut]
        public async Task<IActionResult> Put(int key, [FromBody] Delta<Person> person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentPerson = await _dbContext.People
                .FirstOrDefaultAsync(p => p.PersonId == key);

            if (currentPerson == null)
            {
                // 여기서 Upsert() 를 생각해봄직(Key 를 Client에서 만들 수 있다면.)
                return NotFound(); 
            }

            // Put() 은 PersonId 입력이 없는 경우 기본값 0 까지 복사(CopyUnchangedValues()에 의해..)
            person.TrySetPropertyValue(nameof(currentPerson.PersonId), currentPerson.PersonId);
            // Put() 동작
            person.Put(currentPerson);
            
            await _dbContext.SaveChangesAsync();

            return Updated(currentPerson); // ODataController가 제공하는 Response함수
        }
        
        
        [HttpPatch]
        public async Task<IActionResult> Patch(int key, [FromBody] Delta<Person> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentPerson = await _dbContext.People
                .FirstOrDefaultAsync(p => p.PersonId == key);

            if (currentPerson == null)
            {
                return NotFound();
            }

            patch.Patch(currentPerson);
            await _dbContext.SaveChangesAsync();

            return Updated(currentPerson); // ODataController가 제공하는 Response함수
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int key)
        {
            var currentPerson = await _dbContext.People
                .FirstOrDefaultAsync(p => p.PersonId == key);

            if (currentPerson == null)
            {
                return NotFound();
            }

            _dbContext.People.Remove(currentPerson);
            await _dbContext.SaveChangesAsync();
            return NoContent();
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
        
        // Containment
        
        [EnableQuery]
        [HttpGet("odata/v1/People({key})/VinylRecords({vrKey})")]
        public IActionResult GetVinylRecordsOfPerson(int key, int vrKey)
        {
            var found = _dbContext.People.Where(person => person.PersonId == key);
            if (!found.Any())
            {
                return NotFound();
            }
            return Ok(_dbContext.VinylRecords.Where(record => record.PersonId==key && record.VinylRecordId == vrKey));
        }
    }
}