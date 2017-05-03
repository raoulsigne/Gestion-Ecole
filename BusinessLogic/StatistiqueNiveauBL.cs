using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class StatistiqueNiveauBL
    {
        private ClasseDA classeDA;
        private SequenceDA sequenceDA;
        private TrimestreDA trimestreDA;
        private ParametresDA parametresDA;
        private StatistiqueNiveauDA statistiqueNiveauDA;
        private NiveauDA niveauDA;

        public StatistiqueNiveauBL()
        {
            this.classeDA = new ClasseDA();
            this.sequenceDA = new SequenceDA();
            this.trimestreDA = new TrimestreDA();
            this.parametresDA = new ParametresDA();
            this.statistiqueNiveauDA = new StatistiqueNiveauDA();
            this.niveauDA = new NiveauDA();
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

        //lister tous les Niveaux
        public List<NiveauBE> listerTousLesNiveaux()
        {
            return niveauDA.listerTous();
        }

        // lister tous les niveaux respectant un certain critère
        public List<NiveauBE> listerNiveauxSuivantCritere(string critere)
        {
            return niveauDA.listerSuivantCritere(critere);
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

        // retourne la liste des codes de Niveau deja enregistré (pour le filtre)
        public List<string> getListCodeNiveau(List<NiveauBE> listNiveau)
        {
            List<string> listeCodeNiveau = new List<string>();

            listeCodeNiveau = new List<string>();
            listeCodeNiveau.Add("<Tous Les Niveaux>");
            if (listNiveau != null)
            {
                for (int i = 0; i < listNiveau.Count; i++)
                {
                    listeCodeNiveau.Add(listNiveau.ElementAt(i).codeNiveau);
                }
                //listeCodeCycle.Add("Tous");
                return listeCodeNiveau;
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

        //-------- méthodes qui recherche les statistiques d'une séquence d'un niveau
        public StatistiqueNiveauBE getStatistiqueDuneSequence(String codeNiveau, int annee, String codeSequence) {
            return statistiqueNiveauDA.getStatistiqueDuneSequence(codeNiveau, annee, codeSequence);
        }

        //-------- méthodes qui recherche les statistiques d'un trimestre d'un niveau
        public StatistiqueNiveauBE getStatistiqueDunTrimestre(String codeNiveau, int annee, String codeTrimestre)
        {
            return statistiqueNiveauDA.getStatistiqueDunTrimestre(codeNiveau, annee, codeTrimestre);
        }

        //-------- méthodes qui recherche les statistiques d'une année d'un Niveau
        public StatistiqueNiveauBE getStatistiqueDuneAnnee(String codeNiveau, int annee) {
            return statistiqueNiveauDA.getStatistiqueDuneAnnee(codeNiveau, annee);
        }
    }
}
