using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    public class GestionUtilisateurBL
    {
        UtilisateurDA utilisateurDA;
        GroupeDA groupeDA;
        JournalDA journalDA;

        public GestionUtilisateurBL()
        {
            utilisateurDA = new UtilisateurDA();
            groupeDA = new GroupeDA();
            journalDA = new JournalDA();
        }

        internal List<UtilisateurBE> listerToutUtilisateur()
        {
            return utilisateurDA.listerTous();
        }

        internal List<string> listerValeurColonneGroupe(string p)
        {
            return groupeDA.listerValeursColonne(p);
        }

        internal bool enregistrerUtilisateur(UtilisateurBE utilisateur)
        {
            if (utilisateurDA.ajouter(utilisateur))
            {
                journalDA.journaliser("Enregistrement d'un utilisateur - " + utilisateur.login);
                return true;
            }
            else
                return false;
        }

        internal bool modifierUtilisateur(UtilisateurBE old_utilisateur, UtilisateurBE utilisateur)
        {
            if (utilisateurDA.modifier(old_utilisateur, utilisateur))
            {
                journalDA.journaliser("Modification d'un utilisateur - " + old_utilisateur.login);
                return true;
            }
            else
                return false;
        }

        internal void supprimerUtilisateur(UtilisateurBE utilisateur)
        {
            if(utilisateurDA.supprimer(utilisateur))
                journalDA.journaliser("Suppression d'un utilisateur - " + utilisateur.login);
        }

        public void journaliser(string p)
        {
            journalDA.journaliser(p);
        }
    }
}
