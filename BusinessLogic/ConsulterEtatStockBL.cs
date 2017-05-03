using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    class ConsulterEtatStockBL
    {
        private ArticleDA articleDA;
        private CategorieArticleDA categorieArticleDA;
        private StockerDA stockerDA;
        private AcheterDA acheterDA;
        private ParametresDA parametreDA;
        private JournalDA journalDA;

        public ConsulterEtatStockBL()
        {
            this.articleDA = new ArticleDA();
            this.acheterDA = new AcheterDA();
            this.categorieArticleDA = new CategorieArticleDA();
            this.stockerDA = new StockerDA();
            this.parametreDA = new ParametresDA();
            this.journalDA = new JournalDA();
        }

        //creer un Article
        public bool creerArticle(string codeArticle, string codeCatArticle, string designation)
        {
            ArticleBE articleBE = new ArticleBE(codeArticle, codeCatArticle, designation);
            if (articleDA.ajouter(articleBE))
            {
                journalDA.journaliser("ajout d'un article de code" + codeArticle + ", de catégorie " + codeCatArticle + " et de désignation" + designation);
                return true;
            }
            return false;
        }

        // supprimer un Article
        public bool supprinerArticle(ArticleBE article)
        {
            if (articleDA.supprimer(article))
            {
                journalDA.journaliser("suppression de l'article de code" + article.codeArticle + ", de catégorie " + article.codeCatArticle + " et de désignation" + article.designation);
                return true;
            }
            return false;
        }

        // modifier un article
        public bool modifierArticle(ArticleBE article, ArticleBE newarticle)
        {
            if (articleDA.modifier(article, newarticle))
            {
                journalDA.journaliser("modification de l'article de code" + article.codeArticle + ", de catégorie " + article.codeCatArticle + " et de désignation" + article.designation+". nouveau code : "+newarticle.codeArticle+", nouvelle catégorie : "+newarticle.codeCatArticle+", désignation : "+newarticle.designation);
                return true;
            }
            return false;
        }

        // modifier un Article
        public bool modifierArticle(ArticleBE article)
        {
            if (articleDA.modifier(article))
            {
                journalDA.journaliser("modification de l'article de code" + article.codeArticle + ", de catégorie " + article.codeCatArticle + " et de désignation" + article.designation);
                return true;
            }
            return false;
        }

        // rechercher un Article
        public ArticleBE rechercherArticle(ArticleBE article)
        {
            return articleDA.rechercher(article);
        }

        //lister toutes les Articles
        public List<ArticleBE> listerTousLesArticles()
        {
            return articleDA.listerTous();
        }

        //lister toutes les Catégories d'Articles
        public List<CategorieArticleBE> listerToutesLesCategoriesArticles()
        {
            return categorieArticleDA.listerTous();
        }

        //lister toutes les Stocks
        public List<StockerBE> listerTousLesStocks()
        {
            return stockerDA.listerTous();
        }

        // lister toutes les Articles respectant un certain critère
        public List<StockerBE> listerStocksSuivantCritere(string critere)
        {
            return stockerDA.listerSuivantCritere(critere);
        }

        // lister toutes les Articles respectant un certain critère
        public List<ArticleBE> listerArticleSuivantCritere(string critere)
        {
            return articleDA.listerSuivantCritere(critere);
        }

        // lister toutes les Catégories d'Articles respectant un certain critère
        public List<CategorieArticleBE> listerCategoriesArticleSuivantCritere(string critere)
        {
            return categorieArticleDA.listerSuivantCritere(critere);
        }

        // lister tous les achats
        public List<AcheterBE> listerTousLesAchats()
        {
            return acheterDA.listerTous();
        }

        //retourne la quantité acheté d'un article
        public int GetQuantiteVenduArticle(string codeArticle, int annee)
        {

            return articleDA.getQuantiteVendu(codeArticle, annee);
        }

        //retouner les paramètres
        public ParametresBE getParametres()
        {
            List<ParametresBE> LParametre = parametreDA.listerTous();
            if (LParametre != null)
            {
                if (LParametre.Count != 0)
                {
                    return LParametre.ElementAt(0);
                }
                else return null;
            }
            else return null;
        }

        // retourne la liste des codes des Catégories d'articles deja enregistré (pour le filtre)
        public List<string> getListCodeCategorieArticle(List<CategorieArticleBE> listCategorieArticle)
        {
            List<string> listeCodeCategorieArticle = new List<string>();

            listeCodeCategorieArticle = new List<string>();
            listeCodeCategorieArticle.Add("<Toutes Les Catégories>");

            if (listCategorieArticle != null)
            {
                for (int i = 0; i < listCategorieArticle.Count; i++)
                {
                    listeCodeCategorieArticle.Add(listCategorieArticle.ElementAt(i).codeCatArticle);
                }
                return listeCodeCategorieArticle;
            }
            else return null;
        }

        // retourne la liste des codes des Article deja enregistré (pour le filtre)
        public List<string> getListCodeArticle(List<ArticleBE> listArticle)
        {
            List<string> listeCodeArticle = new List<string>();

            listeCodeArticle = new List<string>();
            listeCodeArticle.Add("<Tous Les Articles>");

            if (listArticle != null)
            {
                for (int i = 0; i < listArticle.Count; i++)
                {
                    listeCodeArticle.Add(listArticle.ElementAt(i).codeArticle);
                }
                return listeCodeArticle;
            }
            else return null;
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

        //fonction qui retourne les infos sur l'état de stock
        public List<EtatStockBE> getEtatStock() {
            return stockerDA.etatStock();
        }

        //fonction qui retourne les infos sur l'état de stock
        public List<EtatStockBE> etatStockSuivantCodeArticle(string critere)
        {
            return stockerDA.etatStockSuivantCodeArticle(critere);
        }

        //fonction qui retourne les infos sur l'état de stock
        public List<EtatStockBE> etatStockSuivantCategorieArticle(string critere)
        {
            return stockerDA.etatStockSuivantCategorieArticle(critere);
        }

        //fonction qui retourne les infos sur l'état de stock
        public List<EtatStockBE> etatStockSuivantDateOperation(DateTime dateOp)
        {
            return stockerDA.etatStockSuivantDateOperation(dateOp);
        }

    }
}
