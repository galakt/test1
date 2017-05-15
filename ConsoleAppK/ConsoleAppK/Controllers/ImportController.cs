using System;
using System.Web.Http;
using ConsoleAppK.Data;
using ConsoleAppK.DataModels;
using Serilog;

namespace ConsoleAppK.Controllers
{
    [Route("import.json")]
    public class ImportController : ApiController
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ILogger _logger;

        public ImportController(IProfileRepository profileRepository, ILogger logger)
        {
            _profileRepository = profileRepository;
            _logger = logger;
        }
        
        [HttpPost]
        public IHttpActionResult Post([FromBody]SyncProfileRequest value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _profileRepository.Upsert(value);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Exception while upsert");
            }

            return InternalServerError();
        }
    }
}