using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    class JournalBE
    {
        public string login { get; set; }
        public string  action { get; set; }
        public string date { get; set; }
        public string heure { get; set; }

        public JournalBE()
        {
            login = "";
            action = "";
            date = "";
            heure = "";
        }

        public JournalBE(string login,string action,string date, string heure )
        {
            this.login = login;
            this.action = action;
            this.date = date;
            this.heure = heure;
        }
    }
}
