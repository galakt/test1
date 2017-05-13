using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppK.DataModels;
using LiteDB;

namespace ConsoleAppK.Data
{
    public class ProfileRepository : IProfileRepository
    {
        public ProfileRepository()
        {
        }

        public bool Upsert(SyncProfileRequest item)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var requests = db.GetCollection<SyncProfileRequest>("Requests");

                return requests.Upsert(item);
            }
        }
    }
}
