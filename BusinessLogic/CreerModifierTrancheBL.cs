using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierTrancheBL
    {
        private TrancheDA trancheDA;
        private JournalDA journalDA;

        public CreerModifierTrancheBL()
        {
            this.trancheDA = new TrancheDA();
            this.journalDA = new JournalDA();
        }

        //creer une Tranche
        public bool creerTranche(string codeTranche, string nomTranche)
        {
            TrancheBE tranche = new TrancheBE(codeTranche, nomTranche);
            if (trancheDA.ajouter(tranche))
            {
                journalDA.journaliser("enregistrement d'une tranche de code " + codeTranche + " et de nom " + nomTranche);
                return true;
            }
            return false;
        }

        // supprimer une Tranche
        public bool supprinerTranche(TrancheBE tranche)
        {
            if (trancheDA.supprimer(tranche))
            {
                journalDA.journaliser("suppression de la tranche de code " + tranche.codetranche + " et de nom " + tranche.nomtranche);
                return true;
            }
            return false;
        }

        // modifier une Tranche
        public bool modifierTranche(TrancheBE tranche, TrancheBE newtranche)
        {
            if (trancheDA.modifier(tranche, newtranche))
            {
                journalDA.journaliser("modification de la tranche de code " + tranche.codetranche + ". ancien nom : " + tranche.nomtranche + ". nouveau code : " + newtranche.codetranche + ", nouveau nom : " + newtranche.nomtranche);
                return true;
            }
            return false;
        }

        // modifier une Tranche
        public bool modifierTranche(TrancheBE tranche)
        {
            if (trancheDA.modifier(tranche))
            {
                journalDA.journaliser("modification de la tranche de code " + tranche.codetranche + ". nouveau nom : " + tranche.nomtranche);
                return true;
            }
            return false;
        }

        // rechercher une Tranche
        public TrancheBE rechercherTranche(TrancheBE tranche)
        {
            return trancheDA.rechercher(tranche);
        }

        //lister tous les tranches
        public List<TrancheBE> listerToutesLesTranche()
        {
            return trancheDA.listerTous();
        }

        // lister tous les tranches respectant un certain critère
        public List<TrancheBE> listerTrancheSuivantCritere(string critere)
        {
            return trancheDA.listerSuivantCritere(critere);
        }
    }
}
