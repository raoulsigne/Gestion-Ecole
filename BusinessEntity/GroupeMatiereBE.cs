using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class GroupeMatiereBE
    {
        public string codegroupe { get; set; }
        public string nomgroupe { get; set; }

        public GroupeMatiereBE() 
        {
            this.codegroupe = "";
            this.nomgroupe = "";
        }

        public GroupeMatiereBE(string code,string nom)
        {
            this.codegroupe = code;
            this.nomgroupe = nom;
        }
    }
}
