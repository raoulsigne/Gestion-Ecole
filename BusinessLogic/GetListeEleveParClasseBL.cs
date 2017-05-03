using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class GetListeEleveParClasseBL
    {
         private ClasseDA classeDA;
         private InscrireDA inscrireDA;
        private EleveDA eleveDA;
        private ParametresDA parametreDA;
        private JournalDA journalDA;

        public GetListeEleveParClasseBL()
        {
            this.classeDA = new ClasseDA();
            this.inscrireDA = new InscrireDA();
            this.eleveDA = new EleveDA();
            this.parametreDA = new ParametresDA();
            this.journalDA = new JournalDA();
        }

        // retourner la liste de toutes les classes de la base de donneés
        public List<ClasseBE> listerToutesLesClasses() {
            return classeDA.listerTous();
        }

        // retourner la liste des inscrits d'une classe et pour une année donnée
        public List<InscrireBE> listeDesEffectifsDuneClassePourUneAnnee(String codeClasse, String annee){
            List<InscrireBE> listInscrireBE = new List<InscrireBE>();
            listInscrireBE = inscrireDA.listerSuivantCritere("codeclasse='"+codeClasse+"' AND annee='"+annee+"'");
            return listInscrireBE;
        }

        //enlever un élève d'une classe
        public bool retireUnEleveDuneClasse(String codeClasse, String matriculeEleve, int annee)
        {
            InscrireBE inscrire = new InscrireBE();
            inscrire.codeClasse = codeClasse;
            inscrire.matricule = matriculeEleve;
            inscrire.annee = annee;

            if (inscrireDA.supprimer(inscrire))
            {
                journalDA.journaliser("suppression de l'inscription de l'élève de matricule " + matriculeEleve + " pour la classe " + codeClasse + ", année : " + annee);
                return true;
            }
            return false;
        }

        //// retourner la liste des inscrits de toutes les classes et pour une année donnée
        //public List<InscrireBE> listeDesEffectifsDesClassePourUneAnnee(String annee)
        //{
        //    List<InscrireBE> listInscrireBE = new List<InscrireBE>();
        //    listInscrireBE = inscrireDA.listerSuivantCritere("annee='" + annee + "'");
        //    return listInscrireBE;
        //}

        //rechercher un élève suivant son matricule
        public EleveBE rechercherEleve(EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
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

        // retourne la liste des codes de Cycle deja enregistré (pour le filtre)
        public List<string> getListCodeClasse(List<ClasseBE> listClasse)
        {
            List<string> listeCodeClasse = new List<string>();

            listeCodeClasse = new List<string>();
            listeCodeClasse.Add("<Toutes Les Classes>");
            if (listClasse != null)
            {
                for (int i = 0; i < listClasse.Count; i++)
                {
                    listeCodeClasse.Add(listClasse.ElementAt(i).codeClasse);
                }
                return listeCodeClasse;
            }
            else return null;
        }
    }
}
