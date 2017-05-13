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
        public void Post([FromBody]SyncProfileRequest value)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
            }

            using (var db = new LiteDatabase(@"MyData.db"))
            {
                // Get customer collection
                //var customers = db.GetCollection<SyncProfileRequest>("customers");

                //// Create your new customer instance
                //var customer = new Customer
                //{
                //    Name = "John Doe",
                //    Phones = new string[] { "8000-0000", "9000-0000" },
                //    IsActive = true
                //};

                //// Insert new customer document (Id will be auto-incremented)
                //customers.Insert(customer);

                //// Update a document inside a collection
                //customer.Name = "Joana Doe";

                //customers.Update(customer);

                //// Index document using a document property
                //customers.EnsureIndex(x => x.Name);

                //// Use Linq to query documents
                //var results = customers.Find(x => x.Name.StartsWith("Jo"));
            }
        }
    }
}