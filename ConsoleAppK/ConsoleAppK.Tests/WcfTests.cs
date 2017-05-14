using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
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
            host = new ServiceHost(typeof(UserInfoProvider), new Uri(Program.BaseAddress+ "UserInfoProvider/"));
            host.Open();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            host.Close();
        }

        [Test]
        public void Test1()
        {
            using (var proxy = new UserInfoProviderProxy())
            {
                var h = proxy.GetUserInfo(Guid.NewGuid());
                var hh = 1;

            }
        }
    }
}
