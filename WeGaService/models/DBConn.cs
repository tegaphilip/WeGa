using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeGaService.libraries;

namespace WeGaService.models
{
    class DBConn
    {
        public static string ErrorMessage = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usn"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public player login(string usn, string pwd)
        {
            try
            {
                using (WegaEntities database = new WegaEntities())
                {
                    var user = database.players.FirstOrDefault(p => p.username == usn);
                    if (user == null)
                    {
                        setErrorMessage("The username and password combination is invalid");
                        return null;
                    }
                    else
                    {
                        if (!Util.veriFyPassword(pwd, user.password))
                        {
                            setErrorMessage("The username and password combination is invalid");
                            return null;
                        }
                        return user;
                    }
                }
            }
            catch (Exception e)
            {
                setErrorMessage(e);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="nickname"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Register(string username, string nickname, string password)
        {
            try
            {
                using (WegaEntities db = new WegaEntities())
                {
                    player p = new player
                    {
                        nickname = nickname,
                        username = username,
                        password = Util.EncryptPassword(password)
                    };

                    // Add the new object to the players collection.
                    db.players.Add(p);
                    // Submit the change to the database.
                    db.SaveChanges();
                    return true;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                setErrorMessage(dbEx);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        private void setErrorMessage(String errorMessage)
        {
            ErrorMessage = errorMessage + "\n";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void setErrorMessage (Exception e)
        {
            ErrorMessage += e.Message + "\n";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbEx"></param>
        private void setErrorMessage(DbEntityValidationException dbEx)
        {
            foreach (var validationErrors in dbEx.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    ErrorMessage += validationError.PropertyName + " >>" + validationError.ErrorMessage + "\n";
                }
            }
        }

<<<<<<< HEAD
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
=======
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string getLatestErrorMessage()
        {
            return ErrorMessage;
>>>>>>> 37c928788076ee788202bf4fc71d081314249bd5
        }
    }
}
