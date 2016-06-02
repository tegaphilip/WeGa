using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WeGaService.libraries;

namespace WeGaService.models
{
    class DBConn
    {
        public static String ErrorMessage = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usn"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public player login(String usn, String pwd)
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
                        if (!Util.VeriFyPassword(pwd, user.password))
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
        public bool Register(String username, String nickname, String password)
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

        public List<String> getPlayersNm()
        {
            using (WegaEntities db = new WegaEntities())
            {
                List<player> pl = db.players.ToList();
                List<String> pname = new List<String>();
                foreach (player p in pl)
                {
                    pname.Add(p.nickname);
                }
                return pname;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static String getLatestErrorMessage()
        {
            return ErrorMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="nickname"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Boolean CreateGame(String SenderNickName, String ReceiverNickName, String Letters)
        {
            if (Letters.Distinct().Count() != 7)
            {
                setErrorMessage("The letters must be 7 unique distinct characters");
                return false;
            }

            //todo use a transaction
            try
            {
                using (WegaEntities db = new WegaEntities())
                {
                    var player1 = db.players.FirstOrDefault(p => p.nickname == SenderNickName);
                    var player2 = db.players.FirstOrDefault(p => p.nickname == ReceiverNickName);

                    if (player1 == null || player2 == null)
                    {
                        setErrorMessage("Could not retreve players");
                        return false;
                    }

                    game g = new game
                    {
                        player1_id = player1.id,
                        player2_id = player2.id,
                        date_started = DateTime.Now
                    };

                    db.games.Add(g);
                    db.SaveChanges();

                    game_letters_history glh = new game_letters_history
                    {
                        letters = Letters,
                        game_id = g.id
                    };
                    // Add the new object to the players collection.
                    db.game_letters_history.Add(glh);
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
        /// <param name="wordPlayed"></param>
        /// <param name="gameId"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public int SendWord(String wordPlayed, int gameId, string nickname)
        {
            try
            {
                using (WegaEntities db = new WegaEntities())
                {
                    var gameCheck = db.games.FirstOrDefault(g => g.id == gameId);
                    var playerCheck = db.players.FirstOrDefault(p => p.nickname == nickname);
                    if (gameCheck == null || playerCheck == null)
                    {
                        throw new Exception("Invalid game id or invalid player nickname");
                    }
                    //check id word is valid
                    if (!IsValidWord(wordPlayed))
                    {
                        throw new Exception("Invalid word played");
                    }
                    //check if word was played before
                    word w = getWord(wordPlayed);
                    var alreadyPlayed = db.game_words_history.FirstOrDefault(gwh => gwh.gwh_game_id == gameId && gwh.gwh_player_id == playerCheck.id && gwh.gwh_word_id == w.id);
                    if (alreadyPlayed != null)
                    {
                        throw new Exception("Word previously played");
                    }

                    //otherwise update user score and insert word and 
                    int score = Util.ComputeScore(wordPlayed);
                    if (gameCheck.player1_id == playerCheck.id)
                    {
                        gameCheck.player1_score = (int)gameCheck.player1_score + score;
                    }
                    else if (gameCheck.player2_id == playerCheck.id)
                    {
                        gameCheck.player2_score = (int)gameCheck.player2_score + score;
                    }
                    else
                    {
                        throw new Exception("This player is not playing this game");
                    }

                    game_words_history gameWordHistory = new game_words_history
                    {
                        gwh_datetime = DateTime.Now,
                        gwh_game_id = gameId,
                        gwh_player_id = playerCheck.id,
                        gwh_word_id = w.id
                    };
                    db.game_words_history.Add(gameWordHistory);
                    db.SaveChanges();
                    return score;
                }
            }
            catch (Exception e)
            {
                setErrorMessage(e);
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordToCheck"></param>
        /// <returns></returns>
        public Boolean IsValidWord(String wordToCheck)
        {
            try
            {
                using (WegaEntities db = new WegaEntities())
                {
                    var wordCheck = db.words.FirstOrDefault(w => w.word1 == wordToCheck);
                    if (wordCheck == null)
                    {
                        throw new Exception();
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                setErrorMessage("Invalid word");
                return false;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordToCheck"></param>
        /// <returns></returns>
        public word getWord(string wordToCheck)
        {
            try
            {
                using (WegaEntities db = new WegaEntities())
                {
                    var wordCheck = db.words.FirstOrDefault(w => w.word1 == wordToCheck);
                    if (wordCheck == null)
                    {
                        throw new Exception();
                    }
                    return wordCheck;
                }
            }
            catch (Exception e)
            {
                setErrorMessage("Invalid word");
                return null;
            }
        }
    }
}
