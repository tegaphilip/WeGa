using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace WeGaService.libraries
{
    class Util
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string EncryptPassword(string pass)
        {
            SHA256Managed crypt = new SHA256Managed();
            StringBuilder hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(pass), 0, Encoding.UTF8.GetByteCount(pass));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();

            //return pass;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="dbPass"></param>
        /// <returns></returns>
        public static bool VeriFyPassword(string password, string dbPass)
        {
            return EncryptPassword(password) == dbPass;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static int ComputeScore(String word)
        {
            int score = word.Length * 10;
            if (word.Length == 7)
            {
                score += 50;
            }
            else if (word.Length == 6)
            {
                score += 25;
            }

            return score;
        }
    }
}
