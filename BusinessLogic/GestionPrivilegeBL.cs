using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    class GestionPrivilegeBL
    {
        private PrivilegeDA privilegeDA;
        JournalDA journalDA;
         //------constructeur---------------------------------------------
        public GestionPrivilegeBL()
        {
            this.privilegeDA = new PrivilegeDA();
            journalDA = new JournalDA();
        }

        //------ajouter un privilèges--------------------------------
        public bool ajouterPrivilege(PrivilegeBE privilegeBE)
        {
            if (privilegeDA.ajouter(privilegeBE))
            {
                journalDA.journaliser("Enregistrement d'un privilege - " + privilegeBE.codePrivilege + " - " + privilegeBE.nomPrivilege);
                return true;
            }
            else
                return false;
        }

        public bool supprimerTousPrivileges()
        {
            if (privilegeDA.supprimerTout())
            {
                journalDA.journaliser("Suppression de tous les privileges");
                return true;
            }
            else
                return false;
        }

        public bool rechercherPrivillege(PrivilegeBE privilege)
        {
            if (privilegeDA.rechercher(privilege) != null)
                return true;
            else
                return false;
        }
    }
}
