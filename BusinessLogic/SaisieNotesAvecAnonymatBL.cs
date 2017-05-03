using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    public class SaisieNotesAvecAnonymatBL
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

        public SaisieNotesAvecAnonymatBL()
        {
            classeDA = new ClasseDA();
            evaluationDA = new TypeevaluationDA();
            matiereDA = new MatiereDA();
            sequenceDA = new SequenceDA();
            eleveDA = new EleveDA();
            notesDA = new NotesDA();
            programmerDA = new ProgrammerDA();
            inscrireDA = new InscrireDA();
            parametreDA = new ParametresDA();
            evaluerDA = new EvaluerDA();
            journalDA = new JournalDA();
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
            if(notesDA.modifier(note))
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

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }
    }
}
