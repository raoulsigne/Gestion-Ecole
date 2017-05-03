using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class TypeclasseBE
    {
         // declaration des éléments;
        public string codetypeclasse { get; set; }
        public string nomtypeclasse { get; set; }
        public decimal fraisinscription { get; set; }

        public TypeclasseBE()
        {
            this.codetypeclasse = "";
            this.nomtypeclasse = "";
            this.fraisinscription = 0;
        }
       
        public TypeclasseBE(string codeT, string nomT, decimal fraisI)
        {
            this.codetypeclasse = codeT;
            this.nomtypeclasse = nomT;
            this.fraisinscription = fraisI;
        }
    }
}
