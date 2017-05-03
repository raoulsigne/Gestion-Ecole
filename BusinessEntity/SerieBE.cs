using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class SerieBE
    {
            // declaration des éléments;
        public string codeserie { get; set; }
        public string nomserie { get; set; }
        

            
       
        public SerieBE(string codeS, string nomS)
        {
            this.codeserie = codeS;
            this.nomserie = nomS;
           
        }

        public SerieBE()
        {
            codeserie = "";
            nomserie = "";
        }
    }
}
