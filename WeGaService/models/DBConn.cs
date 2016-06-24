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

        const int GAME_TIME = 60;

        const string TYPE_LEADERBOARD_AVERAGE_SCORE = "a";
        const string TYPE_LEADERBOARD_TOTAL_SCORE = "t";

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
                var user = GetPlayerByUsername(usn);
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
                    var guy = db.players.FirstOrDefault(pl => pl.nickname == nickname);
                    if (guy != null)
                    {
                        setErrorMessage("The nickname already exists");
                        return false;
                    }

                    guy = db.players.FirstOrDefault(pl => pl.username == username);
                    if (guy != null)
                    {
                        setErrorMessage("The username already exists");
                        return false;
                    }

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
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void setErrorMessage(Exception e)
        {
            ErrorMessage = e.Message;
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

        public List<String> GetPlayerNicknames()
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
        /// <returns>the game id</returns>
        public int CreateGame(String SenderNickName, String ReceiverNickName, String Letters)
        {
            if (Letters == null || SenderNickName == null || ReceiverNickName == null)
            {
                setErrorMessage("Some of the parameters are invalid");
                return 0;
            }

            if (Letters.Distinct().Count() != 7)
            {
                setErrorMessage("The letters must be 7 unique distinct characters");
                return 0;
            }

            if (SenderNickName.Trim() == ReceiverNickName.Trim())
            {
                setErrorMessage("You cannot play against yourself");
                return 0;
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
                        return 0;
                    }

                    if (HasUnfinishedGame(player1.id, player2.id, db))
                    {
                        setErrorMessage("You have an existing game with this user");
                        return 0;
                    }

                    game g = new game
                    {
                        player1_id = player1.id,
                        player2_id = player2.id,
                        date_started = DateTime.Now,
                        game_time = GAME_TIME
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
                    return g.id;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                setErrorMessage(dbEx);
                return 0;
            }
        }

        private Boolean HasUnfinishedGame(int SenderId, int ReceiverId, WegaEntities db)
        {
            game gam = db.games.FirstOrDefault(g => ((g.player1_id == SenderId && g.player2_id == ReceiverId) || (g.player2_id == SenderId && g.player1_id == ReceiverId)) && g.date_ended == null);
            return gam != null;
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
                if (wordPlayed.Distinct().Count() != wordPlayed.Length)
                {
                    throw new Exception("The word played has some repetive letters and repeated letters are not allowed");
                }
                using (WegaEntities db = new WegaEntities())
                {
                    var currentGame = GetGameById(gameId, db);
                    var currentPlayer = GetPlayerByNickname(nickname, db);

                    //check id word is valid
                    if (!IsValidWord(wordPlayed))
                    {
                        throw new Exception("Invalid word played");
                    }

                    //check if word was played before
                    word w = GetWord(wordPlayed);
                    var alreadyPlayed = db.game_words_history.FirstOrDefault(gwh => gwh.gwh_game_id == gameId && gwh.gwh_player_id == currentPlayer.id && gwh.gwh_word_id == w.id);
                    if (alreadyPlayed != null)
                    {
                        throw new Exception("Word previously played");
                    }

                    //otherwise update user score and insert word and 
                    int score = Util.ComputeScore(wordPlayed);
                    bool player1Ended = currentGame.player1_ended == null ? false : true;
                    bool player2Ended = currentGame.player2_ended == null ? false : true;
                    int player1Score = currentGame.player1_score;
                    int player2Score = currentGame.player2_score == null ? 0 : (int) currentGame.player2_score;

                    if (currentGame.player1_id == currentPlayer.id)
                    {
                        if (player1Ended)
                        {
                            throw new Exception("You have already ended this game");
                        }
                        currentGame.player1_score = player1Score + score;
                    }
                    else if (currentGame.player2_id == currentPlayer.id)
                    {
                        if (player2Ended)
                        {
                            throw new Exception("You have already ended this game");
                        }
                        currentGame.player2_score = player2Score + score;
                    }
                    else
                    {
                        throw new Exception("This player is not playing this game");
                    }

                    game_words_history gameWordHistory = new game_words_history
                    {
                        gwh_datetime = DateTime.Now,
                        gwh_game_id = gameId,
                        gwh_player_id = currentPlayer.id,
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
        public word GetWord(string wordToCheck)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public List<Dictionary<String, String>> GetGameRequests(String nickname)
        {
            try
            {
                using (WegaEntities db = new WegaEntities())
                {
                    var user = GetPlayerByNickname(nickname, db);

                    var requests = (from g in db.games
                                    join glh in db.game_letters_history on g.id equals glh.game_id
                                    into t
                                    from glh in t.DefaultIfEmpty()
                                    join players in db.players on g.player1_id equals players.id
                                    into x
                                    from players in x.DefaultIfEmpty()
                                    where (g.player2_id == user.id && !g.game_neglected && (g.player2_ended == null || g.player2_ended == false))
                                    orderby g.date_started
                                    select new
                                    {
                                        g.id,
                                        g.player1_id,
                                        g.player2_id,
                                        g.winner,
                                        g.player1_score,
                                        g.player2_score,
                                        g.date_ended,
                                        g.date_started,
                                        g.game_neglected,
                                        g.player1_ended,
                                        g.player2_ended,
                                        g.game_time,
                                        g.player1_end_time,
                                        g.player2_end_time,
                                        glh.letters,
                                        players.nickname,
                                    }).ToList();
                    List<Dictionary<String, String>> response = new List<Dictionary<String, String>>();


                    foreach (var res in requests)
                    {
                        Dictionary<String, String> str = new Dictionary<string, string>();
                        str.Add("game_id", res.id.ToString());
                        str.Add("player1_id", res.player2_id.ToString());
                        str.Add("player2_id", res.player2_id.ToString());
                        str.Add("winner", res.winner.ToString());
                        str.Add("player1_score", res.player1_score.ToString());
                        str.Add("player2_score", res.player2_score.ToString());
                        str.Add("date_ended", res.date_ended.ToString());
                        str.Add("date_started", res.date_started.ToString());
                        str.Add("game_neglected", res.game_neglected.ToString());
                        str.Add("player1_ended", res.player1_ended.ToString());
                        str.Add("player2_ended", res.player2_ended.ToString());
                        str.Add("game_time", res.game_time.ToString());
                        str.Add("player1_end_time", res.player1_end_time.ToString());
                        str.Add("player2_end_time", res.player2_end_time.ToString());
                        str.Add("letters", res.letters.ToString());
                        str.Add("nickname", res.nickname.ToString());
                        response.Add(str);
                    }
                    return response;
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
        /// <param name="nickname"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        private player GetPlayerByNickname(String nickname, WegaEntities database)
        {
            var db = database == null ? new WegaEntities() : database;
            var pl = db.players.FirstOrDefault(p => p.nickname == nickname);
            if (pl == null)
            {
                throw new Exception("Invalid nickname");
            }

            return pl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        private player GetPlayerById(int id, WegaEntities database)
        {
            var db = database == null ? new WegaEntities() : database;
            var pl = db.players.FirstOrDefault(p => p.id == id);
            if (pl == null)
            {
                throw new Exception("Invalid player id");
            }

            return pl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private player GetPlayerByUsername(String username)
        {
            var db = new WegaEntities();
            var pl = db.players.FirstOrDefault(p => p.username == username);
            if (pl == null)
            {
                throw new Exception("Invalid username");
            }

            return pl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        private game GetGameById(int id, WegaEntities database)
        {
            var db = database == null ? new WegaEntities() : database;
            var g = db.games.FirstOrDefault(p => p.id == id);
            if (g == null)
            {
                throw new Exception("Invalid game id");
            }

            return g;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public string GetGameLetters(int gameId)
        {
            var db = new WegaEntities();
            var g = db.game_letters_history.FirstOrDefault(p => p.game_id == gameId);
            if (g == null)
            {
                throw new Exception("Invalid game id");
            }

            return g.letters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public List<Dictionary<String, String>> GetGameWordsHistory(int gameId, String nickname)
        {
            try
            {
                using (WegaEntities db = new WegaEntities())
                {
                    var user = GetPlayerByNickname(nickname, db);
                    var requests = (from gwh in db.game_words_history
                                    join w in db.words on gwh.gwh_word_id equals w.id
                                    into t
                                    from w in t.DefaultIfEmpty()
                                    where (gwh.gwh_player_id == user.id && gwh.gwh_game_id == gameId)
                                    orderby gwh.gwh_datetime
                                    select new
                                    {
                                        gwh.gwh_datetime,
                                        w.id,
                                        w.word1,
                                        gwh.gwh_player_id,
                                        gwh.gwh_game_id
                                    }).ToList();
                    List<Dictionary<String, String>> response = new List<Dictionary<String, String>>();


                    foreach (var res in requests)
                    {
                        Dictionary<String, String> str = new Dictionary<string, string>();
                        str.Add("gwh_datetime", res.gwh_datetime.ToString());
                        str.Add("word_id", res.id.ToString());
                        str.Add("word", res.word1.ToString());
                        str.Add("player_id", res.gwh_player_id.ToString());
                        str.Add("game_id", res.gwh_game_id.ToString());
                        str.Add("nickname", user.nickname.ToString());
                        response.Add(str);
                    }
                    return response;
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
        /// <param name="gameId"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public bool EndGame(int gameId, string nickname)
        {
            try
            {
                using (WegaEntities db = new WegaEntities())
                {
                    var currentGame = GetGameById(gameId, db);
                    var currentPlayer = GetPlayerByNickname(nickname, db);

                    //update game for user
                    if (currentGame.player1_id == currentPlayer.id)
                    {
                        currentGame.player1_ended = true;
                        currentGame.player1_end_time = DateTime.Now;
                    }
                    else if (currentGame.player2_id == currentPlayer.id)
                    {
                        currentGame.player2_ended = true;
                        currentGame.player2_end_time = DateTime.Now;
                    }
                    else
                    {
                        throw new Exception("This player is not playing this game");
                    }

                    bool player1Ended = currentGame.player1_ended == null ? false : true;
                    bool player2Ended = currentGame.player2_ended == null ? false : true;
                    bool gameNotNeglected = !currentGame.game_neglected;
                    int player1Score = currentGame.player1_score;
                    int player2Score = currentGame.player2_score == null ? 0 : (int)currentGame.player2_score;

                    if (player1Ended && player2Ended && gameNotNeglected)
                    {
                        //game has ended, set winner
                        if (player1Score > player2Score)
                        {
                            currentGame.winner = currentGame.player1_id;
                        }
                        else if (player2Score > player1Score)
                        {
                            currentGame.winner = currentGame.player2_id;
                        }
                        currentGame.date_ended = DateTime.Now;
                    }

                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                setErrorMessage(e);
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public bool NeglectGame(int gameId, string nickname)
        {
            try
            {
                using (WegaEntities db = new WegaEntities())
                {
                    var currentGame = GetGameById(gameId, db);
                    var currentPlayer = GetPlayerByNickname(nickname, db);

                    //update game for user
                    if (currentGame.player1_id == currentPlayer.id)
                    {
                        throw new Exception("You cannot neglect a game you initiated");
                    }

                    if (currentGame.player2_id == currentPlayer.id)
                    {
                        currentGame.game_neglected = true;
                        currentGame.player2_end_time = DateTime.Now;
                    }
                    else
                    {
                        throw new Exception("This player is not playing this game");
                    }
                    currentGame.date_ended = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                setErrorMessage(e);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public bool ResignGame(int gameId, string nickname)
        {
            try
            {
                using (WegaEntities db = new WegaEntities())
                {
                    var currentGame = GetGameById(gameId, db);
                    var currentPlayer = GetPlayerByNickname(nickname, db);

                    //update game for user and set winner
                    if (currentGame.player1_id == currentPlayer.id)
                    {
                        currentGame.player1_ended = true;
                        currentGame.winner = currentGame.player2_id;
                        currentGame.player1_end_time = DateTime.Now;
                    }
                    else if (currentGame.player2_id == currentPlayer.id)
                    {
                        currentGame.player2_ended = true;
                        currentGame.winner = currentGame.player1_id;
                        currentGame.player2_end_time = DateTime.Now;
                    }
                    else
                    {
                        throw new Exception("This player is not playing this game");
                    }

                    currentGame.date_ended = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                setErrorMessage(e);
                return false;
            }
        }


        /// <summary>
        /// This function is not too optimal
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, double> GetLeaderBoard(String type)
        {
            Dictionary<String, double> response = new Dictionary<string, double>();

            try
            {
                using (WegaEntities db = new WegaEntities())
                {
                    List<player> players = db.players.ToList();
                    foreach (player p in players)
                    {
                        List<game> gamesPlayedAndEnded = db.games
                            .Where(g => (g.player1_id == p.id || g.player2_id == p.id) && (g.winner != null))
                            .ToList();

                        int score = 0;
                        foreach (game eachGame in gamesPlayedAndEnded)
                        {
                            int player1Score = eachGame.player1_score;
                            int player2Score = eachGame.player2_score == null ? 0 : (int)eachGame.player2_score;
                            if (eachGame.player1_id == p.id)
                            {
                                score += player1Score;
                            }
                            else if (eachGame.player2_id == p.id)
                            {
                                score += player2Score;
                            }
                        }
                        if (type == TYPE_LEADERBOARD_AVERAGE_SCORE)
                        {
                            if (gamesPlayedAndEnded.Count == 0)
                            {
                                response.Add(p.nickname, 0);
                            }
                            else
                            {
                                response.Add(p.nickname, score / gamesPlayedAndEnded.Count);
                            }
                            
                        }
                        else if (type == TYPE_LEADERBOARD_TOTAL_SCORE)
                        {
                            response.Add(p.nickname, score);
                        }
                    }

                    return response;
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
        /// <returns></returns>
        public bool PingDatabase()
        {
            try
            {
                using (WegaEntities db = new WegaEntities())
                {
                    try
                    {
                        db.games.FirstOrDefault(g => g.id == 1);
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
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
        /// <param name="nickname"></param>
        /// <returns></returns>
        public List<Dictionary<String, String>> GetResults(String nickname)
        {
            try
            {
                using (WegaEntities db = new WegaEntities())
                {
                    var user = GetPlayerByNickname(nickname, db);

                    var requests = (from g in db.games
                                    join glh in db.game_letters_history on g.id equals glh.game_id
                                    into t
                                    from glh in t.DefaultIfEmpty()
                                    join players in db.players on g.player1_id equals players.id
                                    into x
                                    from players in x.DefaultIfEmpty()
                                    join players2 in db.players on g.player2_id equals players2.id
                                    into y
                                    from players2 in y.DefaultIfEmpty()
                                    let nickname2 =  players2.nickname
                                    where (g.player1_id == user.id || g.player2_id == user.id)
                                    orderby g.date_started
                                    select new
                                    {
                                        g.id,
                                        g.player1_id,
                                        g.player2_id,
                                        g.winner,
                                        g.player1_score,
                                        g.player2_score,
                                        g.date_ended,
                                        g.date_started,
                                        g.game_neglected,
                                        g.game_time,
                                        glh.letters,
                                        players.nickname,
                                        nickname2,
                                    }).ToList();
                    List<Dictionary<String, String>> response = new List<Dictionary<String, String>>();


                    foreach (var res in requests)
                    {
                        Dictionary<String, String> str = new Dictionary<string, string>();
                        str.Add("game_id", res.id.ToString());
                        str.Add("player1_id", res.player2_id.ToString());
                        str.Add("player2_id", res.player2_id.ToString());
                        str.Add("winner", res.winner.ToString());
                        str.Add("player1_score", res.player1_score.ToString());
                        str.Add("player2_score", res.player2_score.ToString());
                        str.Add("date_ended", res.date_ended.ToString());
                        str.Add("date_started", res.date_started.ToString());
                        str.Add("game_neglected", res.game_neglected.ToString());
                        str.Add("game_time", res.game_time.ToString());
                        str.Add("letters", res.letters.ToString());
                        str.Add("nickname", res.nickname.ToString());
                        str.Add("nickname2", res.nickname2.ToString());
                        response.Add(str);
                    }
                    return response;
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
        /// <param name="gameId"></param>
        /// <returns></returns>
        public Dictionary<String, List<String>> GetGameDetails(int gameId)
        {
            try
            {
                using (WegaEntities db = new WegaEntities())
                {
                    var gameObject = GetGameById(gameId, db);
                    var player1 = GetPlayerById(gameObject.player1_id, db);
                    var player2 = GetPlayerById(gameObject.player2_id, db);

                    var player1Words = (from gwh in db.game_words_history
                                    join w in db.words on gwh.gwh_word_id equals w.id
                                    into t
                                    from w in t.DefaultIfEmpty()
                                    where (gwh.gwh_player_id == gameObject.player1_id && gwh.gwh_game_id == gameObject.id)
                                    orderby gwh.gwh_datetime
                                    select w.word1).ToList();
                    var player2Words = (from gwh in db.game_words_history
                                join w in db.words on gwh.gwh_word_id equals w.id
                                into t
                                from w in t.DefaultIfEmpty()
                                where (gwh.gwh_player_id == gameObject.player2_id && gwh.gwh_game_id == gameObject.id)
                                orderby gwh.gwh_datetime
                                select w.word1).ToList();
                    Dictionary<String, List<String>> response = new Dictionary<String, List<String>>();
                    response.Add(player1.nickname, player1Words);
                    response.Add(player2.nickname, player2Words);
                    return response;
                }
            }
            catch (Exception e)
            {
                setErrorMessage(e);
                return null;
            }
        }

    }
}
