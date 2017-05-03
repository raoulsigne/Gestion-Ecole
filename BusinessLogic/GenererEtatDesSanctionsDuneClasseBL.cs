using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Ecole.DataAccess;
using Ecole.BusinessEntity;
using Ecole.Utilitaire;

namespace Ecole.BusinessLogic
{
    class GenererEtatDesSanctionsDuneClasseBL
    {
        private ClasseDA classeDA;
        private SequenceDA sequenceDA;
        private ParametresDA parametresDA;
        private EleveDA eleveDA;
        private TrimestreDA trimestreDA;
        private JournalDA journalDA;

        public GenererEtatDesSanctionsDuneClasseBL()
        {
            this.classeDA = new ClasseDA();
            this.sequenceDA = new SequenceDA();
            this.parametresDA = new ParametresDA();
            this.eleveDA = new EleveDA();
            this.trimestreDA = new TrimestreDA();
            this.journalDA = new JournalDA();
        }

       
        // rechercher une Classe
        public ClasseBE rechercherClasse(ClasseBE classe)
        {
            return classeDA.rechercher(classe);
        }

        // rechercher une Sequence
        public SequenceBE rechercherSequence(SequenceBE sequence)
        {
            return sequenceDA.rechercher(sequence);
        }

        // rechercher un élève
        public EleveBE rechercherEleve(EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
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

        //lister toutes les Sequences
        public List<SequenceBE> listerToutesLesSequences()
        {
            return sequenceDA.listerTous();
        }

        // lister toutes les Sequences respectant un certain critère
        public List<SequenceBE> listerSequencesSuivantCritere(string critere)
        {
            return sequenceDA.listerSuivantCritere(critere);
        }

        //lister tous les Trimestres
        public List<TrimestreBE> listerTousLesTrimestres()
        {
            return trimestreDA.listerTous();
        }

        // lister tous les Trimestres respectant un certain critère
        public List<TrimestreBE> listerTrimestresSuivantCritere(string critere)
        {
            return trimestreDA.listerSuivantCritere(critere);
        }

        // retourne l'année du système
        public int getAnneeEnCours()
        {
            return parametresDA.AnneeEnCours();
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
                //listeCodeCycle.Add("Tous");
                return listeCodeClasse;
            }
            else return null;
        }

