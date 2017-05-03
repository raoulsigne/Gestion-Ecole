using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierTrimestreBL
    {
        private TrimestreDA trimestreDA;
        private JournalDA journalDA;

        public CreerModifierTrimestreBL()
        {
            this.trimestreDA = new TrimestreDA();
            this.journalDA = new JournalDA();
        }

        //creer un Trimestre
        public bool creerTrimestre(string codeTrimestre, string nomTrimestre)
        {
            TrimestreBE trimestre = new TrimestreBE(codeTrimestre, nomTrimestre);
            if (trimestreDA.ajouter(trimestre))
            {
                journalDA.journaliser("enregistrement d'un trimestre de code " + codeTrimestre + " et de nom " + nomTrimestre);
                return true;
            }
            return false;
        }

        // supprimer un Trimestre
        public bool supprinertrimestre(TrimestreBE trimestre)
        {
            if (trimestreDA.supprimer(trimestre))
            {
                journalDA.journaliser("suppression d'un trimestre de code " + trimestre.codetrimestre + " et de nom " + trimestre.nomtrimestre);
                return true;
            }
            return false;
        }

        // modifier un Trimestree
        public bool modifierTrimestre(TrimestreBE trimestre, TrimestreBE newTrimestre)
        {
            if (trimestreDA.modifier(trimestre, newTrimestre))
            {
                journalDA.journaliser("modification du trimestre de code " + trimestre.codetrimestre + ". ancien nom : " + trimestre.nomtrimestre + ". nouveau code : " + newTrimestre.codetrimestre + ", nouveau nom : " + newTrimestre.nomtrimestre);
                return true;
            }
            return false;
        }

        // modifier un Trimestre
        public bool modifierTrimestre(TrimestreBE trimestre)
        {
            if (trimestreDA.modifier(trimestre))
            {
                journalDA.journaliser("modification du trimestre de code " + trimestre.codetrimestre + ". nouveau nom : " + trimestre.nomtrimestre);
                return true;
            }
            return false;
        }

        // rechercher un Trimestre
        public TrimestreBE rechercherTrimestre(TrimestreBE trimestre)
        {
            return trimestreDA.rechercher(trimestre);
        }

        //lister tous les Trimestres
        public List<TrimestreBE> listerTousLesTrimestres()
        {
            return trimestreDA.listerTous();
        }

        // lister tous les Trimestres respectant un certain critère
        public List<TrimestreBE> listerTrimestreSuivantCritere(string critere)
        {
            return trimestreDA.listerSuivantCritere(critere);
        }

        // retourne la liste des codes de Trimestre deja enregistré (pour le filtre)
        public List<string> getListCodeTrimestre(List<TrimestreBE> listTrimestre)
        {
            List<string> listeCodeTrimestre = new List<string>();

            listeCodeTrimestre = new List<string>();
            listeCodeTrimestre.Add("<Tous les Codes>");
            if (listTrimestre != null)
            {
                for (int i = 0; i < listTrimestre.Count; i++)
                {
                    listeCodeTrimestre.Add(listTrimestre.ElementAt(i).codetrimestre);
                }
                //listeCodeTrimestre.Add("Tous");
                return listeCodeTrimestre;
            }
            else return null;
        }

        // retourne la liste des Noms de Trimestre deja enregistré (pour le filtre)
        public List<string> getListNomTrimestre(List<TrimestreBE> listTrimestre)
        {
            List<string> listeNomTrimestre = new List<string>();

            listeNomTrimestre = new List<string>();
            listeNomTrimestre.Add("<Tous les Noms>");
            if (listTrimestre != null)
            {
                for (int i = 0; i < listTrimestre.Count; i++)
                {
                    listeNomTrimestre.Add(listTrimestre.ElementAt(i).nomtrimestre);
                }
                //listeNomTrimestre.Add("Tous");
                return listeNomTrimestre;
            }
            else return null;
        }
    }
}
