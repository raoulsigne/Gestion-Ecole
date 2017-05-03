using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierParametresBL
    {
        private ParametresDA parametresDA;
        private JournalDA journalDA;

        public CreerModifierParametresBL()
        {
            this.parametresDA = new ParametresDA();
            this.journalDA = new JournalDA();
        }

        //creer un Parametre
        public bool creerParametre(int idParametre, String nomEcole, String adresse, String telephone, String fax, String
             email, String siteWeb, String directeur, String pays, String region, String ministere, String ministery, String country, String regionA, int annee,
            String departement, String department, String ville, String titreDuChef, String titleOfChief, string logo, string REPERTOIRE_PHOTO)
        {
            ParametresBE parametre = new ParametresBE(idParametre, nomEcole, adresse, telephone, fax, email, siteWeb,
                directeur, pays, region, ministere, ministery, country, regionA, annee, departement, department, ville, titreDuChef, titleOfChief, logo, REPERTOIRE_PHOTO);

            if (parametresDA.ajouter(parametre))
            {
                journalDA.journaliser("enregistrement de paramètres. nomEcole : " + nomEcole + ",  adresse : " + adresse + ", telephone : "+
                    telephone + ", fax : " + fax + ", email : " + email + ", siteWeb : " + siteWeb + ", directeur : " + directeur + ", pays : " + pays + ", etc...");
                return true;
            }
            return false;
        }

        // supprimer un Parametre
        public bool supprinerParametre(ParametresBE parametre)
        {
            if (parametresDA.supprimer(parametre))
            {
                journalDA.journaliser("suppression de paramètres");
                return true;
            }
            return false;
        }

        // modifier un Parametre
        public bool modifierParametre(ParametresBE parametre, ParametresBE newParametre)
        {
            if (parametresDA.modifier(parametre, newParametre))
            {
                journalDA.journaliser("modification de paramètres. nomEcole : " + newParametre.nomEcole + ",  adresse : " + newParametre.adresse + ", telephone : " +
                    newParametre.tel + ", fax : " + newParametre.fax + ", email : " + newParametre.email + ", siteWeb : " + newParametre.siteWeb + ", directeur : " + newParametre.directeur + ", pays : " + newParametre.pays + ", etc...");
                return true;
            }
            return false;
        }

        // modifier un Parametre
        public bool modifierParametre(ParametresBE parametre)
        {
            if (parametresDA.modifier(parametre))
            {
                journalDA.journaliser("modification de paramètres. nomEcole : " + parametre.nomEcole + ",  adresse : " + parametre.adresse + ", telephone : " +
                    parametre.tel + ", fax : " + parametre.fax + ", email : " + parametre.email + ", siteWeb : " + parametre.siteWeb + ", directeur : " + parametre.directeur + ", pays : " + parametre.pays + ", etc...");
                return true;
            }
            return false;
        }

        // rechercher un Parametre
        public ParametresBE rechercherParametre(ParametresBE parametre)
        {
            return parametresDA.rechercher(parametre);
        }

        //lister tous les Parametres
        public List<ParametresBE> listerTousLesParametres()
        {
            return parametresDA.listerTous();
        }

        // lister tous les Parametres respectant un certain critère
        public List<ParametresBE> listerParametreSuivantCritere(string critere)
        {
            return parametresDA.listerSuivantCritere(critere);
        }

        // recherche si un paramètre existe deja dans la BD
        public Boolean existParametre() {
            List<ParametresBE> LParametres = new List<ParametresBE>();
            LParametres = parametresDA.listerTous();
            if (LParametres == null || LParametres.Count == 0)
                return false;
            else return true;
        }

        public ParametresBE getParametre() {
            return parametresDA.getParametre();
        }
    }
}
