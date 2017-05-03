using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.DataAccess;
using Ecole.BusinessEntity;
using System.Globalization;
using System.Threading;

namespace Ecole.BusinessLogic
{
    public class GestionArticleBL
    {
        private SetarticleDA setarticleDA;
        private EleveDA eleveDA;
        private AcheterDA acheterDA;
        private ArticleDA articleDA;
        private ComposerDA composerDA;
        private StockerDA stockerDA;
        private List<ComposerBE> composers;
        private List<StockerBE> stockers;
        private ParametresDA parametreDA;
        private JournalDA journalDA;
        private InscrireDA inscrireDA;
        private ClasseDA classeDA;

        public GestionArticleBL()
        {
            articleDA = new ArticleDA();
            setarticleDA = new SetarticleDA();
            eleveDA = new EleveDA();
            acheterDA = new AcheterDA();
            composerDA = new ComposerDA();
            stockerDA = new StockerDA();
            composers = new List<ComposerBE>();
            stockers = new List<StockerBE>();
            parametreDA = new ParametresDA();
            journalDA = new JournalDA();
            inscrireDA = new InscrireDA();
            classeDA = new ClasseDA();
        }

        internal List<AcheterBE> listerTousAcheter()
        {
            return acheterDA.listerTous();
        }

        internal List<string> listerValeursColonneSetArticle(string p)
        {
            return setarticleDA.listerValeursColonne(p);
        }

        internal SetarticleBE rechercherSetArticle(SetarticleBE setarticle)
        {
            return setarticleDA.rechercher(setarticle);
        }

        internal EleveBE rechercherEleve(EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
        }

        internal List<AcheterBE> listerSuivantCritereAcheter(string p)
        {
            return acheterDA.listerSuivantCritere(p);
        }

        internal bool ajouterAcheter(AcheterBE acheter)
        {
            if (acheterDA.ajouter(acheter))
            {
                journalDA.journaliser("Enregistrement de l'achat d'un article - " + acheter.codesetarticle + " - " + acheter.matricule);
                return true;
            }
            else
                return false;
        }

        internal AcheterBE rechercherAcheter(AcheterBE acheter)
        {
            return acheterDA.rechercher(acheter);
        }

        internal void supprimerAcheter(AcheterBE acheter)
        {
            if (acheterDA.supprimer(acheter))
            {
                journalDA.journaliser("Suppression de l'achat d'un article - " + acheter.codesetarticle + " - " + acheter.matricule);
            }
        }

        internal void decrementerStock(SetarticleBE setarticle, int nombre, int annee)
        {
            if (setarticle != null)
            {
                StockerBE stocker = new StockerBE();
                composers = composerDA.listerSuivantCritere("codesetarticle LIKE " + "'" + setarticle.codesetarticle + "'");
                foreach (ComposerBE c in composers)
                {
                    stocker = new StockerBE();
                    stocker.annee = annee;
                    stocker.codeArticle = c.codeArticle;
                    StockerBE dernier_stock = new StockerBE();
                    dernier_stock.codeArticle = c.codeArticle;
                    dernier_stock = stockerDA.rechercherDernierEnregistrement(stocker);
                    if (dernier_stock != null)
                    {
                        if (dernier_stock.annee < stocker.annee)
                            stocker.stockDebut = dernier_stock.stockRestant;
                        else
                            stocker.stockDebut = dernier_stock.stockDebut;
                        stocker.stockRestant = dernier_stock.stockRestant - (nombre * c.quantite);
                        stocker.quantiteVendue = (nombre * c.quantite);
                        stocker.quantiteAchetee = 0;
                        stocker.puArticle = dernier_stock.puArticle;
                        stocker.dateOperation = DateTime.Today;
                        stocker.codeMagasin = dernier_stock.codeMagasin;

                        stockerDA.ajouter(stocker);
                    }
                }
            }
        }

        internal void incrementerStock(SetarticleBE setarticle, int nombre, int annee)
        {
            if (setarticle != null)
            {
                StockerBE stocker = new StockerBE();
                composers = composerDA.listerSuivantCritere("codesetarticle LIKE " + "'" + setarticle.codesetarticle + "'");
                foreach (ComposerBE c in composers)
                {
                    stocker = new StockerBE();
                    stocker.annee = annee;
                    stocker.codeArticle = c.codeArticle;
                    StockerBE dernier_stock = new StockerBE();
                    dernier_stock.codeArticle = c.codeArticle;
                    dernier_stock = stockerDA.rechercherDernierEnregistrement(stocker);
                    if (dernier_stock != null)
                    {
                        if (dernier_stock.annee < stocker.annee)
                            stocker.stockDebut = dernier_stock.stockRestant;
                        else
                            stocker.stockDebut = dernier_stock.stockDebut;
                        stocker.stockRestant = dernier_stock.stockRestant + (nombre * c.quantite);
                        stocker.quantiteVendue = -(nombre * c.quantite);
                        stocker.quantiteAchetee = 0;
                        stocker.puArticle = dernier_stock.puArticle;
                        stocker.dateOperation = DateTime.Today;
                        stocker.codeMagasin = dernier_stock.codeMagasin;

                        stockerDA.ajouter(stocker);
                    }
                }
            }
        }

