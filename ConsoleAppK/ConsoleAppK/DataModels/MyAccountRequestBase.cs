using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppK.DataModels
{
    public abstract class MyAccountRequestBase
    {
        public Guid UserId { get; set; }

        public Guid RequestId { get; set; }
    }
}
