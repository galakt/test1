using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppK.DataModels;
using LiteDB;

namespace ConsoleAppK.Data
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly LiteDatabase _db;

        public UserInfoRepository()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _db = new LiteDatabase(Path.Combine(baseDirectory, "MyData.db"));
        }

        public UserInfo GetUserInfo(Guid userId)
        {
            var requests = _db.GetCollection<SyncProfileRequest>("Requests");
            var result = requests.FindOne(p => p.UserId == userId);

            if (result == null)
            {
                return null;
            }

            return new UserInfo
            {
                UserId = result.UserId,
                Locale = result.Locale,
                AdvertisingOptIn = result.AdvertisingOptIn,
                CountryIsoCode = result.CountryIsoCode,
                DateModified = result.DateModified
            };
        }
    }
}
