using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class GestionMontantTrancheBL
    {
        CategorieEleveDA categorieDA;
        PrestationDA prestationDA;
        TrancheDA trancheDA;
        MontantTrancheDA montantTrancheDA;
        ParametresDA parametreDA;
        JournalDA journalDA;

        public GestionMontantTrancheBL()
        {
            categorieDA = new CategorieEleveDA();
            prestationDA = new PrestationDA();
            trancheDA = new TrancheDA();
            montantTrancheDA = new MontantTrancheDA();
            parametreDA = new ParametresDA();
            journalDA = new JournalDA();
        }

        internal List<string> listerValeurColonneCategorieEleve(string p)
        {
            return categorieDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonnePrestation(string p)
        {
            return prestationDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneTranche(string p)
        {
            return trancheDA.listerValeursColonne(p);
        }

        internal List<BusinessEntity.MontantTrancheBE> listerToutMontantTranche()
        {
            return montantTrancheDA.listerTous() ;
        }

        internal bool enregistrerMontantTranche(BusinessEntity.MontantTrancheBE m)
        {
            if (montantTrancheDA.ajouter(m))
            {
                journalDA.journaliser("Enregistrement du montant d'une tranche "+m.codePrestation+" "+m.codeTranche);
                return true;
            }
            else
                return false;
        }

        internal void supprimerMontantTranche(BusinessEntity.MontantTrancheBE mt)
        {
            if (montantTrancheDA.supprimer(mt))
            {
                journalDA.journaliser("Suppression du montant d'une tranche " + mt.codePrestation + " " + mt.codeTranche);
            }
        }

        internal MontantTrancheBE rechercherMontantTranche(MontantTrancheBE m)
        {
            return montantTrancheDA.rechercher(m);
        }

        internal int anneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        internal List<MontantTrancheBE> listerSuivantCritereMontantTranche(string p)
        {
            return montantTrancheDA.listerSuivantCritere(p);
        }

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }
    }
}
