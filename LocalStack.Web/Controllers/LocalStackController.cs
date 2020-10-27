using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocalStack.Service.DynamoDb;
using Microsoft.AspNetCore.Mvc;

namespace LocalStack.Web.Controllers
{
    
    [ApiController]
    [Route("api/v1/local")]
    public class LocalStackController: ControllerBase
    {
        private IDynamoService _service;
        public LocalStackController(IDynamoService service)
        {
            _service = service;
        }
        
        [HttpPost("text")]
        public async Task<ActionResult> WriteToMockDb([FromQuery]string text)
        {
            await _service.WriteToDb(text);

            return Ok();
        }

        [HttpGet("text")]
        public async Task<ActionResult<IEnumerable<string>>> GetFromMockDb()
        {
            return Ok(await _service.GetAllFromDb());
        }
    }
}