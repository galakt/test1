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
        
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var r = JsonConvert.SerializeObject(new SyncProfileRequest
            {
                UserId = Guid.NewGuid(),
                RequestId = Guid.NewGuid(),
                AdvertisingOptIn = true,
                CountryIsoCode = "RU",
                DateModified = DateTime.Now,
                Locale = "ru"
            });

            return new string[] { "value1", "value2" };
        }
        
        [HttpPost]
        public IHttpActionResult Post([FromBody]SyncProfileRequest value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _profileRepository.Upsert(value);

            return Ok();
        }
    }
}