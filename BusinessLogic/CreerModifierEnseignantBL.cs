using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierEnseignantBL
    {
        private EnseignantDA enseignantDA;
        private JournalDA journalDA;

        public CreerModifierEnseignantBL()
        {
            this.enseignantDA = new EnseignantDA();
            this.journalDA = new JournalDA();
        }

        //creer une Enseignant
        public bool creerEnseignant(string codeEnseignant, string nomEnseignant, DateTime dateNaiss, String tel, String email, String ville, DateTime dateEmbauche, DateTime dateDepart, String statut, String photo)
        {
            EnseignantBE enseignant = new EnseignantBE(codeEnseignant, nomEnseignant, dateNaiss, tel, email, ville, dateEmbauche, dateDepart);
            enseignant.statut = statut;
            enseignant.photo = photo;

            if (enseignantDA.ajouter(enseignant))
            {
                journalDA.journaliser("enregistrement d'un enseignant de code " + codeEnseignant + " et de nom " + nomEnseignant);
                return true;
            }
            return false;
        }

        // supprimer une Enseignant
        public bool supprinerEnseignant(EnseignantBE enseignant)
        {
            if (enseignantDA.supprimer(enseignant))
            {
                journalDA.journaliser("suppression de l'enseignant de code " + enseignant.codeProf + " et de nom " + enseignant.nomProf);
                return true;
            }
            return false;
        }

        // modifier une Enseignant
        public bool modifierEnseignant(EnseignantBE enseignant, EnseignantBE newEnseignant)
        {
            if (enseignantDA.modifier(enseignant, newEnseignant))
            {
                journalDA.journaliser("modification de l'enseignant de code " + enseignant.codeProf + ". ancien nom : " + enseignant.nomProf + ". nouveau code : " + newEnseignant.codeProf + ", nouveau nom : " + newEnseignant.nomProf);
                return true;
            }
            return false;
        }

        // modifier une Enseignant
        public bool modifierEnseignant(EnseignantBE enseignant)
        {
            if (enseignantDA.modifier(enseignant))
            {
                journalDA.journaliser("modification de l'enseignant de code " + enseignant.codeProf + ". nouveau nom : " + enseignant.nomProf);
                return true;
            }
            return false;
        }

        // rechercher une Enseignant
        public EnseignantBE rechercherEnseignant(EnseignantBE enseignant)
        {
            return enseignantDA.rechercher(enseignant);
        }

        //lister toutes les Enseignant
        public List<EnseignantBE> listerTousLesEnseignants()
        {
            return enseignantDA.listerTous();
        }

        // lister toutes les Enseignants respectant un certain critère
        public List<EnseignantBE> listerSerieSuivantCritere(string critere)
        {
            return enseignantDA.listerSuivantCritere(critere);
        }

        //-----------------MOI--------------------------------------------
        // obtenir le dernier matricule de l'enseignant saisie------------
        internal string getDernierMatricule()
        {
            return enseignantDA.getDernierMatricule();
        }
        //---------------------------Fin MOI-----------------------------
    }
}
