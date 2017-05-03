using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class GestionProgrammeBL
    {
        ClasseDA classeDA;
        ProgrammerDA programmerDA;
        MatiereDA matiereDA;
        EnseignantDA enseignantDA;
        JournalDA journalDA;
        ParametresDA parametreDA;

        public GestionProgrammeBL()
        {
            classeDA = new ClasseDA();
            programmerDA = new ProgrammerDA();
            matiereDA = new MatiereDA();
            enseignantDA = new EnseignantDA();
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

        internal MatiereBE rechercherMatiere(MatiereBE matiere)
        {
            return matiereDA.rechercher(matiere);
        }

        internal EnseignantBE rechercherEnseignant(EnseignantBE enseignant)
        {
            return enseignantDA.rechercher(enseignant);
        }

        internal ClasseBE rechercherClasse(ClasseBE classe)
        {
            return classeDA.rechercher(classe);
        }

        public void journaliserImpression(string action)
        {
            journalDA.journaliser(action);
        }

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }

        internal int anneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }
    }
}
