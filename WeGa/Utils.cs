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
        public static string getLetters()
        {
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

            return new string(letters.ToCharArray().OrderBy(s => (rnd.Next(2) % 2) == 0).ToArray());

            //return letters;
        }

        public static string[] RandomizeStrings(string[] arr)
        {
            Random _random = new Random();
            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
            // Add all strings from array
            // Add new random int each time
            foreach (string s in arr)
            {
                list.Add(new KeyValuePair<int, string>(_random.Next(), s));
            }
            // Sort the list by the random number
            var sorted = from item in list
                         orderby item.Key
                         select item;
            // Allocate new string array
            string[] result = new string[arr.Length];
            // Copy values to array
            int index = 0;
            foreach (KeyValuePair<int, string> pair in sorted)
            {
                result[index] = pair.Value;
                index++;
            }
            // Return copied array
            return result;
        }
    }
}
