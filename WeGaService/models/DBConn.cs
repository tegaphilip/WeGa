using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeGaService.libraries;

namespace WeGaService.models
{
    class DBConn
    {
        public object login(string usn, string pwd)
        {
            using (WegaEntities database = new WegaEntities())
            {
                var user = database.players.FirstOrDefault(p => p.username == usn && p.password == pwd);
                if (user == null)
                {
                    return null;
                }
                else
                {
                    return user;
                }
            }
        }

        public bool Register(string username, string nickname, string password)
        {
            using (WegaEntities db = new WegaEntities())
            {
                player p = new player
                {
                    nickname = nickname,
                    username = username,
                    password = Util.EncryptPassword(password)
                };

                // Add the new object to the Orders collection.
                db.players.Add(p);

                // Submit the change to the database.
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public List<string> getPlayersNm()
        {
            using (WegaEntities db = new WegaEntities())
            {
                List<player> pl = db.players.ToList();
                List<string> pname = new List<string>();
                foreach (player p in pl)
                {
                    pname.Add(p.nickname);
                }
                return pname;
            }
        }
    }
}
