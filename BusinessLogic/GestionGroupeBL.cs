using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class GestionGroupeBL
    {
        private GroupeDA groupeDA;
        JournalDA journalDA;

       //------constructeur---------------------------------------------
        public GestionGroupeBL()
        {
            this.groupeDA = new GroupeDA();
            journalDA = new JournalDA();
        }

        //------lister tous les groupes d'utilisateurs-------------------
        public List<GroupeBE> listerTousLesGroupe()
        {
            return groupeDA.listerTous();
        }

        // retourne la liste des roles de groupes-------------------------
        public List<string> ListerRoleGroupe(List<GroupeBE> listGroupe)
        {
            List<string> listeRole = new List<string>();
             
            listeRole = new List<string>(); 
            if (listGroupe != null)
            {
                for (int i = 0; i < listGroupe.Count; i++)
                {
                    listeRole.Add(listGroupe.ElementAt(i).role);
                }
                return listeRole;
            }
            else return null;
        }


        //------obtenir la description d'un role-------------------
        public String getDescriptionRole(String role)
        {
            return groupeDA.getDescriptionRole(role);
        }

    }
}
