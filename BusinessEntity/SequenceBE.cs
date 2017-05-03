using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class SequenceBE
    {
             // declaration des éléments;
        public string codeseq { get; set; }
        public string nomseq { get; set; }

        public string codetrimestre { get; set; }

        public SequenceBE() {
            this.codeseq = "";
            this.nomseq = "";
            this.codetrimestre = "";
        }
       
        public SequenceBE(string codeS, string nomS, string codeT)
        {
            this.codeseq = codeS;
            this.nomseq = nomS;
            this.codetrimestre = codeT;
           
        }
    }
}
