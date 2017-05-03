using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierArticleBL
    {
        private ArticleDA articleDA;
        private JournalDA journalDA;

        public CreerModifierArticleBL()
        {
            this.articleDA = new ArticleDA();
            this.journalDA = new JournalDA();
        }

        //creer un Article
        public bool creerArticle(string codeArticle, string codeCategorieArticle, string Designation)
        {
            ArticleBE articleBE = new ArticleBE(codeArticle, codeCategorieArticle, Designation);
            if (articleDA.ajouter(articleBE))
            {
                journalDA.journaliser("enregistrement d'un article de code " + codeArticle + ", de catégorie " + codeCategorieArticle + " et de désignation " + Designation);
                return true;
            }
            return false;
        }

        // supprimer un Article
        public bool supprinerArticle(ArticleBE article)
        {
            if (articleDA.supprimer(article))
            {
                journalDA.journaliser("suppression de article de code " + article.codeArticle + ", de catégorie " + article.codeCatArticle + " et de désignation " + article.designation);
                return true;
            }
            return false;
        }

        // modifier un Article
        public bool modifierArticle(ArticleBE article, ArticleBE newarticle)
        {
            if (articleDA.modifier(article, newarticle))
            {
                journalDA.journaliser("modification d'un article de code " + article.codeArticle + ". ancien code : " + article.codeCatArticle + ", ancienne catégorie " + article.codeCatArticle + ", ancienne désignation " + article.designation + ". nouveau code : " + newarticle.codeCatArticle + ", nouvelle catégorie " + newarticle.codeCatArticle + ", nouvelle désignation " + newarticle.designation);
                return true;
            }
            return false;
        }

        // modifier un Article
        public bool modifierArticle(ArticleBE article)
        {
            if (articleDA.modifier(article))
            {
                journalDA.journaliser("modification d'un article de code " + article.codeArticle + ". ancien code : " + article.codeCatArticle + ", ancienne catégorie " + article.codeCatArticle + ", ancienne désignation " + article.designation);
                return true;
            }
            return false;
        }

        // rechercher un Article
        public ArticleBE rechercherArticle(ArticleBE article)
        {
            return articleDA.rechercher(article);
        }

        //lister toutes les Article
        public List<ArticleBE> listerTousLesArticle()
        {
            return articleDA.listerTous();
        }

        // lister toutes les Article respectant un certain critère
        public List<ArticleBE> listerArticleSuivantCritere(string critere)
        {
            return articleDA.listerSuivantCritere(critere);
        }

        // retourne la liste des codes de Magasin deja enregistré (pour le filtre)
        public List<string> getListCodeMagasin(List<MagasinBE> listMagasin)
        {
            List<string> listeCodeMagasin = new List<string>();

            listeCodeMagasin = new List<string>();
            if (listMagasin != null)
            {
                for (int i = 0; i < listMagasin.Count; i++)
                {
                    listeCodeMagasin.Add(listMagasin.ElementAt(i).codeMagasin);
                }
                //listeCodeMagasin.Add("Tous");
                return listeCodeMagasin;
            }
            else return null;
        }

        // retourne la liste des codes des Article deja enregistré (pour le filtre)
        public List<string> getListCodeArticle(List<ArticleBE> listArticle)
        {
            List<string> listeCodeArticle = new List<string>();

            listeCodeArticle = new List<string>();
            listeCodeArticle.Add("<Tous les Articles>");
            if (listArticle != null)
            {
                for (int i = 0; i < listArticle.Count; i++)
                {
                    listeCodeArticle.Add(listArticle.ElementAt(i).codeArticle);
                }
                //listeCodeArticle.Add("Tous");
                return listeCodeArticle;
            }
            else return null;
        }

        // retourne la liste des codes des Catégorie d'article deja enregistré (pour le filtre)
        public List<string> getListCodeCatArticle1(List<CategorieArticleBE> listCatArticle)
        {
            List<string> listeCodeCatArticle = new List<string>();

            listeCodeCatArticle = new List<string>();
            if (listCatArticle != null)
            {
                for (int i = 0; i < listCatArticle.Count; i++)
                {
                    listeCodeCatArticle.Add(listCatArticle.ElementAt(i).codeCatArticle);
                }
                //listeCodeCatArticle.Add("Tous");
                return listeCodeCatArticle;
            }
            else return null;
        }

        // retourne la liste des codes des Catégorie d'article deja enregistré (pour le filtre)
        public List<string> getListCodeCatArticle2(List<CategorieArticleBE> listCatArticle)
        {
            List<string> listeCodeCatArticle = new List<string>();

            listeCodeCatArticle = new List<string>();
            listeCodeCatArticle.Add("<Toutes les Catégories>");

            if (listCatArticle != null)
            {
                for (int i = 0; i < listCatArticle.Count; i++)
                {
                    listeCodeCatArticle.Add(listCatArticle.ElementAt(i).codeCatArticle);
                }
                //listeCodeCatArticle.Add("Tous");
                return listeCodeCatArticle;
            }
            else return null;
        }
    }
}
