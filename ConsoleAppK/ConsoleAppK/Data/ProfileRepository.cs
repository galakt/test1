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
    public class ProfileRepository : IProfileRepository
    {
        private readonly LiteDatabase _db;

        public ProfileRepository()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _db = new LiteDatabase(Path.Combine(baseDirectory, "MyData.db"));
        }

        public bool Upsert(SyncProfileRequest item)
        {
            using (var t = _db.BeginTrans())
            {
                var requests = _db.GetCollection<SyncProfileRequest>("Requests");
                var result = requests.Upsert(item);
                t.Commit();

                return result;
            }
        }
    }
}
