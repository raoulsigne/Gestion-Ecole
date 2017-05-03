using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;
using Ecole.UI;

namespace Ecole.BusinessLogic
{
    public class GestionConnexionBL
    {
        UtilisateurDA utilisateurDA;
        ParametresDA parametreDA;
        JournalDA journalDA;

        public GestionConnexionBL()
        {
            utilisateurDA = new UtilisateurDA();
            journalDA = new JournalDA();
            parametreDA = new ParametresDA();
        }

        public UtilisateurBE connect(string login, string password)
        {
            UtilisateurBE utilisateur = new UtilisateurBE();
            utilisateur.login = login;
            utilisateur.password = password;

            utilisateur = utilisateurDA.rechercherAvecPassword(utilisateur);

            return utilisateur;
        }

        public void journaliser(string action)
        {
            journalDA.journaliser(action);
        }


        internal List<ParametresBE> listerTousLesParametres()
        {
            return parametreDA.listerTous();
        }
    }
}
