using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private static List<Record> records = new List<Record>{
                new Record {
                    Id = 1,
                    Content = "Foo",
                    Content2 = "Bar"
                },
                new Record {
                    Id = 2,
                    Content = "Fizz",
                    Content2 = "Buzz"
                }
            };
        private readonly DataContext dataContext;

        public RecordController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }


        //ACTIONS
        [HttpGet]
        public async Task<ActionResult<List<Record>>> Get()
        {
            
            return Ok(await dataContext.Records.ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult<List<Record>>> AddRecord(Record record)
        {
            dataContext.Records.Add(record);
            await dataContext.SaveChangesAsync();
            return Ok(await dataContext.Records.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Record>>> Get(int id)
        {
            var rec = await dataContext.Records.FindAsync(id);
            if(rec == null)
            {
                return BadRequest("Record not found.");
            }
            return Ok(rec);
        }
        [HttpPut]
        public async Task<ActionResult<List<Record>>> UpdateRecord(Record request)
        {
            var rec = await dataContext.Records.FindAsync(request.Id);
            if (rec == null)
            {
                return BadRequest("Record not found.");
            }
            rec.Content = request.Content;
            rec.Content2 = request.Content2; 

            return Ok(await dataContext.Records.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Record>>> DeleteRecord(int id)
        {
            var rec = await dataContext.Records.FindAsync(id);
            if (rec == null)
            {
                return BadRequest("Record not found.");
            }
            dataContext.Records.Remove(rec);
            return Ok(await dataContext.Records.ToListAsync());
        }
    }
}
