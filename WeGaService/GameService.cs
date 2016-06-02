using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WeGaService.models;
using WeGaService.libraries;
using System.ServiceModel.Web;

namespace WeGaService
{
    public class GameService : IGameService
    {
        const string ERROR = "error";
        const string SUCCESS = "success";

        public Dictionary<String, String> Login(string username, string password)
        {
            Dictionary<String, String> response = new Dictionary<string, string>();
            response["status"] = ERROR;

            var p = new DBConn().login(username, password);
            if (p == null)
            {
                response["message"] = DBConn.getLatestErrorMessage();
            }
            else
            {
                response["status"] = SUCCESS;
                response["nickname"] = p.nickname;
                response["username"] = p.username;
            }

            return response;
        }

        public bool RegisterPlayer(string username, string nickname, string password)
        {
            return new DBConn().Register(username, nickname, password);
        }

        public List<string> getPlayersNm()
        {
            return new DBConn().getPlayersNm();
        }


        public Dictionary<String, String> CreateGame(string SenderNickName, string ReceiverNickName, string Letters)
        {
            Dictionary<String, String> response = new Dictionary<string, string>();
            response["status"] = ERROR;
            if (new DBConn().CreateGame(SenderNickName, ReceiverNickName, Letters))
            {
                response["status"] = SUCCESS;
            }
            else
            {
                response["message"] = DBConn.getLatestErrorMessage();
            }
            return response;
        }
    }
}
