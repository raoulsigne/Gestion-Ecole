using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class DefinirEvaluationMatiereBL
    {
      
        private EvaluerDA evaluerDA;
        private MatiereDA matiereDA;
        private ClasseDA classeDA;
        private TypeevaluationDA typeevaluationDA;
        private ParametresDA parametreDA;
        private SequenceDA sequenceDA;
        private JournalDA journalDA;

        public DefinirEvaluationMatiereBL()
        {
            this.evaluerDA = new EvaluerDA();
            this.matiereDA = new MatiereDA();
            this.classeDA = new ClasseDA();
            this.typeevaluationDA = new TypeevaluationDA();
            this.parametreDA = new ParametresDA();
            this.sequenceDA = new SequenceDA();
            this.journalDA = new JournalDA();
        }

        //creer une Evaluer
        public bool creerEvaluer(string codeEvaluation, string codemat, string codeclasse, int poids, int annee, String codeSequence)
        {
            EvaluerBE evaluer = new EvaluerBE(codeEvaluation, codemat, codeclasse, poids, annee, codeSequence);
            if (evaluerDA.ajouter(evaluer))
            {
                journalDA.journaliser("enregistrement d'une évaluation de code " + codeEvaluation + " pour la matière " + codemat + " de la classe " + codeclasse + ", avec un poids de "+ poids +" pour la séquence "+codeSequence + " de l'année "+annee);
                return true;
            }
            return false;
        }

        // supprimer une Evaluer
        public bool supprinerEvaluer(EvaluerBE evaluer)
        {
            if (evaluerDA.supprimer(evaluer))
            {
                journalDA.journaliser("suppression de l'évaluation de code " + evaluer.codeEvaluation + " pour la matière " + evaluer.codeMat + " de la classe " + evaluer.codeClasse + " de l'année " + evaluer.annee);
                return true;
            }
            return false;
        }

        // supprimer une Evaluer
        public bool supprinerEvaluerSuivantCritere(String critere)
        {
            if (evaluerDA.supprimerSuivantCritere(critere))
            {
                journalDA.journaliser("suppression de l'évaluation respectant le critère " + critere);
                return true;
            }
            return false;
        }

        // modifier une Evaluer
        public bool modifierEvaluer(EvaluerBE evaluer, EvaluerBE newEvaluer)
        {
            if (evaluerDA.modifier(evaluer, newEvaluer))
            {
                journalDA.journaliser("modification de l'évaluation de code " + evaluer.codeEvaluation + " pour la matière " + evaluer.codeMat + " de la classe " + evaluer.codeClasse + ", pour la séquence " + evaluer.codeSequence + " de l'année " + evaluer.annee + ". nouveau poids : " + newEvaluer.poids);
                return true;
            }
            return false;
        }

        // rechercher une Evaluer
        public EvaluerBE rechercherEvaluer(EvaluerBE evaluer)
        {
            return evaluerDA.rechercher(evaluer);
        }

        // rechercher une classe
        public ClasseBE rechercherClasse(ClasseBE classe)
        {
            return classeDA.rechercher(classe);
        }

        //lister toutes les Evaluer
        public List<EvaluerBE> listerTousLesEvaluers()
        {
            return evaluerDA.listerTous();
        }

        // lister tous les Evaluer respectant un certain critère
        public List<EvaluerBE> listerEvaluersSuivantCritere(string critere)
        {
            return evaluerDA.listerSuivantCritere(critere);
        }

        //lister toutes les Classes
        public List<ClasseBE> listerToutesLesClasses()
        {
            return classeDA.listerTous();
        }

        //lister toutes les Classe Ordonnées
        public List<ClasseBE> listerTousLesClasseOrderByNiveau()
        {
            return classeDA.listerTousOrderByNiveau();
        }

        //lister toutes les Sequences
        public List<SequenceBE> listerToutesLesSequences()
        {
            return sequenceDA.listerTous();
        }

        //lister tous les Type d'évaluation
        public List<TypeevaluationBE> listerTousLesTypesEvaluations()
        {
            return typeevaluationDA.listerTous();
        }

        // retourne la liste des Codes de Sequence deja enregistrés
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

        // retourne la liste des Codes de Sequence deja enregistrés
        public List<string> getListCodeSequence2(List<SequenceBE> listSequence)
        {
            List<string> listeCodeSequence = new List<string>();

            listeCodeSequence = new List<string>();
            listeCodeSequence.Add("<Toutes les Séquences>");
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

        // retourne la liste des Codes de Classe deja enregistrés
        public List<string> getListCodeClasse(List<ClasseBE> listClasse)
        {
            List<string> listeCodeClasse = new List<string>();

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

        // retourne la liste des Codes de Classe deja enregistrés
        public List<string> getListCodeClasse2(List<ClasseBE> listClasse)
        {
            List<string> listeCodeClasse = new List<string>();

            listeCodeClasse.Add("<Toutes les classes>");
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

        // retourne la liste des Codes de matières deja enregistrés
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

        // retourne la liste des Codes de matières deja enregistrés
        public List<string> getListCodeMatiere2(List<MatiereBE> listMatiere)
        {
            List<string> listeCodeMatiere = new List<string>();

            listeCodeMatiere = new List<string>();
            listeCodeMatiere.Add("<Toutes les matières>");
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

        // retourne la liste des Codes des Types d'évaluation deja enregistrés
        public List<string> getListCodeTypeEvaluation(List<TypeevaluationBE> listTypeEvaluation)
        {
            List<string> listeCodeTypeEvaluation = new List<string>();

            listeCodeTypeEvaluation = new List<string>();
            if (listTypeEvaluation != null)
            {
                for (int i = 0; i < listTypeEvaluation.Count; i++)
                {
                    listeCodeTypeEvaluation.Add(listTypeEvaluation.ElementAt(i).codeevaluation);
                }
                return listeCodeTypeEvaluation;
            }
            else return null;
        }


        // lister les matières programmer dans une classe
        public List<MatiereBE> ListeMatiereDuneClasse(ClasseBE classe, int annee) {
            return classeDA.ListeMatiereDuneClasse(classe, annee);
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

        //lister tous les Type d'évaluation
        public List<string[]> obtenirListeEvaluation(string classe, int annee, string sequence)
        {
            return evaluerDA.obtenirListerEvaluation(classe, annee, sequence);
        }
    }
}
