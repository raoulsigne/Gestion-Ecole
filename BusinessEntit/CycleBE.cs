using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_Ecole.BusinessEntity
{
    class CycleBE
    {
        public CycleBE()
        {
            this.classes = new HashSet<ClasseBE>();
            this.codeCycle = "";
            this.nomCycle = "";
        }

        public CycleBE(String  code, String nom)
        {
            this.classes = new HashSet<ClasseBE>();
            this.codeCycle = code;
            this.nomCycle = nom;
        }

        public string codeCycle { get; set; }
        public string nomCycle { get; set; }

        public virtual ICollection<ClasseBE> classes { get; set; }
    }
}
