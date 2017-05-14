using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using ConsoleAppK.WCF;
using NUnit.Framework;

namespace ConsoleAppK.Tests
{
    [TestFixture]
    public class WcfTests
    {
        private static ServiceHost host;

        [OneTimeSetUp]
        public void Init()
        {
            host = WcfStartup.CreateServiceHost(Program.WcfBaseAddress);
            host.Open();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            host.Close();
        }

        [Test]
        public void ShouldThrowOnNotFound()
        {
            using (var proxy = new UserInfoProviderProxy(new ServiceEndpoint(ContractDescription.GetContract(typeof(IUserInfoProvider)), new BasicHttpBinding(),
                new EndpointAddress(Program.WcfBaseAddress))))
            {
                Assert.Throws<FaultException<UserNotFound>>(() => proxy.GetUserInfo(Guid.NewGuid()));
            }
        }
    }
}
