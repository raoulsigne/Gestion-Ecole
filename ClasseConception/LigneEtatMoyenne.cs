using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class LigneEtatMoyenne
    {
        public string matricule { get; set; }
        public string nom { get; set; }
        public double note { get; set; }
        public int coef { get; set; }
        public double total { get; set; }
        public int rang { get; set; }
        public string appreciation { get; set; }
        public string appreciationEnseignant { get; set; }

        public LigneEtatMoyenne()
        {
            this.matricule = "";
            this.nom = "";
            this.note = 0;
            this.coef = 0;
            this.total = 0;
            this.rang = 0;
            this.appreciation = "";
            this.appreciationEnseignant = "";
        }

        public LigneEtatMoyenne(string mat, string nom, double note, int coef, double total, int rang, string appreciation, string appreciationEnseignant)
        {
            this.matricule = mat;
            this.nom = nom;
            this.note = note;
            this.coef = coef;
            this.total = total;
            this.rang = rang;
            this.appreciation = appreciation;
            this.appreciationEnseignant = appreciationEnseignant;
        }
    }
}
