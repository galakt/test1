using System.Collections.Generic;
using System.Web.Http;

namespace ConsoleAppK.Controllers
{
    [Route("import.json")]
    public class ImportController : ApiController
    {
        public ImportController()
        {
            
        }
        
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}