        // retourne la liste des codes de Classe deja enregistré (pour le filtre)
        public List<string> getListCodeClasse2(List<ClasseBE> listClasse)
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
                //listeCodeCycle.Add("Tous");
                return listeCodeClasse;
            }
            else return null;
        }

        // retourne la liste des codes de Matière deja enregistré (pour le filtre)
        public List<string> getListCodeMatiere(List<MatiereBE> listMatiere)
        {
            List<string> listeCodeMatiere = new List<string>();

            listeCodeMatiere = new List<string>();
            if (listMatiere != null)
            {
                for (int i = 0; i < listMatiere.Count; i++)
                {
                    listeCodeMatiere.Add(listMatiere.ElementAt(i).codeMat);
                }
                return listeCodeMatiere;
            }
            else return null;
        }

        // retourne la liste des codes de Séquence deja enregistré (pour le filtre)
        public List<string> getListCodeSequence(List<SequenceBE> listSequence)
        {
            List<string> listeCodeSequence = new List<string>();

            listeCodeSequence = new List<string>();
            if (listSequence != null)
            {
                for (int i = 0; i < listSequence.Count; i++)
                {
                    listeCodeSequence.Add(listSequence.ElementAt(i).codeseq);
                }
                return listeCodeSequence;
            }
            else return null;
        }

        // retourne la liste des codes de Séquence deja enregistré (pour le filtre)
        public List<string> getListCodeSequence2(List<SequenceBE> listSequence)
        {
            List<string> listeCodeSequence = new List<string>();

            listeCodeSequence = new List<string>();
            listeCodeSequence.Add("<Toutes Les Séquences>");
            if (listSequence != null)
            {
                for (int i = 0; i < listSequence.Count; i++)
                {
                    listeCodeSequence.Add(listSequence.ElementAt(i).codeseq);
                }
                return listeCodeSequence;
            }
            else return null;
        }

        // retourne la liste des codes de Trimestre deja enregistré (pour le filtre)
        public List<string> getListCodeTrimestre(List<TrimestreBE> listTrimestre)
        {
            List<string> listeCodeTrimestre = new List<string>();

            listeCodeTrimestre = new List<string>();
            if (listTrimestre != null)
            {
                for (int i = 0; i < listTrimestre.Count; i++)
                {
                    listeCodeTrimestre.Add(listTrimestre.ElementAt(i).codetrimestre);
                }
                return listeCodeTrimestre;
            }
            else return null;
        }

        // retourne la liste des codes de Trimestre deja enregistré (pour le filtre)
        public List<string> getListCodeTrimestre2(List<TrimestreBE> listTrimestre)
        {
            List<string> listeCodeTrimestre = new List<string>();

            listeCodeTrimestre = new List<string>();
            listeCodeTrimestre.Add("<Tous Les Trimestres>");
            if (listTrimestre != null)
            {
                for (int i = 0; i < listTrimestre.Count; i++)
                {
                    listeCodeTrimestre.Add(listTrimestre.ElementAt(i).codetrimestre);
                }
                return listeCodeTrimestre;
            }
            else return null;
        }

        // liste les matières d'une classe
        public List<MatiereBE> listeDesMatieresDuneClasse(ClasseBE classe, int annee)
        {
            return classeDA.ListeMatiereDuneClasse(classe, annee);
        }

        //lister les élèves  inscrit à une classe pour une année
        public List<EleveBE> ListeDesElevesDuneClasse(ClasseBE classe, int annee)
        {
            return classeDA.listeEleves(classe, annee);
        }

        public String getClasseEleve(String matricule, int annee)
        {
            return classeDA.getClasseEleve(matricule, annee);
        }

        public List<MatiereBE> ListeMatiereDuneClasse(ClasseBE classe, int annee)
        {
            return classeDA.ListeMatiereDuneClasse(classe, annee);
        }

        public void genererEtatSanctionSequentielleDuneClasse(String codeClasse, String codeSequence, int annee) { 
            //On liste les de la classe
            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;
            List<EleveBE> LEleve = ListeDesElevesDuneClasse(classe, annee);

            SequenceBE sequence = new SequenceBE();
            sequence.codeseq = codeSequence;
            sequence = sequenceDA.rechercher(sequence);

            

            if (LEleve != null && LEleve.Count != 0) {
                if (sequence != null)
                {
                    

                    CreerEtat etat = new CreerEtat("Etat des sanctions - " + codeClasse + " - Séquence" + codeSequence + " - " + annee, "état des Sanctions " + sequence.nomseq);
                    etat.etatDesSanctionsSequentiellesDuneClasse(LEleve, codeClasse, codeSequence, annee);
                }
            }

            journalDA.journaliser("génération de l'état des sanctions de la séquence " + codeSequence + " de l'année " + annee + " pour la classe " + codeClasse);

        }

        public void genererEtatSanctionTrimestrielleDuneClasse(String codeClasse, String codeTrimestre, int annee)
        {
            //On liste les de la classe
            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;
            List<EleveBE> LEleve = ListeDesElevesDuneClasse(classe, annee);

            TrimestreBE trimestre = new TrimestreBE();
            trimestre.codetrimestre = codeTrimestre;
            trimestre = trimestreDA.rechercher(trimestre);

            if (LEleve != null && LEleve.Count != 0)
            {
                if (trimestre != null)
                {
                    CreerEtat etat = new CreerEtat("Etat des sanctions - " + codeClasse + " - trimestre" + codeTrimestre + " - " + annee, "état des Sanctions " + trimestre.nomtrimestre);
                    etat.etatDesSanctionsTrimestriellesDuneClasse(LEleve, codeClasse, codeTrimestre, annee);
                }
            }

            journalDA.journaliser("génération de l'état des sanctions du trimestyre " + codeTrimestre + " de l'année " + annee + " pour la classe " + codeClasse);

        }


        public void genererEtatSanctionAnnuelleDuneClasse(String codeClasse, int annee)
        {
            //On liste les de la classe
            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;
            List<EleveBE> LEleve = ListeDesElevesDuneClasse(classe, annee);

            if (LEleve != null && LEleve.Count != 0)
            {
                CreerEtat etat = new CreerEtat("Etat des sanctions - " + codeClasse + " - Année - " + annee, "état des Sanctions Année" + annee);
                etat.etatDesSanctionsAnnuellesDuneClasse(LEleve, codeClasse, annee);
            }

            journalDA.journaliser("génération de l'état des sanctions de l'année " + annee + " pour la classe " + codeClasse);

        }

    }
}
