using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    class CreerModifierMagasinBL
    {
        private MagasinDA magasinDA;
        private JournalDA journalDA;

        public CreerModifierMagasinBL()
        {
            this.magasinDA = new MagasinDA();
            this.journalDA = new JournalDA();
        }

        //creer un Magasin
        public bool creerMagasin(string codeMagasin, string nomMagasin)
        {
            MagasinBE magasin = new MagasinBE(codeMagasin, nomMagasin);
            if (magasinDA.ajouter(magasin))
            {
                journalDA.journaliser("enregistrement d'un magasin de code " + codeMagasin + " et de nom " + nomMagasin);
                return true;
            }
            return false;
        }

        // supprimer un Magasin
        public bool supprinerMagasin(MagasinBE magasin)
        {
            if (magasinDA.supprimer(magasin))
            {
                journalDA.journaliser("suppression du magasin de code " + magasin.codeMagasin + " et de nom " + magasin.nomMagasin);
                return true;
            }
            return false;
        }

        // modifier un Magasin
        public bool modifierMagasin(MagasinBE magasin, MagasinBE newmagasin)
        {
            if (magasinDA.modifier(magasin, newmagasin))
            {
                journalDA.journaliser("modification du magasin de code " + magasin.codeMagasin + ". ancien nom : " + magasin.nomMagasin + ". nouveau code : " + magasin.codeMagasin + ", nouveau nom : " + magasin.nomMagasin);
                return true;
            }
            return false;
        }

        // modifier un Magasin
        public bool modifierMagasin(MagasinBE magasin)
        {
            if (magasinDA.modifier(magasin))
            {
                journalDA.journaliser("modification du magasin de code " + magasin.codeMagasin + ". nouveau nom : " + magasin.nomMagasin);
                return true;
            }
            return false;
        }

        // rechercher un Magasin
        public MagasinBE rechercherMagasin(MagasinBE magasin)
        {
            return magasinDA.rechercher(magasin);
        }

        //lister tous les Magasins
        public List<MagasinBE> listerToutesLesMagasin()
        {
            return magasinDA.listerTous();
        }

        // lister tous les Magasins respectant un certain critère
        public List<MagasinBE> listerMagasinSuivantCritere(string critere)
        {
            return magasinDA.listerSuivantCritere(critere);
        }

        // retourne la liste des codes de Magasin deja enregistré (pour le filtre)
        public List<string> getListCodeMagasin(List<MagasinBE> listMagasin)
        {
            List<string> listeCodeMagasin = new List<string>();

            listeCodeMagasin = new List<string>();
            listeCodeMagasin.Add("<Tous les Codes>");
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

        // retourne la liste des Noms de Magasin deja enregistré (pour le filtre)
        public List<string> getListNomMagasin(List<MagasinBE> listMagasin)
        {
            List<string> listeNomMagasin = new List<string>();

            listeNomMagasin = new List<string>();
            listeNomMagasin.Add("<Tous les Noms>");
            if (listMagasin != null)
            {
                for (int i = 0; i < listMagasin.Count; i++)
                {
                    listeNomMagasin.Add(listMagasin.ElementAt(i).nomMagasin);
                }
                //listeNomMagasin.Add("Tous");
                return listeNomMagasin;
            }
            else return null;
        }
    }
}
