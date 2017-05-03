using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    public class GestionGroupeUtilisateurBL
    {
        GroupeDA groupeDA;
        JournalDA journalDA;

        public GestionGroupeUtilisateurBL()
        {
            groupeDA = new GroupeDA();
            journalDA = new JournalDA();
        }

        internal bool enregistrer(BusinessEntity.GroupeBE groupe)
        {
            if(groupeDA.ajouter(groupe))
            {
                journalDA.journaliser("Enregistrement d'un groupe d'utilisateur - "+groupe.description);
                return true;
            }
            else
                return false;
        }

        internal bool modifierGroupe(BusinessEntity.GroupeBE old_groupe, BusinessEntity.GroupeBE groupe)
        {
            if(groupeDA.modifier(old_groupe,groupe))
            {
                journalDA.journaliser("Modification d'un groupe d'utilisateur - " + old_groupe.description+ " - "+groupe.description);
                return true;
            }
            else
                return false;
        }

        internal void supprimerGroupe(BusinessEntity.GroupeBE groupe)
        {
            if(groupeDA.supprimer(groupe))
            {
                journalDA.journaliser("Suppression d'un groupe d'utilisateur - " + groupe.description);
            }
        }

        internal List<BusinessEntity.GroupeBE> listerToutGroupe()
        {
            return groupeDA.listerTous();
        }

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }
    }
}
