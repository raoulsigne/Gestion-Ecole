using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class SanctionnerBE
    {
             // declaration des éléments;
        public string codesanction { get; set; }
        public string matricule { get; set; }
        public int annee { get; set; }
        public int quantité { get; set; }
        public DateTime datesanction { get; set; }
        public string date { get; set; }
        public string sequence { get; set; }
        public string etat { get; set; }

        public SanctionnerBE()
        {
            this.codesanction = "";
            this.matricule = "";
            this.annee = 0;
            this.quantité = 0;
            this.datesanction = DateTime.Today;
            this.sequence = "";
            this.etat = "";
            this.date = "";
        }
       
        public SanctionnerBE(string codeS, string matricule, int annee, int quantite, DateTime dateS,string seq, string etat)
        {
            this.codesanction = codeS;
            this.matricule = matricule;
            this.annee = annee;
            this.quantité = quantite;
            this.datesanction = dateS;
            this.sequence = seq;
            this.etat = etat;
            this.date = dateS.ToShortDateString();
        }
    }
}
