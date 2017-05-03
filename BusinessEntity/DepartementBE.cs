using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class DepartementBE
    {
        public DepartementBE()
        {
            this.eleves = new HashSet<EleveBE>();
            this.codeDept = "";
            this.nomDept = "";
        }

        public DepartementBE(String code, String nom)
        {
            this.eleves = new HashSet<EleveBE>();
            this.codeDept = code;
            this.nomDept = nom;
        }

        public string codeDept { get; set; }
        public string nomDept { get; set; }

        public virtual ICollection<EleveBE> eleves { get; set; }
    }
}
