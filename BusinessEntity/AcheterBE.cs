using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class AcheterBE
    {
        public string codesetarticle { get; set; }
        public string matricule { get; set; }
        public string login { get; set; }
        public int annee { get; set; }
        public System.DateTime datAchat { get; set; }
        public decimal montant { get; set; }
        public string dateAchat { get; set; }
        public int quantite { get; set; }

        public virtual EleveBE eleve { get; set; }

        public AcheterBE()
        {
            codesetarticle = "";
            matricule = "";
            login = "";
            annee = DateTime.Today.Year;
            datAchat = DateTime.Today;
            dateAchat = datAchat.ToShortDateString();
            montant = 0;
            quantite = 0;
        }

        public AcheterBE(String article, String matricule, String login, int annee, DateTime date, decimal montant,int quantite)
        {
            this.codesetarticle = article;
            this.matricule = matricule;
            this.login = login;
            this.annee = annee;
            datAchat = date;
            this.montant = montant;
            this.quantite = quantite;
            this.dateAchat = datAchat.ToShortDateString();
        }
    }
}
