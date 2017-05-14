using System;
using System.Collections.Generic;
using System.Web.Http;
using ConsoleAppK.Data;
using ConsoleAppK.DataModels;
using LiteDB;
using Newtonsoft.Json;

namespace ConsoleAppK.Controllers
{
    [Route("import.json")]
    public class ImportController : ApiController
    {
        private readonly IProfileRepository _profileRepository;

        public ImportController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        
        [HttpPost]
        public IHttpActionResult Post([FromBody]SyncProfileRequest value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_profileRepository.Upsert(value))
            {
                return Ok();
            }

            return InternalServerError();
        }
    }
}