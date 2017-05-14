using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Formatting;
using ConsoleAppK.DataModels;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace ConsoleAppK.Tests
{
    [TestFixture]
    public class WebApiTests
    {
        private HttpServer _server;
        private readonly string _url = Program.WebApiBaseAddress; 

        [OneTimeSetUp]
        public void Init()
        {
            var config = Startup.CreateConfig();
            _server = new HttpServer(config);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            _server.Dispose();
        }

        [Test]
        public void ShouldReturnOkOnRightPost()
        {
            var sut = new HttpClient(_server);

            var rightPostData = new SyncProfileRequest
            {
                UserId = Guid.NewGuid(),
                RequestId = Guid.NewGuid(),
                AdvertisingOptIn = true,
                CountryIsoCode = "US",
                Locale = "en-US",
                DateModified = DateTime.Now,
            };
            using (var request = RequestBuilder.CreateRequest(_url, "import.json", "application/json", HttpMethod.Post, rightPostData, new JsonMediaTypeFormatter()))
            using (HttpResponseMessage response = sut.SendAsync(request).Result)
            {
                Assert.That(response.StatusCode == HttpStatusCode.OK);
            }
        }


        [Test] 
        public void ShouldReturnBadRequestOnBadCountryIsoCode()
        {
            var sut = new HttpClient(_server);

            var rightPostData = new SyncProfileRequest
            {
                UserId = Guid.NewGuid(),
                RequestId = Guid.NewGuid(),
                AdvertisingOptIn = true,
                CountryIsoCode = "USS",
                Locale = "en-US",
                DateModified = DateTime.Now,
            };
            using (var request = RequestBuilder.CreateRequest(_url, "import.json", "application/json", HttpMethod.Post, rightPostData, new JsonMediaTypeFormatter()))
            using (HttpResponseMessage response = sut.SendAsync(request).Result)
            {
                Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
            }
        }


        [Test]
        public void ShouldReturnBadRequestOnBadLocale()
        {
            var sut = new HttpClient(_server);

            var rightPostData = new SyncProfileRequest
            {
                UserId = Guid.NewGuid(),
                RequestId = Guid.NewGuid(),
                AdvertisingOptIn = true,
                CountryIsoCode = "US",
                Locale = "enUS",
                DateModified = DateTime.Now,
            };
            using (var request = RequestBuilder.CreateRequest(_url, "import.json", "application/json", HttpMethod.Post, rightPostData, new JsonMediaTypeFormatter()))
            using (HttpResponseMessage response = sut.SendAsync(request).Result)
            {
                Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
            }
        }
    }
}
