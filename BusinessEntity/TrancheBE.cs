using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class TrancheBE
    {
            // declaration des éléments;
        public string codetranche { get; set; }
        public string nomtranche { get; set; }


        public TrancheBE()
        {
            codetranche = "";
            nomtranche = "";
        }    
       
        public TrancheBE(string codeT, string nomT)
        {
            this.codetranche = codeT;
            this.nomtranche = nomT;
            
        }
    }
}
