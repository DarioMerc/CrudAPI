using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<ActionResult<List<Record>>> Get()
        {
            
            return Ok(records);
        }
        [HttpPost]
        public async Task<ActionResult<List<Record>>> AddRecord(Record record)
        {
            records.Add(record);
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Record>>> Get(int id)
        {
            var rec = records.Find(x => x.Id == id);
            return Ok(rec);
        }
    }
}
