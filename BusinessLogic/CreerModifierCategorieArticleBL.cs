using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierCategorieArticleBL
    {
        private CategorieArticleDA categorieArticleDA;
        private JournalDA journalDA;

        public CreerModifierCategorieArticleBL()
        {
            this.categorieArticleDA = new CategorieArticleDA();
            this.journalDA = new JournalDA();
        }

        //creer une CategorieArticle
        public bool creerCategorieArticle(string codeCatArticle, string nomCatArticle)
        {
            CategorieArticleBE typeclasseBE = new CategorieArticleBE(codeCatArticle, nomCatArticle);
            if (categorieArticleDA.ajouter(typeclasseBE))
            {
                journalDA.journaliser("enregistrement d'une catégorie d'article de code " + codeCatArticle + " et de nom " + nomCatArticle);
                return true;
            }
            return false;
        }

        // supprimer une CategorieArticle
        public bool supprinerCategorieArticle(CategorieArticleBE categorieArticle)
        {
            if (categorieArticleDA.supprimer(categorieArticle))
            {
                journalDA.journaliser("suppression de la catégorie d'article de code " + categorieArticle.codeCatArticle + " et de nom " + categorieArticle.nomCatArticle);
                return true;
            }
            return false;
        }

        // modifier une CategorieArticle
        public bool modifierCategorieArticle(CategorieArticleBE categorieArticle, CategorieArticleBE newcategorieArticle)
        {
            if (categorieArticleDA.modifier(categorieArticle, newcategorieArticle))
            {
                journalDA.journaliser("modification de la catégorie d'article de code " + categorieArticle.codeCatArticle + " et de nom " + categorieArticle.nomCatArticle + ". nouveau code : " + newcategorieArticle.codeCatArticle + ", nouveau nom : " + newcategorieArticle.nomCatArticle);
                return true;
            }
            return false;
        }

        // modifier une CategorieArticle
        public bool modifierCategorieArticle(CategorieArticleBE categorieArticle)
        {
            if (categorieArticleDA.modifier(categorieArticle))
            {
                journalDA.journaliser("modification de la catégorie d'article de code " + categorieArticle.codeCatArticle + " et de nom " + categorieArticle.nomCatArticle );
                return true;
            }
            return false;
        }

        // rechercher une CategorieArticle
        public CategorieArticleBE rechercherCategorieArticle(CategorieArticleBE catArticle)
        {
            return categorieArticleDA.rechercher(catArticle);
        }

        //lister toutes les CategorieArticle
        public List<CategorieArticleBE> listerTousLesCategorieArticle()
        {
            return categorieArticleDA.listerTous();
        }

        // lister toutes les CategorieArticle respectant un certain critère
        public List<CategorieArticleBE> listerCategorieArticleSuivantCritere(string critere)
        {
            return categorieArticleDA.listerSuivantCritere(critere);
        }

        // retourne la liste des codes des Catégories d'articles deja enregistré (pour le filtre)
        public List<string> getListCodeCategorieArticle(List<CategorieArticleBE> listCategorieArticle)
        {
            List<string> listeCodeCategorieArticle = new List<string>();

            listeCodeCategorieArticle = new List<string>();
            listeCodeCategorieArticle.Add("<Tous les Codes>");
            if (listCategorieArticle != null)
            {
                for (int i = 0; i < listCategorieArticle.Count; i++)
                {
                    listeCodeCategorieArticle.Add(listCategorieArticle.ElementAt(i).codeCatArticle);
                }
                //listeCodeCategorieArticle.Add("Tous");
                return listeCodeCategorieArticle;
            }
            else return null;
        }

        // retourne la liste des noms des niveaux deja enregistré (pour le filtre)
        public List<string> getListNomCategorieArticle(List<CategorieArticleBE> listCategorieArticle)
        {
            List<string> listeNomCategorieArticle = new List<string>();

            listeNomCategorieArticle = new List<string>();
            listeNomCategorieArticle.Add("<Tous les Noms>");
            if (listCategorieArticle != null)
            {
                for (int i = 0; i < listCategorieArticle.Count; i++)
                {
                    listeNomCategorieArticle.Add(listCategorieArticle.ElementAt(i).nomCatArticle);
                }
                //listeNomNiveau.Add("Tous");
                return listeNomCategorieArticle;
            }
            else return null;
        }
    }
}
