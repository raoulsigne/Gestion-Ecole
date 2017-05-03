using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class LigneBulletinSequentiel
    {
        public LigneBulletinSequentiel() {
            this.matricule = "";
            this.codeMat = "";
            this.nomMat = "";
            this.codeprof = "";
            this.nomProf = "";
            this.coef = 0;
            this.codeGroupe = "";
            this.nomGroupe = "";
            this.codeSeq = "";
            this.codeEvaluation = "";
            this.noteEvaluation = null;
            this.moyenneSequence = 0;
            this.rangSequentiel = 0;
            this.moyenneSeqClasse = 0; 
            this.moyenneSeqMin = 0; 
            this.moyenneSeqMax = 0; 
            this.mention = ""; 
            this.totalPointGroupe = 0; 
            this.totalPointMaxGroupe  = 0;        
            this.totalCoefGroupe = 0;
            this.moyenneGroupe = 0;
            this.appreciation = "";
        }

        public LigneBulletinSequentiel(String matricule, String codeMat, String nomMat, String codeprof, String nomProf,
            int coef, String codeGroupe, String nomGroupe, String codeSeq, String codeEvaluation, String noteEvaluation, double moyenneSequence, int rangSequentiel,
            double moyenneSeqClasse, double moyenneSeqMin, double moyenneSeqMax, String mention, double totalPointGroupe,
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

            this.codeEvaluation = codeEvaluation;
            this.noteEvaluation = noteEvaluation;
            this.moyenneSequence = moyenneSequence;
            this.rangSequentiel = rangSequentiel;
            this.moyenneSeqClasse = moyenneSeqClasse;
            this.moyenneSeqMin = moyenneSeqMin;
            this.moyenneSeqMax = moyenneSeqMax; 

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

        public String codeEvaluation { get; set; } //le code de l'évaluation
        public String noteEvaluation { get; set; } // la moyenne de l'évaluation
        public double moyenneSequence { get; set; } // la moyenne de la séquence
        public int rangSequentiel { get; set; }  // le rang séquentiel
        public double moyenneSeqClasse { get; set; } // la moyenne séquentielle de le classe
        public double moyenneSeqMin { get; set; } // la moyenne séquentielle minimale de le classe
        public double moyenneSeqMax { get; set; } // la moyenne séquentielle maximale de le classe

        public String mention { get; set; } // la mention de la moyenne de l'élève

        public double totalPointGroupe { get; set; } // le total des points du groupe
        public double totalPointMaxGroupe { get; set; } // le total des points Max du groupe        
        public double totalCoefGroupe { get; set; } // la total des coeficient du groupe
        public double moyenneGroupe { get; set; } // la moyenne du groupe de l'élève
        public String appreciation { get; set; } // l'appréciation de la moyenne de l'élève
    }
}
