using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class ResultatBE
    {
             // declaration des éléments;
        public string codeseq { get; set; }
        public string matricule { get; set; }
        public int annee { get; set; }
        public double point { get; set; }
        public int coef { get; set; }
        public double moyenne { get; set; }
        public int rang { get; set; }
        public double moyenneclasse { get; set; }
        public string mention { get; set; }
        public string remarque { get; set; }
        public string decision { get; set; }
        public string appreciation { get; set; }    
       
        public ResultatBE(string codeS, string matricule, int annee, double point, int coef, double moyenne,
            int rang, double moyenneclasse, string mention, string remarque, string decision, string appreciation)
        {
            this.codeseq = codeS;
            this.matricule = matricule;
            this.annee = annee;
            this.point = point;
            this.coef = coef;
            this.moyenne = moyenne;
            this.rang = rang;
            this.moyenneclasse = moyenneclasse;
            this.mention = mention;
            this.remarque = remarque;
            this.decision = decision;
            this.appreciation = appreciation;
        }

        public ResultatBE()
        {
            this.codeseq = "";
            this.matricule = "";
            this.annee = DateTime.Today.Year;
            this.point = 0;
            this.coef = 0;
            this.moyenne = 0;
            this.rang = 0;
            this.moyenneclasse = 0;
            this.mention = "";
            this.remarque = "";
            this.decision = "";
            this.appreciation = "";
        }
    }
}
