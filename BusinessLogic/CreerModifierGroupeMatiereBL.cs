using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierGroupeMatiereBL
    {
        private GroupeMatiereDA groupeMatiereDA;
        private JournalDA journalDA;

        public CreerModifierGroupeMatiereBL()
        {
            this.groupeMatiereDA = new GroupeMatiereDA();
            this.journalDA = new JournalDA();
        }

        //creer un GroupeMatiere
        public bool creerGroupeMatiere(string codeGroupeMatiere, string nomGroupeMatiere)
        {
            GroupeMatiereBE groupeMatiere = new GroupeMatiereBE(codeGroupeMatiere, nomGroupeMatiere);
            if (groupeMatiereDA.ajouter(groupeMatiere))
            {
                journalDA.journaliser("enregistrement d'un groupe de matière de code " + codeGroupeMatiere + " et de nom " + nomGroupeMatiere);
                return true;
            }
            return false;
        }

        // supprimer une GroupeMatiere
        public bool supprinerGroupeMatiere(GroupeMatiereBE groupeMatiere)
        {
            if (groupeMatiereDA.supprimer(groupeMatiere))
            {
                journalDA.journaliser("suppression du groupe de matière de code " + groupeMatiere.codegroupe + " et de nom " + groupeMatiere.nomgroupe);
                return true;
            }
            return false;
        }

        // modifier un GroupeMatiere
        public bool modifierGroupeMatiere(GroupeMatiereBE groupeMatiere, GroupeMatiereBE newGroupeMatiere)
        {
            if (groupeMatiereDA.modifier(groupeMatiere, newGroupeMatiere))
            {
                journalDA.journaliser("modification du groupe de matière code " + groupeMatiere.codegroupe + ". ancien nom : " + groupeMatiere.nomgroupe + ". nouveau code : " + newGroupeMatiere.codegroupe + ", nouveau nom : " + newGroupeMatiere.nomgroupe);
                return true;
            }
            return false;
        }


        // modifier un GroupeMatiere
        public bool modifierGroupeMatiere(GroupeMatiereBE groupeMatiere)
        {
            if (groupeMatiereDA.modifier(groupeMatiere))
            {
                journalDA.journaliser("modification du groupe de matière code " + groupeMatiere.codegroupe + ". nouveau nom : " + groupeMatiere.nomgroupe);
                return true;
            }
            return false;
        }

        // remplacer un GroupeMatiere
        //public bool remplacerGroupeMatiere(GroupeMatiereBE groupeMatiere)
        //{
        //    return groupeMatiereDA.remplacer(groupeMatiere);
        //}

        // rechercher un GroupeMatiere
        public GroupeMatiereBE rechercherGroupeMatiere(GroupeMatiereBE groupeMatiere)
        {
            return groupeMatiereDA.rechercher(groupeMatiere);
        }

        //lister toutes les GroupeMatiere
        public List<GroupeMatiereBE> listerTousLesGroupeMatieres()
        {
            return groupeMatiereDA.listerTous();
        }

        // lister toutes les GroupeMatiere respectant un certain critère
        public List<GroupeMatiereBE> listerGroupeMatiereSuivantCritere(string critere)
        {
            return groupeMatiereDA.listerSuivantCritere(critere);
        }

        // retourne la liste des codes de série deja enregistré (pour le filtre)
        public List<string> getListCodeGroupeMatiere(List<GroupeMatiereBE> listGroupeMatiere)
        {
            List<string> listeCodeGroupeMatiere = new List<string>();

            listeCodeGroupeMatiere = new List<string>();
            listeCodeGroupeMatiere.Add("<Tous les Codes>");
            if (listGroupeMatiere != null)
            {
                for (int i = 0; i < listGroupeMatiere.Count; i++)
                {
                    listeCodeGroupeMatiere.Add(listGroupeMatiere.ElementAt(i).codegroupe);
                }
                //listeCodeGroupeMatiere.Add("Tous");
                return listeCodeGroupeMatiere;
            }
            else return null;
        }

        // retourne la liste des Noms de GroupeMatiere deja enregistré (pour le filtre)
        public List<string> getListNomGroupeMatiere(List<GroupeMatiereBE> listGroupeMatiere)
        {
            List<string> listeNomGroupeMatiere = new List<string>();

            listeNomGroupeMatiere = new List<string>();
            listeNomGroupeMatiere.Add("<Tous les Noms>");
            if (listGroupeMatiere != null)
            {
                for (int i = 0; i < listGroupeMatiere.Count; i++)
                {
                    listeNomGroupeMatiere.Add(listGroupeMatiere.ElementAt(i).nomgroupe);
                }
                //listeNomGroupeMatiere.Add("Tous");
                return listeNomGroupeMatiere;
            }
            else return null;
        }
    }
}
