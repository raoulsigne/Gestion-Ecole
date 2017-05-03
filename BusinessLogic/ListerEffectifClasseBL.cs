using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{ // Fenêtre servant à Lister / Editer la liste des Effectifs par classe et par niveau
    class ListerEffectifClasseBL
    {
        private ClasseDA classeDA;
        private InscrireDA inscrireDA;
        private EleveDA eleveDA;
        private ParametresDA parametreDA;
        private AppartenirDA appartenirDA;

        public ListerEffectifClasseBL()
        {
            this.classeDA = new ClasseDA();
            this.inscrireDA = new InscrireDA();
            this.eleveDA = new EleveDA();
            this.parametreDA = new ParametresDA();
            this.appartenirDA = new AppartenirDA();
        }

        // retourner la liste de toutes les classes de la base de donneés
        public List<ClasseBE> listerToutesLesClasses()
        {
            return classeDA.listerTous();
        }

        // retourner la liste des inscrits de toutes les classes et pour une année donnée
        public List<InscrireBE> listeDesEffectifsDeToutesLesClassePourUneAnnee(String annee)
        {
            List<InscrireBE> listInscrireBE = new List<InscrireBE>();
            listInscrireBE = inscrireDA.listerSuivantCritere("annee='" + annee + "'");
            return listInscrireBE;
        }

        // retourner la liste des inscrits d'une classe et pour une année donnée
        public List<InscrireBE> listeDesEffectifsDuneClassePourUneAnnee(String codeClasse, String annee)
        {
            List<InscrireBE> listInscrireBE = new List<InscrireBE>();
            listInscrireBE = inscrireDA.listerSuivantCritere("codeclasse='" + codeClasse + "' AND annee='" + annee + "'");
            return listInscrireBE;
        }

        //rechercher un élève suivant son matricule
        public EleveBE rechercherEleve(EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
        }

        //rechercher une classe suivant son code
        public ClasseBE rechercherClasse(ClasseBE classe)
        {
            return classeDA.rechercher(classe);
        }

        //rechercher une classe suivant son code
        public List<AppartenirBE> ListerAppartenirSuivantCritere(String critere)
        {
            return appartenirDA.listerSuivantCritere(critere);
        }

        //enlever un élève d'une classe
        public bool retireUnEleveDuneClasse(String codeClasse, String matriculeEleve, int annee)
        {
            InscrireBE inscrire = new InscrireBE();
            inscrire.codeClasse = codeClasse;
            inscrire.matricule = matriculeEleve;
            inscrire.annee = annee;

            return inscrireDA.supprimer(inscrire);
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
            listeCodeClasse.Add("<Toutes les Classes>");

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
