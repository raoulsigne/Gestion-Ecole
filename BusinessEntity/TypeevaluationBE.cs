using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class TypeevaluationBE
    {
         // declaration des éléments;
        public string codeevaluation { get; set; }
        public string nomeval { get; set; }
       

            
       
        public TypeevaluationBE(string codeE, string nomE)
        {
            this.codeevaluation = codeE;
            this.nomeval = nomE;
            
        }

        public TypeevaluationBE()
        {
            this.codeevaluation = "";
            this.nomeval = "";
        }
    }
}
