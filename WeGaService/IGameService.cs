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
        Dictionary<String, String> Login(string username, string password);

        [OperationContract]
        bool RegisterPlayer(string username, string nickname, string password);

        [OperationContract]
        List<string> GetPlayerNicknames();

        [OperationContract]
        Dictionary<String, String> CreateGame(string SenderNickName, string ReceiverNickName, string Letters);

        [OperationContract]
        Dictionary<String, String> SendWord(String wordPlayed, int gameId, string nickname);

        [OperationContract]
        List<Dictionary<String, String>> GetGameRequests(String nickname);

        [OperationContract]
        List<Dictionary<String, String>> GetGameWordsHistory(int gameId, String nickname);

        [OperationContract]
        Dictionary<String, String> EndGame(int gameId, string nickname);

        [OperationContract]
        Dictionary<String, String> NeglectGame(int gameId, string nickname);

        [OperationContract]
        Dictionary<String, String> ResignGame(int gameId, string nickname);

        [OperationContract]
        Dictionary<String, double> GetLeaderBoard(string type);

        [OperationContract]
        bool PingDatabase();
    }
}
