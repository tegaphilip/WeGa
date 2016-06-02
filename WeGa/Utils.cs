using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGa
{
    class Utils
    {
        private static string vowelSet = "AEIOU";
        private static string consonantSet = "BCDFGHJKLMNPQRSTVWXYZ";

        public static double getScreenWidth()
        {
            return System.Windows.SystemParameters.PrimaryScreenWidth;
        }

        public static double getScreenHeight()
        {
            return System.Windows.SystemParameters.PrimaryScreenHeight;
        }

        //Method to get shuffled letters.
        public static string getLetters() {
            Random rnd = new Random();
            char c;
            string letters = "";
            do
            {
                c = vowelSet[rnd.Next(0, 5)];
                if (!letters.Contains(c))
                    letters += c;
            } while (letters.Length < 3);
            do
            {
                c = consonantSet[rnd.Next(0, 21)];
                if (!letters.Contains(c))
                    letters += c;
            } while (letters.Length < 7);
            return letters;
        }
    }
}
