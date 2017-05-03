using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_Ecole.BusinessEntity
{
    class AcheterBE
    {
        public string codesetarticle { get; set; }
        public string matricule { get; set; }
        public string login { get; set; }
        public int annee { get; set; }
        public System.DateTime datAchat { get; set; }

        public virtual Eleve eleve { get; set; }

        public AcheterBE()
        {
            codesetarticle = "";
            matricule = "";
            login = "";
            annee = DateTime.Today.Year;
            datAchat = DateTime.Today;
        }

        public AcheterBE(String article, String matricule, String login, int annee, DateTime date)
        {
            this.codesetarticle = article;
            this.matricule = matricule;
            this.login = login;
            this.annee = annee;
            datAchat = date;
        }
    }
}
