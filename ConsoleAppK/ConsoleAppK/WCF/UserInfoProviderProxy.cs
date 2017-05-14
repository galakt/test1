using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppK.DataModels;

namespace ConsoleAppK.WCF
{
    public class UserInfoProviderProxy : ClientBase<IUserInfoProvider>, IUserInfoProvider
    {
        public UserInfo GetUserInfo(Guid userId)
        {
            return base.Channel.GetUserInfo(userId);
        }
    }
}
