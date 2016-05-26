using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGa
{
    class Utils
    {
        public static double getScreenWidth()
        {
            return System.Windows.SystemParameters.PrimaryScreenWidth;
        }

        public static double getScreenHeight()
        {
            return System.Windows.SystemParameters.PrimaryScreenHeight;
        }
    }
}
