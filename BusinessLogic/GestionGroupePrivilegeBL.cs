using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    class GestionGroupePrivilegeBL
    {
      
         private GroupePrivilegeDA groupePrivilegeDA;
         JournalDA journalDA;

       //------constructeur---------------------------------------------
         public GestionGroupePrivilegeBL()
        {
            this.groupePrivilegeDA = new GroupePrivilegeDA();
            journalDA = new JournalDA();
        }

         //------ajouter un groupePrivilèges--------------------------------
         public bool ajouterGroupePrivilege(GroupePrivilegeBE gp)
         {
             return groupePrivilegeDA.ajouter(gp);
         }

         //------ajouter un groupePrivilèges--------------------------------
         public bool supprimerTousLesPrivilegeDunRole(String role)
         {
             return groupePrivilegeDA.supprimerTousDunRole(role); 
         }

        //------lister les privilèges d'un role--------------------------
         public List<String> listerPrivilegeDunRole(String role)
         {
             return groupePrivilegeDA.listerPrivilegeDunRole(role);           
         }



    }
}
