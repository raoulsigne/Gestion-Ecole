using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class ApprovisionnementArticleBL
    {
        private StockerDA stockerDA;
        private AcheterDA acheterDA;
        private JournalDA journalDA;
        private ParametresDA parametresDA;

        public ApprovisionnementArticleBL()
        {
            this.stockerDA = new StockerDA();
            this.acheterDA = new AcheterDA();
            this.journalDA = new JournalDA();
            this.parametresDA = new ParametresDA();
        }

        //creer un Stock
        public bool creerStock(string codeMagasin, string codeArticle, Int16 stockDebut, Int16 quantiteAchetee, Int16 quantiteVendue, DateTime dateOperation,
            int annee, Int16 puArticle, Int16 stockRestant)
        {
            StockerBE stock = new StockerBE(codeMagasin, codeArticle, stockDebut, quantiteAchetee, quantiteVendue, dateOperation, annee, puArticle, stockRestant);
            
            if (stockerDA.ajouter(stock))
            {
                journalDA.journaliser("enregistrement d'un approvisionnement de " + quantiteAchetee + " articles de type " + codeArticle + " dans le magasin " + codeMagasin + " au prix unitaire de " + puArticle);
                return true;
            }
            return false;
        }

        //creer un Stock
        public bool creerStock(StockerBE stock)
        {
           if (stockerDA.ajouter(stock))
            {
                journalDA.journaliser("enregistrement d'un approvisionnement de " + stock.quantiteAchetee + " articles de type " + stock.codeArticle + " dans le magasin " + stock.codeMagasin + " au prix unitaire de " + stock.puArticle);
                return true;
            }
            return false;
        }

        // supprimer un stock
        public bool supprinerStock(StockerBE stock)
        {
            if (stockerDA.supprimer(stock))
            {
                journalDA.journaliser("suppresion de l'approvisionnement de " + stock.quantiteAchetee + " articles de type " + stock.codeArticle + " dans le magasin " + stock.codeMagasin + " au prix unitaire de " + stock.puArticle);
                return true;
            }
            return false;
        }

        // modifier un stock
        public bool modifierStock(StockerBE stock, StockerBE newStock)
        {
            if (stockerDA.modifier(stock, newStock))
            {
                journalDA.journaliser("modification de l'approvisionnement de " + stock.quantiteAchetee + " articles de type " + stock.codeArticle + " dans le magasin " + stock.codeMagasin + " au prix unitaire de " + stock.puArticle + ". nouvelle quantité : " + newStock.quantiteAchetee + ", nouveau prix unitaire : " + newStock.puArticle);
                return true;
            }
            return false;
        }

        // rechercher un stock
        public StockerBE rechercherStock(StockerBE stock)
        {

            return stockerDA.rechercher(stock);

        }

        //lister tous les stock
        public List<StockerBE> listerToutesLesStock()
        {
            return stockerDA.listerTous();
        }

        // lister tous les stock respectant un certain critère
        public List<StockerBE> listerStockSuivantCritere(string critere)
        {
            return stockerDA.listerSuivantCritere(critere);
        }

        // retourne la liste des codes de Article deja enregistré (pour le filtre)
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

        // retourne la liste des codes de Article deja enregistré (pour le filtre)
        public List<string> getListCodeArticle2(List<ArticleBE> listArticle)
        {
            List<string> listeCodeArticle = new List<string>();

            listeCodeArticle = new List<string>();
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

        // retourne la liste des codes de Magasin deja enregistré (pour le filtre)
        public List<string> getListCodeMagasin(List<MagasinBE> listMagasin)
        {
            List<string> listeCodeMagasin = new List<string>();

            listeCodeMagasin = new List<string>();
            listeCodeMagasin.Add("<Tous les Magasins>");
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

        // retourne la liste des codes de Magasin deja enregistré (pour le filtre)
        public List<string> getListCodeMagasin2(List<MagasinBE> listMagasin)
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

        //rechercge si un article a deja été vendu
        public Boolean ADejaEteVendu(String codeArticle) {
            return acheterDA.ADejaEteVendu(codeArticle);
        }

        //fonction qui teste si la table stoker est vide
        public Boolean tableStokerIsEmpty() {
            List<StockerBE> LStocker = stockerDA.listerTous();
            if (LStocker == null || LStocker.Count == 0) {
                return true;
            }
            return false;
        }

        //fonction qui retourne le dernier enregistrement de la table stocker
        public StockerBE dernierEnregistrementStocker(string codeArticle, string codeMagasin) {
            return stockerDA.dernierEnregistrement(codeArticle, codeMagasin);
        }

        //fonction qui retourne l'année en cours
        public int getAnneeEnCours() {
            return parametresDA.AnneeEnCours();
        }

    }
}
