using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class TypeoperationBE
    {
             // declaration des éléments;
        public string codetypeop { get; set; }
        public string libelletypeop { get; set; }
        

            
       
        public TypeoperationBE(string codeT, string libelleT)
        {
            this.codetypeop = codeT;
            this.libelletypeop = libelleT;
            
        }
    }
}
