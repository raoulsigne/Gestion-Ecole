using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class TitularisationEnseignantBL
    {
        private DirigerDA dirigerDA;
        private ClasseDA classeDA;
        private EnseignantDA enseignantDA;
        private ProgrammerDA programmerDA;
        private ParametresDA parametreDA;
        private ResultatDA resultatDA;
        private JournalDA journalDA;

        public TitularisationEnseignantBL()
        {
            this.dirigerDA = new DirigerDA();
            this.classeDA = new ClasseDA();
            this.enseignantDA = new EnseignantDA();
            this.programmerDA = new ProgrammerDA();
            this.parametreDA = new ParametresDA();
            this.resultatDA = new ResultatDA();
            this.journalDA = new JournalDA();
        }

        //creer une Titularisation
        public bool creerTitularisation(string codeClasse, string idprof, int annee)
        {
            DirigerBE diriger = new DirigerBE(codeClasse, idprof, annee);
            if (dirigerDA.ajouter(diriger))
            {
                journalDA.journaliser("définition de l'enseignant de matricule " + idprof + " comme titulaire de la classe " + codeClasse + " pour l'année : " + annee);
                return true;
            }
            return false;
        }

        // supprimer une Titularisation
        public bool supprinerTitularisation(DirigerBE diriger)
        {
            if (dirigerDA.supprimer(diriger))
            {
                journalDA.journaliser("suppression de la titularisation de l'enseignant de matricule " + diriger.codeProf + " pour la classe " + diriger.codeClasse + " de l'année : " + diriger.annee);
                return true;
            }
            return false;
        }

        // modifier une Titularisation
        public bool modifierTitularisation(DirigerBE diriger, DirigerBE newDiriger)
        {
            if (dirigerDA.modifier(diriger, newDiriger))
            {
                journalDA.journaliser("modification du professeur titulaire de la classe " + diriger.codeClasse + " pour l'année " + diriger.annee + ". code de l'ancien titulaire : " + diriger.codeProf + ", code du nouveau titulaire : " + diriger.codeProf);
                return true;
            }
            return false;
        }

        // rechercher une Titularisation
        public DirigerBE rechercherTitularisation(DirigerBE diriger)
        {
            return dirigerDA.rechercher(diriger);
        }

        // rechercher un enseignant suivant certains critères
        public EnseignantBE rechercherEnseignant(EnseignantBE enseignant)
        {
            return enseignantDA.rechercher(enseignant);
        }

        //lister toutes les Titularisations
        public List<DirigerBE> listerToutesLesTitularisations()
        {
            return dirigerDA.listerTous();
        }

        // lister toutes les Titularisations respectant un certain critère
        public List<DirigerBE> listerTitularisationsSuivantCritere(string critere)
        {
            return dirigerDA.listerSuivantCritere(critere);
        }

        //lister toutes les Classes
        public List<ClasseBE> listerToutesLesClasses()
        {
            return classeDA.listerTous();
        }

        // lister toutes les Classes respectant un certain critère
        public List<ClasseBE> listerClassesSuivantCritere(string critere)
        {
            return classeDA.listerSuivantCritere(critere);
        }

        //lister tous les Resultats
        public List<ResultatBE> listerTousLesResultats()
        {
            return resultatDA.listerTous();
        }

        // lister tous les Resultats respectant un certain critère
        public List<ResultatBE> listerResultatsSuivantCritere(string critere)
        {
            return resultatDA.listerSuivantCritere(critere);
        }

        // retourne la liste des enseignants d'une Classe
        public List<EnseignantBE> getListEnseignants(ClasseBE classe, int annee)
        {
            return classeDA.listeEnseignants(classe, annee);
        }

        // retourne la liste des Noms des Enseignants d'une Classe
        public List<string> getListNomEnseignant(List<EnseignantBE> listEnseignant)
        {
            List<string> listeNomEnseignant = new List<string>();

            listeNomEnseignant = new List<string>();
            if (listEnseignant != null)
            {
                for (int i = 0; i < listEnseignant.Count; i++)
                {
                    listeNomEnseignant.Add(listEnseignant.ElementAt(i).codeProf + " - " +listEnseignant.ElementAt(i).nomProf);
                }
                return listeNomEnseignant;
            }
            else return null;
        }

        // recherche si un enseignant a été programmer dans une classe pour une année scolaire
        public bool estProgramme(int idProf, string codeClasse, int annee)
        {
            List<ProgrammerBE> LProgrammer = programmerDA.listerSuivantCritere("IDPROF = '" + idProf + "' and codeclasse ='" + codeClasse + "' and annee = '" + annee + "'");
            if (LProgrammer != null)
            {
                if (LProgrammer.Count != 0)
                    return true;
                else return false;
            }
            else return false;
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

        // retourne la liste des codes de Classe deja enregistré (pour le filtre)
        public List<string> getListCodeClasse(List<ClasseBE> listClasse)
        {
            List<string> listeCodeClasse = new List<string>();

            listeCodeClasse = new List<string>();
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
