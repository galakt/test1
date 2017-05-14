using System;
using ConsoleAppK.Data;
using ConsoleAppK.DataModels;

namespace ConsoleAppK.WCF
{
    public class UserInfoProvider : IUserInfoProvider
    {
        private readonly IUserInfoRepository _userInfoRepository;

        public UserInfoProvider(IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }
        public UserInfo GetUserInfo(Guid userId)
        {
            return _userInfoRepository.GetUserInfo(userId);
        }
    }
}
