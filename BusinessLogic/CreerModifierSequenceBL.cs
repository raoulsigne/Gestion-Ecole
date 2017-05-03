using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierSequenceBL
    {
        private SequenceDA sequenceDA;
        private TrimestreDA trimestreDA;
        private JournalDA journalDA;

        public CreerModifierSequenceBL()
        {
            this.sequenceDA = new SequenceDA();
            this.trimestreDA = new TrimestreDA();
            this.journalDA = new JournalDA();
        }

        //creer une Sequence
        public bool creerSequence(string codeSequence, string nomSequence, String codeTrimestre)
        {
            SequenceBE sequence = new SequenceBE(codeSequence, nomSequence, codeTrimestre);
            if (sequenceDA.ajouter(sequence))
            {
                journalDA.journaliser("enregistrement d'une séquence de code " + codeSequence + " et de nom " + nomSequence + "pour le trimestre " + codeTrimestre);
                return true;
            }
            return false;
        }

        // supprimer une Sequence
        public bool supprinerSequence(SequenceBE sequence)
        {
            if (sequenceDA.supprimer(sequence))
            {
                journalDA.journaliser("suppression de la séquence de code " + sequence.codeseq + " et de nom " + sequence.nomseq + "pour le trimestre " + sequence.codetrimestre);
                return true;
            }
            return false;
        }

        // modifier une Sequence
        public bool modifierSequence(SequenceBE sequence, SequenceBE newSequence)
        {
            if (sequenceDA.modifier(sequence, newSequence))
            {
                journalDA.journaliser("modification de la séquence de code " + sequence.codeseq + ". nouveau code : " + newSequence.codeseq + ", nouveau nom : " + newSequence.nomseq + ", nouveau trimestre : " + newSequence.codetrimestre);
                return true;
            }
            return false;
        }

        // modifier une Sequence
        public bool modifierSequence(SequenceBE sequence)
        {
            if (sequenceDA.modifier(sequence))
            {
                journalDA.journaliser("modification de la séquence de code " + sequence.codeseq + ". nouveau code : " + sequence.codeseq + ", nouveau nom : " + sequence.nomseq + ", nouveau trimestre : " + sequence.codetrimestre);
                return true;
            }
            return false;
        }

        // rechercher une Sequence
        public SequenceBE rechercherSequence(SequenceBE sequence)
        {
            return sequenceDA.rechercher(sequence);
        }

        //lister toutes les Sequence
        public List<SequenceBE> listerToutesLesSequences()
        {
            return sequenceDA.listerTous();
        }

        // lister toutes les Sequences respectant un certain critère
        public List<SequenceBE> listerSequenceSuivantCritere(string critere)
        {
            return sequenceDA.listerSuivantCritere(critere);
        }

        //lister tous les Trimestres
        public List<TrimestreBE> listerTousLesTrimestres()
        {
            return trimestreDA.listerTous();
        }

        // retourne la liste des codes de Sequence deja enregistré (pour le filtre)
        public List<string> getListCodeSequence(List<SequenceBE> listSequence)
        {
            List<string> listeCodeSequence = new List<string>();

            listeCodeSequence = new List<string>();
            listeCodeSequence.Add("<Tous les Codes>");
            if (listSequence != null)
            {
                for (int i = 0; i < listSequence.Count; i++)
                {
                    listeCodeSequence.Add(listSequence.ElementAt(i).codeseq);
                }
                //listeCodeSequence.Add("Tous");
                return listeCodeSequence;
            }
            else return null;
        }

        // retourne la liste des Noms de Sequence deja enregistré (pour le filtre)
        public List<string> getListNomSequence(List<SequenceBE> listSequence)
        {
            List<string> listeNomSequence = new List<string>();

            listeNomSequence = new List<string>();
            listeNomSequence.Add("<Tous les Noms>");
            if (listSequence != null)
            {
                for (int i = 0; i < listSequence.Count; i++)
                {
                    listeNomSequence.Add(listSequence.ElementAt(i).nomseq);
                }
                //listeNomSequence.Add("Tous");
                return listeNomSequence;
            }
            else return null;
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

        // retourne la liste des codes de Trimestre deja enregistré (pour le comboBox de l'enregistrement)
        public List<string> getListCodeTrimestre2(List<TrimestreBE> listTrimestre)
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
    }
}
