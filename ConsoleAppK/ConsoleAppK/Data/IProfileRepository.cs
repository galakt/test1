using ConsoleAppK.DataModels;

namespace ConsoleAppK.Data
{
    public interface IProfileRepository
    {
        bool Upsert(SyncProfileRequest item);
    }
}
