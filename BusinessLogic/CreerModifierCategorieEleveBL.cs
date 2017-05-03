using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierCategorieEleveBL
    {
        private CategorieEleveDA categorieEleveDA;
        private JournalDA journalDA;

        public CreerModifierCategorieEleveBL()
        {
            this.categorieEleveDA = new CategorieEleveDA();
            this.journalDA = new JournalDA();
        }

        //creer une CategorieEleve
        public bool creerCategorieEleve(string codeCat, string nomCat)
        {
            CategorieEleveBE categorie = new CategorieEleveBE(codeCat, nomCat);
            if (categorieEleveDA.ajouter(categorie))
            {
                journalDA.journaliser("enregistrement d'une catégorie d'élève de code " + codeCat + " et de nom " + nomCat);
                return true;
            }
            return false;
        }

        // supprimer une CategorieEleve
        public bool supprinerCategorieEleve(CategorieEleveBE cat)
        {
            if (categorieEleveDA.supprimer(cat))
            {
                journalDA.journaliser("suppression de la catégorie d'élève de code " + cat.codeCatEleve + " et de nom " + cat.nomCatEleve);
                return true;
            }
            return false;
        }

        // modifier une CategorieEleve
        public bool modifierCategorieEleve(CategorieEleveBE cat, CategorieEleveBE newcat)
        {
            if (categorieEleveDA.modifier(cat, newcat))
            {
                journalDA.journaliser("modification de la catégorie d'élève de code " + cat.codeCatEleve + ". ancien nom " + cat.nomCatEleve+", nouveau nom : "+newcat.nomCatEleve);
                return true;
            }
            return false;
        }

        // modifier une CategorieEleve
        public bool modifierCategorieEleve(CategorieEleveBE cat)
        {
            if (categorieEleveDA.modifier(cat))
            {
                journalDA.journaliser("modification de la catégorie d'élève de code " + cat.codeCatEleve + ". ancien nom " + cat.nomCatEleve);
                return true;
            }
            return false;
        }

        // rechercher une CategorieEleve
        public CategorieEleveBE rechercherCategorieEleve(CategorieEleveBE cat)
        {
            return categorieEleveDA.rechercher(cat);
        }

        //lister toutes les CategorieEleve
        public List<CategorieEleveBE> listerToutesLesCategorieEleve()
        {
            return categorieEleveDA.listerTous();
        }

        // lister tous les CategorieEleve respectant un certain critère
        public List<CategorieEleveBE> listerCategorieEleveSuivantCritere(string critere)
        {
            return categorieEleveDA.listerSuivantCritere(critere);
        }
    }
}
