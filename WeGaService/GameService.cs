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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="nickname"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool RegisterPlayer(string username, string nickname, string password)
        {
            return new DBConn().Register(username, nickname, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> getPlayersNm()
        {
            return new DBConn().getPlayersNm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SenderNickName"></param>
        /// <param name="ReceiverNickName"></param>
        /// <param name="Letters"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordPlayed"></param>
        /// <param name="gameId"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public Dictionary<string, string> SendWord(string wordPlayed, int gameId, string nickname)
        {
            Dictionary<String, String> response = new Dictionary<string, string>();
            response["status"] = ERROR;

            String score = new DBConn().SendWord(wordPlayed, gameId, nickname).ToString();
            response["value"] = score;
            if (score == "0")
            {
                response["message"] = DBConn.getLatestErrorMessage();
            }
            else
            {
                response["status"] = SUCCESS;
            }
            return response;
        }
    }
}
