using System;
using System.ServiceModel;
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
            var info = _userInfoRepository.GetUserInfo(userId);
            if (info == null)
            {
                throw new FaultException<UserNotFound>(new UserNotFound(), "Reason: User not found");
            }

            return info;
        }
    }
}
