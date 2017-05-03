using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierSerieBL
    {
        private SerieDA serieDA;
        private JournalDA journalDA;

        public CreerModifierSerieBL()
        {
            this.serieDA = new SerieDA();
            this.journalDA = new JournalDA();
        }

        //creer une série
        public bool creerSerie(string codeSerie, string nomSerie){
            SerieBE serie = new SerieBE(codeSerie, nomSerie);
            if (serieDA.ajouter(serie))
            {
                journalDA.journaliser("enregistrement d'une série de code " + codeSerie + " et de nom " + nomSerie);
                return true;
            }
            return false;
        }

        // supprimer une série
        public bool supprinerSerie(SerieBE serie) {
            if (serieDA.supprimer(serie))
            {
                journalDA.journaliser("suppression de la série de code " + serie.codeserie + " et de nom " + serie.nomserie);
                return true;
            }
            return false;
        }

        // modifier une série
        public bool modifierSerie(SerieBE serie, SerieBE newserie) {
            if (serieDA.modifier(serie, newserie))
            {
                journalDA.journaliser("modification de la série de code " + serie.codeserie + ". ancien nom : " + serie.nomserie + ". nouveau code : " + newserie.codeserie + ", nouveau nom : " + newserie.nomserie);
                return true;
            }
            return false;
        }

        // modifier une série
        public bool modifierSerie(SerieBE serie)
        {
            if (serieDA.modifier(serie))
            {
                journalDA.journaliser("modification de la série de code " + serie.codeserie + ". nouveau nom : " + serie.nomserie);
                return true;
            }
            return false;
        }

        // remplacer une série
        public bool remplacerSerie(SerieBE serie)
        {
            if (serieDA.remplacer(serie))
            {
                journalDA.journaliser("modification de la série de code " + serie.codeserie + ". nouveau nom : " + serie.nomserie);
                return true;
            }
            return false;
        }

        // rechercher une série
        public SerieBE rechercherSerie(SerieBE serie)
        {
            return serieDA.rechercher(serie);
        }

        //lister toutes les série
        public List<SerieBE> listerToutesLesSeries() {
            return serieDA.listerTous();
        }

        // lister toutes les séries respectant un certain critère
        public List<SerieBE> listerSerieSuivantCritere(string critere) {
            return serieDA.listerSuivantCritere(critere);
        }

        // retourne la liste des codes de série deja enregistré (pour le filtre)
        public List<string> getListCodeSerie(List<SerieBE> listSerie)
        {
            List<string> listeCodeSerie = new List<string>();

            listeCodeSerie = new List<string>();
            listeCodeSerie.Add("<Tous les Codes>");
            if (listSerie != null)
            {
                for (int i = 0; i < listSerie.Count; i++)
                {
                    listeCodeSerie.Add(listSerie.ElementAt(i).codeserie);
                }
                //listeCodeSerie.Add("Tous");
                return listeCodeSerie;
            }
            else return null;
        }

        // retourne la liste des Noms de série deja enregistré (pour le filtre)
        public List<string> getListNomSerie(List<SerieBE> listSerie)
        {
            List<string> listeNomSerie = new List<string>();

            listeNomSerie = new List<string>();
            listeNomSerie.Add("<Tous les Noms>");
            if (listSerie != null)
            {
                for (int i = 0; i < listSerie.Count; i++)
                {
                    listeNomSerie.Add(listSerie.ElementAt(i).nomserie);
                }
                //listeNomSerie.Add("Tous");
                return listeNomSerie;
            }
            else return null;
        }

    }
}
