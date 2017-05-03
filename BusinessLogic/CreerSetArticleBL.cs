using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    class CreerSetArticleBL
    {
        private SetarticleDA setArticleDA;
        private ComposerDA composerDA;
        private ParametresDA parametreDA;
        private JournalDA journalDA;

        public CreerSetArticleBL()
        {
            this.setArticleDA = new SetarticleDA();
            this.composerDA = new ComposerDA();
            this.parametreDA = new ParametresDA();
            this.journalDA = new JournalDA();
        }

        //creer un Set Article
        public bool creerSetArticle(SetarticleBE setArticle)
        {
            if (setArticleDA.ajouter(setArticle))
            {
                journalDA.journaliser("enregistrement d'un set article de code " + setArticle.codesetarticle + " et de nom " + setArticle.nomsetarticle);
                return true;
            }
            return false;
        }

        //creer une composition de Set Article
        public bool creerCompositionSetArticle(ComposerBE composer)
        {
            if (composerDA.ajouter(composer))
            {
                journalDA.journaliser("ajout de " + composer.quantite + " articles de code " + composer.codeArticle + " dans la composition du set article de code " + composer.codeSetArticle + " pour l'année " + composer.annee);
                return true;
            }
            return false;
        }

        // supprimer un set Article
        public bool supprinerSetArticle(SetarticleBE setArticle)
        {
            if (setArticleDA.supprimer(setArticle))
            {
                journalDA.journaliser("suppression du set article de code " + setArticle.codesetarticle + " et de nom " + setArticle.nomsetarticle);
                return true;
            }
            return false;
        }

        // modifier un set Article
        public bool modifierSetArticle(SetarticleBE setArticle, SetarticleBE newSetArticle)
        {
            if (setArticleDA.modifier(setArticle, newSetArticle))
            {
                journalDA.journaliser("modification du set article de code " + setArticle.codesetarticle + ". nouveau code : " + newSetArticle.codesetarticle + ", nouveau nom : " + newSetArticle.nomsetarticle);
                return true;
            }
            return false;
        }

        // modifier un set Article
        public bool modifierSetArticle(SetarticleBE setArticle)
        {
            if (setArticleDA.modifier(setArticle))
            {
                journalDA.journaliser("modification du set article de code " + setArticle.codesetarticle + ". nouveau code : " + setArticle.codesetarticle + ", nouveau nom : " + setArticle.nomsetarticle);
                return true;
            }
            return false;
        }

        // rechercher un set Article
        public SetarticleBE rechercherSetArticle(SetarticleBE setArticle)
        {
            return setArticleDA.rechercher(setArticle);
        }

        //lister tous les set Article
        public List<SetarticleBE> listerTousLesSetArticle()
        {
            return setArticleDA.listerTous();
        }

        // lister tous les Article respectant un certain critère
        public List<SetarticleBE> listerSetArticleSuivantCritere(string critere)
        {
            return setArticleDA.listerSuivantCritere(critere);
        }

        //lister tous les éléments de la table Composer
        public List<ComposerBE> listerTousComposer()
        {
            return composerDA.listerTous();
        }

        //lister tous les éléments de la table Composer respectant un critère
        public List<ComposerBE> listerComposerSuivantCrietere(string critere)
        {
            return composerDA.listerSuivantCritere(critere);
        }

        //lister tous les éléments de la table Composer respectant un critère
        public bool supprimerComposer(ComposerBE composer)
        {
            if (composerDA.supprimer(composer))
            {
                journalDA.journaliser("suppression de l'article de code " + composer.codeArticle + " dans le set article de code " + composer.codeSetArticle + " pour l'année " + composer.annee);
                return true;
            }
            return false;
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
    }
}
