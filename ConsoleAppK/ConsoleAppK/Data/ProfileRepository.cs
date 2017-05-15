using System;
using System.IO;
using ConsoleAppK.DataModels;
using LiteDB;
using Serilog;

namespace ConsoleAppK.Data
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ILogger _logger;
        private readonly LiteDatabase _db;

        public ProfileRepository(ILogger logger)
        {
            _logger = logger;
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _db = new LiteDatabase(Path.Combine(baseDirectory, "MyData.db"));
            var dbLog = _db.Log;
            dbLog.Level = Logger.ERROR;
            dbLog.Logging += ROnLogging;
        }

        private void ROnLogging(string s)
        {
            _logger.Error($"LiteDatabase error: {s}");
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
