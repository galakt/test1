using System;
using System.IO;
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
            var r = _db.Log;
            r.Level = Logger.FULL;
            r.Logging += ROnLogging;
        }

        private void ROnLogging(string s)
        {
            var r = 1;
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
