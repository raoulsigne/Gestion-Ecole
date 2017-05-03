using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    public class GestionModifierPasswordBL
    {
        UtilisateurDA utilisateurDA;
        JournalDA journalDA;

        public GestionModifierPasswordBL()
        {
            utilisateurDA = new UtilisateurDA();
            journalDA = new JournalDA();
        }

        internal UtilisateurBE rechercherUtilisateur(UtilisateurBE utilisateur)
        {
            return utilisateurDA.rechercherAvecPassword(utilisateur);
        }

        internal bool modifierUtilisateur(UtilisateurBE utilisateur, UtilisateurBE user)
        {
            if (utilisateurDA.modifier(utilisateur, user))
            {
                journalDA.journaliser("Modification des information d'un utilisateur - "+user.login);
                return true;
            }
            else
                return false;
        }
    }
}
