using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class TrimestreBE
    {
        // declaration des éléments;
        public string codetrimestre { get; set; }
        public string nomtrimestre { get; set; }


        public TrimestreBE() {
            this.codetrimestre = "";
            this.nomtrimestre = "";
        }

        public TrimestreBE(string codeT, string nomT)
        {
            this.codetrimestre = codeT;
            this.nomtrimestre = nomT;
           
        }
    }
}
