using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class NotesBE
    {
        // définition des attributs -----------------------------------------
        // attribut matricule
        public String matricule { get; set; }
 
        // attribut codeMatière
        public String codeMat { get; set; }

        // attribut codeSequence
        public String codeSeq { get; set; }

        // attribut codeEvaluation
        public String codeEvaluation { get; set; }

        // attribut note
        public Double note { get; set; }

        // attribut annee
        public int annee { get; set; }

        public string anonymat { get; set; }

        // constructeur avec paramètres --------------------------------------
        public NotesBE(String matricule, String codeMatiere, String codeSequence, String codeEvaluation, Double note, int annee, string anonymat)
        {
            this.matricule = matricule;
            this.codeMat = codeMatiere;
            this.codeSeq = codeSequence;
            this.codeEvaluation = codeEvaluation;
            this.note = note;
            this.annee = annee;
            this.anonymat = anonymat;
        }

        public NotesBE()
        {
            this.matricule = "";
            this.codeMat = "";
            this.codeSeq = "";
            this.codeEvaluation = "";
            this.note = 0;
            this.anonymat = "";
            this.annee = DateTime.Today.Year;
        }
    }
}