        public bool disponibliteStock(string codesetarticle, int quantite, int annee)
        {
            return stockerDA.disponibiliteSetArticle(codesetarticle, quantite, annee);
        }

        internal List<AcheterBE> listerSuivantCritereAcheters(string p)
        {
            return acheterDA.listerSuivantCritere(p);
        }

        internal int anneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        internal string rechercherNumeroAcheter(AcheterBE acheter)
        {
            return acheterDA.rechercherNumero(acheter).ToString();
        }

        internal int rechercherNumeroAchat(AcheterBE acheter)
        {
            return acheterDA.rechercherNumero(acheter);
        }

        internal List<string> listerValeurColonneClasse(string p)
        {
            return classeDA.listerValeursColonne(p);
        }

        internal List<InscrireBE> listerSuivantCritereInscrits(string p)
        {
            return inscrireDA.listerSuivantCritere(p);
        }

        internal InscrireBE rechercherInscrire(InscrireBE inscrire)
        {
            return inscrireDA.rechercherClasse(inscrire);
        }

        internal void decrementerStock(string codearticle, int nombre, int annee, DateTime dateoperation, int numerovente)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            StockerBE stocker = new StockerBE();
            stocker.annee = annee;
            stocker.codeArticle = codearticle;
            StockerBE dernier_stock = new StockerBE();
            dernier_stock.codeArticle = codearticle;
            dernier_stock = stockerDA.rechercherDernierEnregistrement(stocker);
            if (dernier_stock != null)
            {
                if (dernier_stock.annee < stocker.annee)
                    stocker.stockDebut = dernier_stock.stockRestant;
                else
                    stocker.stockDebut = dernier_stock.stockDebut;
                stocker.stockRestant = dernier_stock.stockRestant - nombre;
                stocker.quantiteVendue = nombre;
                stocker.quantiteAchetee = 0;
                stocker.puArticle = dernier_stock.puArticle;
                stocker.dateOperation = dateoperation;
                stocker.codeMagasin = dernier_stock.codeMagasin;
                stocker.numeroVente = numerovente;

                stockerDA.ajouter(stocker, numerovente);
            }
        }

        internal void incrementerStock(string codearticle, int nombre, int annee, DateTime dateoperation, int numerovente)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;
            
            StockerBE stocker = new StockerBE();
            stocker.annee = annee;
            stocker.codeArticle = codearticle;
            StockerBE dernier_stock = new StockerBE();
            dernier_stock.codeArticle = codearticle;
            dernier_stock = stockerDA.rechercherDernierEnregistrement(stocker);
            if (dernier_stock != null)
            {
                if (dernier_stock.annee < stocker.annee)
                    stocker.stockDebut = dernier_stock.stockRestant;
                else
                    stocker.stockDebut = dernier_stock.stockDebut;
                stocker.stockRestant = dernier_stock.stockRestant + nombre;
                stocker.quantiteVendue = -nombre;
                stocker.quantiteAchetee = 0;
                stocker.puArticle = dernier_stock.puArticle;
                stocker.dateOperation = dateoperation;
                stocker.codeMagasin = dernier_stock.codeMagasin;
                stocker.numeroVente = numerovente;

                stockerDA.ajouter(stocker);
            }
        }

        internal List<ArticleBE> listerTousLesArticle()
        {
            return articleDA.listerTous();
        }

        internal List<EleveBE> listerElevesDuneClasse(string codeclasse, int annee)
        {
            ClasseBE c = new ClasseBE();
            c.codeClasse = codeclasse;
            c = classeDA.rechercher(c);
            return classeDA.listeEleves(c, annee);
        }

        internal bool ajouterSetArticle(SetarticleBE set)
        {
            return setarticleDA.ajouter(set);
        }

        internal List<StockerBE> rechercherLigneStocker(int numero)
        {
            return stockerDA.rechercherParNumeroVente(numero);
        }

        internal ArticleBE rechercherArticle(ArticleBE article)
        {
            return articleDA.rechercher(article);
        }
    }
}
