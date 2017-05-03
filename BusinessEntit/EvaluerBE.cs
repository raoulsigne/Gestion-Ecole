using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_Ecole.BusinessEntity
{
    class EvaluerBE
    {
        public string codeEvaluation { get; set; }
        public string codeMat { get; set; }
        public string codeClasse { get; set; }
        public int poids { get; set; }
        public int annee { get; set; }

        public virtual ClasseBE classe { get; set; }

        public EvaluerBE()
        {
            this.codeEvaluation = "";
            this.codeMat = "";
            this.codeClasse = "";
            this.poids = 0;
            this.annee = DateTime.Today.Year;
        }

        public EvaluerBE(String codeEvaluation, String codemat, String codeclasse, int poids, int annee)
        {
            this.codeEvaluation = codeEvaluation;
            this.codeMat = codemat;
            this.codeClasse = codeclasse;
            this.poids = poids;
            this.annee = annee;
        }
    }
}
