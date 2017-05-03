using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ecole.UI
{
    public class Utilitaires
    {
        public static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        public static bool formatTelephone(string text)
        {
            Regex regex = new Regex("[^0-9,/,+]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
    }
}
