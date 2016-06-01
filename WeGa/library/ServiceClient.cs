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

       public List<string> getPlayersNm()
        {
            try
            {
                return proxy.getPlayersNm().ToList<string>();
            }
            catch
            {
                return null;
            }
        }
    }
}
