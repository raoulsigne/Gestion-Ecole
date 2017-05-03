using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class PrivilegeBE
    {
        // définition des attributs -----------------------------------------
        // attribut codePrivilege
        public String codePrivilege { get; set; }

        // attribut nomPrivilege
        public String nomPrivilege { get; set; }

        // constructeur de la classe -----------------------------------------
        public PrivilegeBE() { }

        // constructeur avec paramètres --------------------------------------
        public PrivilegeBE(String codePrivilege, String nomPrivilege)
        {
            this.codePrivilege = codePrivilege;
            this.nomPrivilege = nomPrivilege;
        }
    }
}
