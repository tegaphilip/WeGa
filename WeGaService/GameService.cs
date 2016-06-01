using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WeGaService.models;

namespace WeGaService
{
    public class GameService : IGameService
    {
        public bool Login(string username, string password)
        {
            var user = new DBConn().login(username, password);
            return user != null;
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
