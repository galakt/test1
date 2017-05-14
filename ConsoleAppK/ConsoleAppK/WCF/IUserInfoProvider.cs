using System;
using System.ServiceModel;
using ConsoleAppK.DataModels;

namespace ConsoleAppK.WCF
{
    [ServiceContract]
    public interface IUserInfoProvider
    {
        [OperationContract]
        [FaultContract(typeof(UserNotFound))]
        UserInfo GetUserInfo(Guid userId);
    }
}
