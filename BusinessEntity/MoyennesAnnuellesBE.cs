using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class MoyennesAnnuellesBE
    {
        // définition des attributs -----------------------------------------
        // attribut codeMatière     
        public String codeMat { get; set; }            

        // attribut matricule
        public String matricule { get; set; }
       
        // attribut moyenne
        public Double moyenne { get; set; }
       
        // attribut annee
        public int annee { get; set; }

        //champ rang
        public int rang { get; set; }

        //la moyennne de la classe
        public double moyenneClasse { get; set; }

        //la mention de l'élève
        public string mention { get; set; }

        //la moyenne minimale de la classe
        public double moyenneMin { get; set; }

        //la moyenne maximale de la classe de la classe
        public double moyenneMax { get; set; }

        //l'appréciation de l'élève
        public string appreciation { get; set; }
       
        // constructeur de la classe -----------------------------------------
        public MoyennesAnnuellesBE() {
            this.codeMat = "";
            this.matricule = "";
            this.moyenne = 0;
            this.annee = 0;
            this.rang = 0;
            this.moyenneClasse = 0;
            this.mention = "";
            this.moyenneMin = 0;
            this.moyenneMax = 0;
            this.appreciation = "";
        }

        // constructeur avec paramètres --------------------------------------
        public MoyennesAnnuellesBE(String codeMatiere, String matricule, Double moyenne, int annee, int rang, double moyenneClasse, string mention, double moyenneMin, double moyenneMax, string appreciation)
        {
            this.codeMat = codeMatiere;
            this.matricule = matricule;
            this.moyenne = moyenne;
            this.annee = annee;
            this.rang = rang;
            this.moyenneClasse = moyenneClasse;
            this.mention = mention;
            this.moyenneMin = moyenneMin;
            this.moyenneMax = moyenneMax;
            this.appreciation = appreciation;
        }
    }
}
