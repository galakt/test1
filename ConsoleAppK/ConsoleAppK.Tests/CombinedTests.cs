using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ConsoleAppK.DataModels;
using ConsoleAppK.WCF;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace ConsoleAppK.Tests
{
    [TestFixture]
    public class CombinedTests
    {
        private HttpServer _server;
        private readonly string _url = Program.WebApiBaseAddress;
        private static ServiceHost host;

        [OneTimeSetUp]
        public void Init()
        {
            var config = Startup.CreateConfig();
            _server = new HttpServer(config);
            host = WcfStartup.CreateServiceHost(Program.WcfBaseAddress);
            host.Open();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            _server.Dispose();
            host.Close();
        }

        [Test]
        public void ShouldReturnUserInfoAfterInsert()
        {
            var apiClient = new HttpClient(_server);
            var userId = Guid.NewGuid();
            var reqDate = DateTime.Now;

            var rightPostData = new SyncProfileRequest
            {
                UserId = userId,
                RequestId = Guid.NewGuid(),
                AdvertisingOptIn = true,
                CountryIsoCode = "US",
                Locale = "en-US",
                DateModified = reqDate,
            };
            using (var request = RequestBuilder.CreateRequest(_url, "import.json", "application/json", HttpMethod.Post, rightPostData, new JsonMediaTypeFormatter()))
            using (HttpResponseMessage response = apiClient.SendAsync(request).Result)
            {
                Assert.That(response.StatusCode == HttpStatusCode.OK);
            }

            using (var proxy = new UserInfoProviderProxy(new ServiceEndpoint(ContractDescription.GetContract(typeof(IUserInfoProvider)), new BasicHttpBinding(),
    new EndpointAddress(Program.WcfBaseAddress))))
            {
                var userInfo = proxy.GetUserInfo(userId);

                Assert.That(userInfo != null);
                Assert.That(userInfo.UserId == userId);
                Assert.That(DatesAreEqual(userInfo.DateModified, reqDate));
            }
        }

        [Test]
        public void ShouldReturnNewUserInfoAfterUpsert()
        {
            var apiClient = new HttpClient(_server);
            var userId = Guid.NewGuid();
            var reqDate = DateTime.Now.AddHours(-1).AddDays(-1);

            var rightPostData = new SyncProfileRequest
            {
                UserId = userId,
                RequestId = Guid.NewGuid(),
                AdvertisingOptIn = true,
                CountryIsoCode = "US",
                Locale = "en-US",
                DateModified = reqDate,
            };
            using (var request = RequestBuilder.CreateRequest(_url, "import.json", "application/json", HttpMethod.Post, rightPostData, new JsonMediaTypeFormatter()))
            using (HttpResponseMessage response = apiClient.SendAsync(request).Result)
            {
                Assert.That(response.StatusCode == HttpStatusCode.OK);
            }
            
            var newReqDate = DateTime.Now;
            rightPostData.DateModified = newReqDate;

            using (var request = RequestBuilder.CreateRequest(_url, "import.json", "application/json", HttpMethod.Post, rightPostData, new JsonMediaTypeFormatter()))
            using (HttpResponseMessage response = apiClient.SendAsync(request).Result)
            {
                Assert.That(response.StatusCode == HttpStatusCode.OK);
            }

            using (var proxy = new UserInfoProviderProxy(new ServiceEndpoint(ContractDescription.GetContract(typeof(IUserInfoProvider)), new BasicHttpBinding(),
    new EndpointAddress(Program.WcfBaseAddress))))
            {
                var userInfo = proxy.GetUserInfo(userId);

                Assert.That(userInfo != null);
                Assert.That(userInfo.UserId == userId);
                Assert.That(DatesAreEqual(userInfo.DateModified, newReqDate));
            }
        }

        private bool DatesAreEqual(DateTime d1, DateTime d2)
        {
            // Because of looks like LiteDB round last 4 ticks digits
            return d1 - d2 < TimeSpan.FromSeconds(1);
        }
    }
}
