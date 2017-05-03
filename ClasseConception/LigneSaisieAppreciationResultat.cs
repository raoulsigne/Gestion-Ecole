using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;

namespace Ecole.ClasseConception
{
    public class LigneSaisieAppreciationResultat
    {
         public string nomEleve { get; set; }
        public string matricule { get; set; }
        public string Periode { get; set; }
        public int annee { get; set; }
        //public string codeMatiere { get; set; }
        public double moyenne { get; set; }
        public string mention { get; set; }
        public int rang { get; set; }
        public double moyenneClasse { get; set; }
        //public double moyenneMin { get; set; }
        //public double moyenneMax { get; set; }
        public string decision { get; set; }
        public string appreciation { get; set; }

        public EleveBE eleve { get; set; }
       

        public LigneSaisieAppreciationResultat() { 
        }

        public LigneSaisieAppreciationResultat(string nomEleve, string matricule, string Periode, int annee, string codeMatiere, double moyenne,
            string mention, int rang, double moyenneClasse, double moyenneMin, double moyenneMax, string decision, string appreciation)
        {
            this.nomEleve = nomEleve;
            this.matricule = matricule;
            this.Periode = Periode;
            this.annee = annee;
            //this.codeMatiere = codeMatiere;
            this.moyenne = moyenne;
            this.mention = mention;
            this.rang = rang;
            this.moyenneClasse = moyenneClasse;
            //this.moyenneMin = moyenneMin;
            //this.moyenneMax = moyenneMax;
            this.decision = decision;
            this.appreciation = appreciation;
        }
    }
}
