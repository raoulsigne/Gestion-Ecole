using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    public class SaisieAnonymatBL
    {
        ClasseDA classeDA;
        TypeevaluationDA evaluationDA;
        MatiereDA matiereDA;
        SequenceDA sequenceDA;
        EleveDA eleveDA;
        NotesDA notesDA;
        ProgrammerDA programmerDA;
        InscrireDA inscrireDA;
        EvaluerDA evaluerDA;
        JournalDA journalDA;
        ParametresDA parametreDA;

        public SaisieAnonymatBL()
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
            journalDA = new JournalDA();
            parametreDA = new ParametresDA();
        }

        internal List<string> listerValeurColonneClasse(string p)
        {
            return classeDA.listerValeursColonne(p);
        }

        internal List<ProgrammerBE> listerSuivantCritereProgrammer(string p)
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
                journalDA.journaliser("Enregistrement d'un anonymat - "+note.anonymat);
            }
        }

        internal List<InscrireBE> listerSuivantCritereInscrire(string p)
        {
            return inscrireDA.listerSuivantCritere(p);
        }

        internal NotesBE rechercherEleveDansNote(NotesBE note)
        {
            return notesDA.rechercherEleve(note);
        }

        internal InscrireBE rechercherInscrire(InscrireBE inscrire)
        {
            return inscrireDA.rechercher(inscrire);
        }

        internal List<string> listerEvaluation(string classe, string matiere, string sequence, int annee)
        {
            return evaluerDA.obtenirListerEvaluation(classe, matiere, sequence, annee);
        }

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }

        internal int anneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        internal NotesBE rechercherNote(NotesBE note)
        {
            return notesDA.rechercher(note);
        }
    }
}
