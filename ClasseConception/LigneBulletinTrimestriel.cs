using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class LigneBulletinTrimestriel
    {
        public LigneBulletinTrimestriel() {
            this.matricule = "";
            this.codeMat = "";
            this.nomMat = "";
            this.codeprof = "";
            this.nomProf = "";
            this.coef = 0;
            this.codeGroupe = "";
            this.nomGroupe = "";
            this.codeSeq = "";
            this.moyenneSeq = 0;
            this.moyenneTrim = 0;
            this.rangTrim = 0;
            this.moyenneClasseTrim = 0; 
            this.moyenneMinClasseTrim = 0; 
            this.moyenneMaxClasseTrim = 0; 
            this.mention = ""; 
            this.totalPointGroupe = 0; 
            this.totalPointMaxGroupe  = 0;        
            this.totalCoefGroupe = 0;
            this.moyenneGroupe = 0;
            this.appreciation = "";
        }

        public LigneBulletinTrimestriel(String matricule, String codeMat, String nomMat, String codeprof, String nomProf,
            int coef, String codeGroupe, String nomGroupe, String codeSeq, double moyenneSeq, double moyenneTrim, int rangTrim,
            double moyenneClasseTrim, double moyenneMinClasseTrim, double moyenneMaxClasseTrim, String mention, double totalPointGroupe,
            double totalPointMaxGroupe, double totalCoefGroupe, double moyenneGroupe, String appreciation)
        {
            this.matricule = matricule;
            this.codeMat = codeMat;
            this.nomMat = nomMat;
            this.codeprof = codeprof;
            this.nomProf = nomProf;
            this.coef = coef;
            this.codeGroupe = codeGroupe;
            this.nomGroupe = nomGroupe;
            this.codeSeq = codeSeq;
            this.moyenneSeq = moyenneSeq;
            this.moyenneTrim = moyenneTrim;
            this.rangTrim = rangTrim;
            this.moyenneClasseTrim = moyenneClasseTrim;
            this.moyenneMinClasseTrim = moyenneMinClasseTrim;
            this.moyenneMaxClasseTrim = moyenneMaxClasseTrim;
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
        public String codeSeq { get; set; } // le code de la séquence
        public double moyenneSeq { get; set; } // la moyenne de la séquence
        public double moyenneTrim { get; set; } // le moyenne du trimestre
        public int rangTrim { get; set; } // le rang trimestriel
        public double moyenneClasseTrim { get; set; } // la moyenne trimestrielle de le classe
        public double moyenneMinClasseTrim { get; set; } // la moyenne trimestrielle minimale de la classe
        public double moyenneMaxClasseTrim { get; set; } // la moyenne trimestrielle de le classe
        public String mention { get; set; } // la mention de la moyenne de l'élève
        public double totalPointGroupe { get; set; } // le total des points du groupe
        public double totalPointMaxGroupe { get; set; } // le total des points Max du groupe        
        public double totalCoefGroupe { get; set; } // la total des coeficient du groupe
        public double moyenneGroupe { get; set; } // la moyenne du groupe de l'élève
        public String appreciation { get; set; }

    }
}
