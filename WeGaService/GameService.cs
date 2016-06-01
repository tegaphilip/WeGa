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
            Dictionary<String, String> user = new Dictionary<string, string>();
            user["status"] = ERROR;

            var p = new DBConn().login(username, password);
            if (p == null)
            {
                user["message"] = DBConn.getLatestErrorMessage();
            }
            else
            {
                user["status"] = SUCCESS;
                user["nickname"] = p.nickname;
                user["username"] = p.username;
            }

            return user;
        }

        public bool RegisterPlayer(string username, string nickname, string password)
        {
            return new DBConn().Register(username, nickname, password);
        }

        public List<string> getPlayersNm()
        {
            return new DBConn().getPlayersNm();
        }
    }
}
