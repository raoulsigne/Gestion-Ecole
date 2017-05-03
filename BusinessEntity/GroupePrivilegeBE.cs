using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class GroupePrivilegeBE
    {   
        // définition des attributs -----------------------------------------

        // attribut codePrivilege
        public String codePrivilege { get; set; }
       

        // attribut role
        public String role { get; set; }
 
        // attribut annee
        public int annee { get; set; }

        // constructeur de la classe -----------------------------------------
        public GroupePrivilegeBE() { 
        }

        // constructeur avec paramètres --------------------------------------
        public GroupePrivilegeBE(String codePrivilege, String role, int annee)
        {
            this.codePrivilege = codePrivilege;
            this.role = role;
            this.annee = annee;
        }
    }
}
