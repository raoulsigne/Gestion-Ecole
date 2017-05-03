using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    class LigneEtatJournal
    {
        public string num { get; set; }
        public string login { get; set; }
        public string nom { get; set; }
        public string  action { get; set; }
        public string date { get; set; }
        public string heure { get; set; }

        public LigneEtatJournal()
        {
            num = "";
            login = "";
            nom = "";
            action = "";
            date = DateTime.Today.ToShortDateString();
            heure = DateTime.Today.ToShortTimeString();
        }

        public LigneEtatJournal(string num,string login, string nom, string action, string date, string heure)
        {
            this.num = num;
            this.login = login;
            this.nom = nom;
            this.action = action;
            this.date = date;
            this.heure = heure;
        }

    }
}
