using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppK.DataModels;

namespace ConsoleAppK.Data
{
    public interface IProfileRepository
    {
        void Upsert(SyncProfileRequest item);
    }
}
