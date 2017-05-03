using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierCycleBL
    {
        private CycleDA cycleDA;
        private JournalDA journalDA;

        public CreerModifierCycleBL()
        {
            this.cycleDA = new CycleDA();
            this.journalDA = new JournalDA();
        }

        //creer un Cycle
        public bool creerCycle(string codeCycle, string nomCycle){
            CycleBE serie = new CycleBE(codeCycle, nomCycle);
            if (cycleDA.ajouter(serie))
            {
                journalDA.journaliser("enregistrement d'un cycle de code " + codeCycle + " et de nom " + nomCycle);
                return true;
            }
            return false;
        }

        // supprimer un Cycle
        public bool supprinerCycle(CycleBE cycle) {
            if (cycleDA.supprimer(cycle))
            {
                journalDA.journaliser("suppression du cycle de code " + cycle.codeCycle + " et de nom " + cycle.nomCycle);
                return true;
            }
            return false;
        }

        // modifier un Cycle
        public bool modifierCycle(CycleBE cycle, CycleBE newcycle)
        {
            if (cycleDA.modifier(cycle, newcycle))
            {
                journalDA.journaliser("modification du cycle de code " + cycle.codeCycle + ". ancien nom : "+cycle.nomCycle+". nouveau code : " + newcycle.codeCycle+", nouveau nom : "+newcycle.nomCycle);
                return true;
            }
            return false;
        }

        // modifier un Cycle
        public bool modifierCycle(CycleBE cycle)
        {
            if (cycleDA.modifier(cycle))
            {
                journalDA.journaliser("modification du cycle de code " + cycle.codeCycle + ". nouveau nom : " + cycle.nomCycle);
                return true;
            }
            return false;
        }

        // rechercher un Cycle
        public CycleBE rechercherCycle(CycleBE cycle)
        {
            return cycleDA.rechercher(cycle);
        }

        //lister tous les Cycles
        public List<CycleBE> listerToutesLesCycle() {
            return cycleDA.listerTous();
        }

        // lister tous les Cycle respectant un certain critère
        public List<CycleBE> listerCycleSuivantCritere(string critere) {
            return cycleDA.listerSuivantCritere(critere);
        }

        // retourne la liste des codes de Cycle deja enregistré (pour le filtre)
        public List<string> getListCodeCycle(List<CycleBE> listCycle)
        {
            List<string> listeCodeCycle = new List<string>();

            listeCodeCycle = new List<string>();
            listeCodeCycle.Add("<Tous les Codes>");
            if (listCycle != null)
            {
                for (int i = 0; i < listCycle.Count; i++)
                {
                    listeCodeCycle.Add(listCycle.ElementAt(i).codeCycle);
                }
                //listeCodeCycle.Add("Tous");
                return listeCodeCycle;
            }
            else return null;
        }

        // retourne la liste des Noms de Cycle deja enregistré (pour le filtre)
        public List<string> getListNomCycle(List<CycleBE> listCycle)
        {
            List<string> listeNomCycle = new List<string>();

            listeNomCycle = new List<string>();
            listeNomCycle.Add("<Tous les Noms>");
            if (listCycle != null)
            {
                for (int i = 0; i < listCycle.Count; i++)
                {
                    listeNomCycle.Add(listCycle.ElementAt(i).nomCycle);
                }
                //listeNomCycle.Add("Tous");
                return listeNomCycle;
            }
            else return null;
        }
    }
}
