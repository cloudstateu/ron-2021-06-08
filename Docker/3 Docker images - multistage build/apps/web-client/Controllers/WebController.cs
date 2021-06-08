using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_client;

namespace web_client.Controllers
{
    [Route("")]
    [Route("api/[controller]")]
    [ApiController]
    public class WebController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public  ActionResult<string> Get()
        {
            try
            {
                var response = String.Format("Service  response: {0}", DateTime.Now);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "No data";
            }
        }
    }
}
