using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WeGa.GameServiceReference;

namespace WeGa.library
{
    class ServiceClient
    {
        private GameServiceClient client;
        private IGameService proxy;
        private ChannelFactory<IGameService> channelFactory;

        public ServiceClient()
        {
            client = new GameServiceClient();
            channelFactory = new ChannelFactory<IGameService>("BasicHttpBinding_IGameService");
            proxy = channelFactory.CreateChannel();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Dictionary<String, String> Login(string username, string password)
        {
            try
            {
                return proxy.Login(username, password);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public bool Register(string username, string password, string nickname)
        {
            try
            {
                return proxy.RegisterPlayer(username, nickname, password);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> GetPlayerNicknames()
        {
            try
            {
                return proxy.GetPlayerNicknames().ToList<string>();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Dictionary<String, String> CreateGame(string senderNickName, string receiverNickName, string letters)
        {
            try
            {
                return proxy.CreateGame(senderNickName, receiverNickName, letters);
            }
            catch
            {
                return null;
            }
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
            try
            {
                return proxy.SendWord(wordPlayed, gameId, nickname);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public Dictionary<string, string>[] GetGameRequests(string nickname)
        {
            try
            {
                return proxy.GetGameRequests(nickname);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public Dictionary<string, string>[] GetGameWordsHistory(int gameId, string nickname)
        {
            try
            {
                return proxy.GetGameWordsHistory(gameId, nickname);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public Dictionary<string, string> EndGame(int gameId, string nickname)
        {
            try
            {
                return proxy.EndGame(gameId, nickname);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public Dictionary<string, string> NeglectGame(int gameId, string nickname)
        {
            try
            {
                return proxy.NeglectGame(gameId, nickname);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public Dictionary<string, string> ResignGame(int gameId, string nickname)
        {
            try
            {
                return proxy.ResignGame(gameId, nickname);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Dictionary<string, double> GetLeaderBoard(string type)
        {
            try
            {
                return proxy.GetLeaderBoard(type);
            }
            catch
            {
                return null;
            }
        }
    }
}
