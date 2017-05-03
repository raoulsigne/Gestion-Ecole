using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.ClasseConception;
using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    
    class GestionRecapitulatifSequentielBL
    {
        ClasseDA classeDA;
        EleveDA eleveDA;
        MoyennesDA moyennesDA;
        SequenceDA sequenceDA;
        ParametresDA parametreDA;
        EnseignantDA enseignantDA;
        ProgrammerDA programmerDA;
        JournalDA journalDA;
        ResultatDA resultatDA;

        public GestionRecapitulatifSequentielBL()
        {
            classeDA = new ClasseDA();
            eleveDA = new EleveDA();
            moyennesDA = new MoyennesDA();
            sequenceDA = new SequenceDA();
            parametreDA = new ParametresDA();
            enseignantDA = new EnseignantDA();
            programmerDA = new ProgrammerDA();
            journalDA = new JournalDA();
            resultatDA = new ResultatDA();
        }

        public LigneRecapitulatif recapitulatifSequentielEleve(EleveBE eleve, string codeclasse, string codesequence, int annee)
        {
            journalDA.journaliser("Saisie du recapitulatif sequentiel de la " + codesequence+ " de " + eleve.matricule);
            return moyennesDA.recapitulatifSequentielEleve(eleve, codeclasse, codesequence, annee);
        }

        public LigneRecapSeq recapitulatifSequentielEleve_new(EleveBE eleve, string codeclasse, string codesequence, int annee)
        {
            journalDA.journaliser("Saisie du recapitulatif sequentiel de la " + codesequence + " de " + eleve.matricule);
            return moyennesDA.recapitulatifSequentielEleve_new(eleve, codeclasse, codesequence, annee);
        }

        public Synthese obtenirSyntheseSequentielle(string codeclasse, string codesequence, int annee)
        {
            return moyennesDA.syntheseSequentielleClasse(codeclasse, codesequence, annee);
        }


        public int anneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        public List<EleveBE> listeEleveDuneClasse(ClasseBE classe, int annee)
        {
            return classeDA.listeEleves(classe,annee);
        }

        public ClasseBE rechercherClasse(ClasseBE classe)
        {
            return classeDA.rechercher(classe);
        }

        public SequenceBE rechercherSequence(SequenceBE sequence)
        {
            return sequenceDA.rechercher(sequence);
        }
    
        public List<string> listerValeurColonneClasse(string p)
        {
 	        return classeDA.listerValeursColonne(p);
        }

        public List<string> listerValeurColonneSequence(string p)
        {
 	        return sequenceDA.listerValeursColonne(p);
        }

        public string obtenirProfTitulaire(string p, int annee)
        {
            string codeprof = classeDA.getCodeProfTitulaireDuneClasse(p, annee);
            EnseignantBE enseignant = new EnseignantBE();
            enseignant.codeProf = codeprof;
            enseignant = enseignantDA.rechercher(enseignant);
            if (enseignant != null)
                return enseignant.nomProf;
            else
                return "";
        }

        public List<string> listeCodeMatiereDuneClasse(string codeclasse, int annee)
        {
            return programmerDA.listeCodeMatiereDuneClasse(codeclasse, annee);
        }

        public List<string> listeCodeGroupeDuneClasse(string codeclasse, int annee)
        {
            return programmerDA.listeCodeGroupeDuneClasse(codeclasse, annee);
        }

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }

        internal double obtenirMoyenneClasse(string p, string codeseq, int annee)
        {
            return resultatDA.obtenirMoyenneSequentielleDuneClasse(p, codeseq, annee);
        }
    }

}
