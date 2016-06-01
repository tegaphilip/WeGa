using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGaService.libraries
{
    class Util
    {
        public static string EncryptPassword(string pass)
        {
            return pass;
        }

        public static bool veriFyPassword(string password, string dbPass)
        {
            return EncryptPassword(password) == dbPass;
        }
    }
}
