using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    public class SaisieNotesSansAnonymatBL
    {
        ClasseDA classeDA;
        TypeevaluationDA evaluationDA;
        MatiereDA matiereDA;
        SequenceDA sequenceDA;
        EleveDA eleveDA;
        NotesDA notesDA;
        ProgrammerDA programmerDA;
        InscrireDA inscrireDA;
        ParametresDA parametreDA;
        EvaluerDA evaluerDA;
        JournalDA journalDA;
        TrimestreDA trimestreDA;

        public SaisieNotesSansAnonymatBL()
        {
            classeDA = new ClasseDA();
            evaluationDA = new TypeevaluationDA();
            matiereDA = new MatiereDA();
            sequenceDA = new SequenceDA();
            eleveDA = new EleveDA();
            notesDA = new NotesDA();
            programmerDA = new ProgrammerDA();
            inscrireDA = new InscrireDA();
            evaluerDA = new EvaluerDA();
            parametreDA = new ParametresDA();
            journalDA = new JournalDA();
            trimestreDA = new TrimestreDA();
        }

        internal List<string> listerValeurColonneClasse(string p)
        {
            return classeDA.listerValeursColonne(p);
        }

        internal List<ProgrammerBE> listerSuivantCritereProgramer(string p)
        {
            return programmerDA.listerSuivantCritere(p);
        }

        internal List<string> listerValeurColonneSequence(string p)
        {
            return sequenceDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneTypeEvaluation(string p)
        {
            return evaluationDA.listerValeursColonne(p);
        }

        internal List<NotesBE> listerSuivantCritereNotes(string p)
        {
            return notesDA.listerSuivantCritere(p);
        }

        internal EleveBE rechercherEleve(EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
        }

        internal void enregistrerAnonymat(NotesBE note)
        {
            if (notesDA.modifier(note))
            {
                journalDA.journaliser("Enregistrement d'une note - " + note.note);
            }
        }

        internal List<InscrireBE> listerSuivantCritereInscrire(string p)
        {
            return inscrireDA.listerSuivantCritere(p);
        }

        internal int anneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        internal NotesBE rechercherNotes(NotesBE note)
        {
            return notesDA.rechercher(note);
        }

        internal List<string> listerEvaluation(string classe, string matiere, string sequence, int annee)
        {
            return evaluerDA.obtenirListerEvaluation(classe, matiere, sequence, annee);
        }

        internal ClasseBE rechercherClasse(ClasseBE classe)
        {
            return classeDA.rechercher(classe);
        }

        internal MatiereBE rechercherMatiere(MatiereBE matiere)
        {
            return matiereDA.rechercher(matiere);
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

        // lister les matières programmer dans une classe
        public List<MatiereBE> ListeMatiereDuneClasse(ClasseBE classe, int annee)
        {
            return classeDA.ListeMatiereDuneClasse(classe, annee);
        }

        //lister tous les Trimestres
        public List<TrimestreBE> listerTousLesTrimestres()
        {
            return trimestreDA.listerTous();
        }

        // lister toutes les Sequences respectant un certain critère
        public List<SequenceBE> listerSequenceSuivantCritere(string critere)
        {
            return sequenceDA.listerSuivantCritere(critere);
        }

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }
    }
}
