using System;
using LiteDB;

namespace ConsoleAppK.DataModels
{
    public abstract class MyAccountRequestBase
    {
        [BsonId]
        public Guid UserId { get; set; }

        public Guid RequestId { get; set; }
    }
}
