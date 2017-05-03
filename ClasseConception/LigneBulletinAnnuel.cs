using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class LigneBulletinAnnuel
    {
        public LigneBulletinAnnuel() {
            this.matricule = "";
            this.codeMat = "";
            this.nomMat = "";
            this.codeprof = "";
            this.nomProf = "";
            this.coef = 0;
            this.codeGroupe = "";
            this.nomGroupe = "";
            this.codeSequence = "";
            this.moyenneSequence = 0;
            this.moyenneAnnuelle = 0;
            this.rangAnnuel = 0;
            this.moyenneClasseAnnuelle = 0; 
            this.moyenneMinClasseAnnuelle = 0; 
            this.moyenneMaxClasseAnnuelle = 0; 
            this.mention = ""; 
            this.totalPointGroupe = 0; 
            this.totalPointMaxGroupe  = 0;        
            this.totalCoefGroupe = 0;
            this.moyenneGroupe = 0;
            this.appreciation = "";

        }

        public LigneBulletinAnnuel(String matricule, String codeMat, String nomMat, String codeprof, String nomProf,
            int coef, String codeGroupe, String nomGroupe, String codeTrimestre, double moyenneTrimestre, double moyenneAnnuelle, int rangAnnuel,
            double moyenneClasseAnnuelle, double moyenneMinClasseAnnuelle, double moyenneMaxClasseAnnuelle, String mention, double totalPointGroupe,
            double totalPointMaxGroupe, double totalCoefGroupe, double moyenneGroupe, string appreciation)
        {
            this.matricule = matricule;
            this.codeMat = codeMat;
            this.nomMat = nomMat;
            this.codeprof = codeprof;
            this.nomProf = nomProf;
            this.coef = coef;
            this.codeGroupe = codeGroupe;
            this.nomGroupe = nomGroupe;
            this.codeSequence = codeTrimestre;
            this.moyenneSequence = moyenneTrimestre;
            this.moyenneAnnuelle = moyenneAnnuelle;
            this.rangAnnuel = rangAnnuel;
            this.moyenneClasseAnnuelle = moyenneClasseAnnuelle;
            this.moyenneMinClasseAnnuelle = moyenneMinClasseAnnuelle;
            this.moyenneMaxClasseAnnuelle = moyenneMaxClasseAnnuelle;
            this.mention = mention;
            this.totalPointGroupe = totalPointGroupe;
            this.totalPointMaxGroupe = totalPointMaxGroupe;
            this.totalCoefGroupe = totalCoefGroupe;
            this.moyenneGroupe = moyenneGroupe;
            this.appreciation = appreciation;
        }

        public String matricule { get; set; } // le matricule de l'élève
        public String codeMat { get; set; } // le code de la matière
        public String nomMat { get; set; } // le nom de la matière
        public String codeprof { get; set; } // le code de l'enseignant
        public String nomProf { get; set; } // le nom de l'enseignant
        public int coef { get; set; } // le coeficient de la matière
        public String codeGroupe { get; set; } // le code du groupe de la matière
        public String nomGroupe { get; set; } // le nom du groupe de la matière
        public String codeSequence { get; set; } // le code du trimestre
        public double moyenneSequence { get; set; } // la moyenne du trimestre
        public double moyenneAnnuelle { get; set; } // le moyenne annuelle
        public int rangAnnuel { get; set; } // le rang annuel
        public double moyenneClasseAnnuelle { get; set; } // la moyenne annuelle de le classe
        public double moyenneMinClasseAnnuelle { get; set; } // la moyenne Annuelle minimale de la classe
        public double moyenneMaxClasseAnnuelle { get; set; } // la moyenne Annuelle de le classe
        public String mention { get; set; } // la mention de la moyenne de l'élève
        public double totalPointGroupe { get; set; } // le total des points du groupe
        public double totalPointMaxGroupe { get; set; } // le total des points Max du groupe        
        public double totalCoefGroupe { get; set; } // la total des coeficient du groupe
        public double moyenneGroupe { get; set; } // la moyenne du groupe de l'élève
        public string appreciation { get; set; } // l'appreciation
    }
}
