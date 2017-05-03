using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class ProgrammerBE
    {
        // declaration des éléments;
        public string codeclasse { get; set; }
        public string codematiere { get; set; }
        public string codeprof { get; set; }
        public int coef { get; set; }
        public int annee { get; set; }
        public string codegroupe { get; set; }

            
       
        public ProgrammerBE(string codeC, string codeM, string codeP, int coef, int annee,string codegroupe)
        {
            this.codeclasse = codeC;
            this.codematiere = codeM;
            this.codeprof = codeP;
            this.coef = coef;
            this.annee = annee;
            this.codegroupe = codegroupe;
        }

        public ProgrammerBE()
        {
            this.codeclasse = "";
            this.codematiere = "";
            this.codeprof = "";
            this.coef = 0;
            this.annee = DateTime.Today.Year;
            this.codegroupe = "";
        }
    }
}
