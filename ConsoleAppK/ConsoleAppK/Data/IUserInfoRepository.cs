using System;
using ConsoleAppK.DataModels;

namespace ConsoleAppK.Data
{
    public interface IUserInfoRepository
    {
        UserInfo GetUserInfo(Guid userId);
    }
}
