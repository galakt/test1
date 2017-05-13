using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
