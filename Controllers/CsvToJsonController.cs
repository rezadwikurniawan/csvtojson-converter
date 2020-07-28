using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CsvToJsonConverter.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CsvToJsonController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {            
            return Ok("Welcome :)");
        }
    }
}