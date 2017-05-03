using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class EvaluerBE
    {
        public string codeEvaluation { get; set; }
        public string codeMat { get; set; }
        public string codeClasse { get; set; }
        public int poids { get; set; }
        public int annee { get; set; }
        public String codeSequence { get; set; }

        public virtual ClasseBE classe { get; set; }
        public virtual TypeevaluationBE typeEvaluation { get; set; }
        public virtual MatiereBE matiere { get; set; }

        public EvaluerBE()
        {
            this.codeEvaluation = "";
            this.codeMat = "";
            this.codeClasse = "";
            this.poids = 0;
            this.annee = DateTime.Today.Year;

            this.classe = new ClasseBE();
            this.typeEvaluation = new TypeevaluationBE();
            this.matiere = new MatiereBE();
            this.codeSequence = "";
        }

        public EvaluerBE(String codeEvaluation, String codemat, String codeclasse, int poids, int annee, String codeSequence)
        {
            this.codeEvaluation = codeEvaluation;
            this.codeMat = codemat;
            this.codeClasse = codeclasse;
            this.poids = poids;
            this.annee = annee;
            this.codeSequence = codeSequence;

            this.classe = new ClasseBE();
            this.typeEvaluation = new TypeevaluationBE();
            this.matiere = new MatiereBE();
        }
    }
}
