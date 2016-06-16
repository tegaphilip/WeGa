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
                response["id"] = p.id.ToString();
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
        public List<string> GetPlayerNicknames()
        {
            return new DBConn().GetPlayerNicknames();
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
            int gameId = new DBConn().CreateGame(SenderNickName, ReceiverNickName, Letters);
            if (gameId != 0)
            {
                response["status"] = SUCCESS;
                response["game_id"] = gameId + "";
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public List<Dictionary<String, String>> GetGameRequests(string nickname)
        {
            return new DBConn().GetGameRequests(nickname);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public List<Dictionary<string, string>> GetGameWordsHistory(int gameId, string nickname)
        {
            return new DBConn().GetGameWordsHistory(gameId, nickname);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public Dictionary<string, string> EndGame(int gameId, string nickname)
        {
            Dictionary<String, String> response = new Dictionary<string, string>();
            response["status"] = ERROR;

            String score = new DBConn().EndGame(gameId, nickname).ToString();
            if (score == "False")
            {
                response["message"] = DBConn.getLatestErrorMessage();
            }
            else
            {
                response["status"] = SUCCESS;
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public Dictionary<string, string> NeglectGame(int gameId, string nickname)
        {
            Dictionary<String, String> response = new Dictionary<string, string>();
            response["status"] = ERROR;

            String score = new DBConn().NeglectGame(gameId, nickname).ToString();
            if (score == "False")
            {
                response["message"] = DBConn.getLatestErrorMessage();
            }
            else
            {
                response["status"] = SUCCESS;
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public Dictionary<string, string> ResignGame(int gameId, string nickname)
        {
            Dictionary<String, String> response = new Dictionary<string, string>();
            response["status"] = ERROR;

            String score = new DBConn().ResignGame(gameId, nickname).ToString();
            if (score == "False")
            {
                response["message"] = DBConn.getLatestErrorMessage();
            }
            else
            {
                response["status"] = SUCCESS;
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Dictionary<string, double> GetLeaderBoard(string type)
        {
            return new DBConn().GetLeaderBoard(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool PingDatabase()
        {
            return new DBConn().PingDatabase();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public List<Dictionary<string, string>> GetResults(string nickname)
        {
            return new DBConn().GetResults(nickname);
        }
    }
}
