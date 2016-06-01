using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WeGaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IGameService
    {
        [OperationContract]
        bool Login(string username, string password);

        [OperationContract]
        bool RegisterPlayer(string username, string nickname, string password);
    }
}
