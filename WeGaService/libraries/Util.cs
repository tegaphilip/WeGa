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

        public static bool veriFyPassword(string password, string dbPass)
        {
            return EncryptPassword(password) == dbPass;
        }
    }
}
