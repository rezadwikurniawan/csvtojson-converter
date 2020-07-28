using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvToJsonConverter.Model;
using CsvToJsonConverter.Services;
using CsvToJsonConverter.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CsvToJsonConverter.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CsvToJsonController : ControllerBase
    {

        private readonly IConverterService _service;
        public CsvToJsonController(IConverterService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Get()
        {            
            return Ok("Welcome :)");
        }
         [HttpPost]
        public ActionResult Post([FromBody] RequestModel request)
        {
            if (request.content == string.Empty || !Helper.IsBase64String(request.content))
            {
                return BadRequest("Not a valid content");
            }
            return Ok(_service.ConvertCsvToJson(request));            
        }
    }
}