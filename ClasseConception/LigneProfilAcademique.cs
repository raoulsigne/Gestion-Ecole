using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class LigneProfilAcademique
    {
        public LigneProfilAcademique()
        {
            
        }

        public LigneProfilAcademique(String matricule, int annee, String codeClasse, String codeMatiere, String codeSequence,
            String codeTrimestre, double moyenne, String mention, int coef, int rang, double moyenneClasse, double moyenneMin,
            double moyenneMax, double totalPoint, double moyenneAnnuelle, String mentionAnnuelle, int rangAnnuel, double moyenneAnnuelleClasse)
        {
            this.matricule = matricule;
            this.annee = annee;
            this.codeClasse = codeClasse;

            this.codeMatiere = codeMatiere;
            this.codeSequence = codeSequence;
            this.codeTrimestre = codeTrimestre;
            this.moyenne = moyenne;
            this.mention = mention;
            this.coef = coef;
            this.rang = rang;
            this.moyenneClasse = moyenneClasse;
            this.moyenneMin = moyenneMin;
            this.moyenneMax = moyenneMax;
            this.totalPoint = totalPoint;
            this.moyenneAnnuelle = moyenneAnnuelle;
            this.mentionAnnuelle = mentionAnnuelle;
            this.rangAnnuel = rangAnnuel;
            this.moyenneAnnuelleClasse = moyenneAnnuelleClasse;
        }

        public String matricule { get; set; } // le maatricule de l'élève
        public int annee { get; set; }
        public String codeClasse { get; set; }
        public String codeMatiere { get; set; }
        public String codeSequence { get; set; }
        public String codeTrimestre { get; set; }
        public double moyenne { get; set; } //La moyenne séquentielle de la matière
        public String mention { get; set; }
        public int coef { get; set; }
        public int rang { get; set; }
        public double moyenneClasse { get; set; }
        public double moyenneMin { get; set; }
        public double moyenneMax { get; set; }
        public double totalPoint { get; set; } //la somme totale des point (pour toutes les matières) de l'élève pour l'année
        public double moyenneAnnuelle { get; set; }
        public String mentionAnnuelle { get; set; }
        public int rangAnnuel { get; set; }
        public double moyenneAnnuelleClasse { get; set; }
    }
}
