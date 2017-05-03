using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierNiveauBL
    {
        private NiveauDA niveauDA;
        private JournalDA journalDA;

        public CreerModifierNiveauBL()
        {
            this.niveauDA = new NiveauDA();
            this.journalDA = new JournalDA();
        }

        //creer une niveau
        public bool creerNiveau(string codeNiveau, string nomNiveau, int niveau)
        {
            NiveauBE niveauBE = new NiveauBE(codeNiveau, nomNiveau, niveau);
            if (niveauDA.ajouter(niveauBE))
            {
                journalDA.journaliser("enregistrement d'un niveau de code " + codeNiveau + " et de nom " + nomNiveau);
                return true;
            }
            return false;
        }

        // supprimer une niveau
        public bool supprinerNiveau(NiveauBE niveau)
        {
            if (niveauDA.supprimer(niveau))
            {
                journalDA.journaliser("suppression du niveau de code " + niveau.codeNiveau + " et de nom " + niveau.nomNiveau);
                return true;
            }
            return false;
        }

        // modifier une niveau
        public bool modifierNiveau(NiveauBE niveau, NiveauBE newniveau)
        {
            if (niveauDA.modifier(niveau, newniveau))
            {
                journalDA.journaliser("modification du niveau de code " + niveau.codeNiveau + ". ancien nom : " + niveau.nomNiveau + ". nouveau code : " + newniveau.codeNiveau + ", nouveau nom : " + newniveau.nomNiveau);
                return true;
            }
            return false;
        }

        // modifier une niveau
        public bool modifierNiveau(NiveauBE niveau)
        {
            if (niveauDA.modifier(niveau))
            {
                journalDA.journaliser("modification du niveau de code " + niveau.codeNiveau + ". nouveau nom : " + niveau.nomNiveau);
                return true;
            }
            return false;
        }

        // rechercher une niveau
        public NiveauBE rechercherNiveau(NiveauBE NiveauBE)
        {
            return niveauDA.rechercher(NiveauBE);
        }

        //lister toutes les niveaux
        public List<NiveauBE> listerTousLesNiveaux() {
            return niveauDA.listerTous();
        }

        // lister toutes les niveaux respectant un certain critère
        public List<NiveauBE> listerNiveauSuivantCritere(string critere)
        {
            return niveauDA.listerSuivantCritere(critere);
        }

        // retourne la liste des codes des niveaux deja enregistré (pour le filtre)
        public List<string> getListCodeNiveau(List<NiveauBE> listNiveau)
        {
            List<string> listeCodeNiveau = new List<string>();

            listeCodeNiveau = new List<string>();
            listeCodeNiveau.Add("<Tous les Codes>");
            if (listNiveau != null)
            {
                for (int i = 0; i < listNiveau.Count; i++)
                {
                    listeCodeNiveau.Add(listNiveau.ElementAt(i).codeNiveau);
                }
                //listeCodeNiveau.Add("Tous");
                return listeCodeNiveau;
            }
            else return null;
        }

        // retourne la liste des noms des niveaux deja enregistré (pour le filtre)
        public List<string> getListNomNiveau(List<NiveauBE> listNiveau)
        {
            List<string> listeNomNiveau = new List<string>();

            listeNomNiveau = new List<string>();
            listeNomNiveau.Add("<Tous les Noms>");
            if (listNiveau != null)
            {
                for (int i = 0; i < listNiveau.Count; i++)
                {
                    listeNomNiveau.Add(listNiveau.ElementAt(i).nomNiveau);
                }
                //listeNomNiveau.Add("Tous");
                return listeNomNiveau;
            }
            else return null;
        }
    }
}
