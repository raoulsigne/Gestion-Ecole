using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    public class GestionSanctionClasseBL
    {
        ClasseDA classeDA;
        SequenceDA sequenceDA;
        DisciplineDA disciplineDA;
        ParametresDA parametreDA;
        InscrireDA inscrireDA;
        EleveDA eleveDA;
        SanctionnerDA sanctionnerDA;
        JournalDA journalDA;

        public GestionSanctionClasseBL()
        {
            classeDA = new ClasseDA();
            sequenceDA = new SequenceDA();
            disciplineDA = new DisciplineDA();
            parametreDA = new ParametresDA();
            inscrireDA = new InscrireDA();
            eleveDA = new EleveDA();
            sanctionnerDA = new SanctionnerDA();
            journalDA = new JournalDA();
        }

        internal List<string> listerValeurColonneClasse(string p)
        {
            return classeDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneSequence(string p)
        {
            return sequenceDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneDiscipline(string p)
        {
            return disciplineDA.listerValeursColonne(p);
        }

        internal DisciplineBE rechercherDiscipline(DisciplineBE discipline)
        {
            return disciplineDA.rechercher(discipline);
        }

        internal int anneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        internal List<InscrireBE> listerSuivantCritereInscrire(string p)
        {
            return inscrireDA.listerSuivantCritere(p);
        }

        internal EleveBE rechercherEleve(EleveBE e)
        {
            return eleveDA.rechercher(e);
        }

        internal List<SanctionnerBE> listerSuivantCritereSanctionner(string p)
        {
            return sanctionnerDA.listerSuivantCritere(p);
        }

        internal List<SanctionnerBE> listerSanctionnerClasse(string classe, string sanction, int annee, string sequence, string date)
        {
            return sanctionnerDA.elevesSanctionner_PourUneSanction_DansUneClasse(classe, sanction, annee, sequence, date);
        }

        internal void enregistrerSanctionner(SanctionnerBE sanctionner)
        {
            if (sanctionnerDA.ajouter(sanctionner))
            {
                journalDA.journaliser("Enregistrement d'une sanction - " + sanctionner.codesanction + " - " + sanctionner.matricule);
            }
        }

        internal ClasseBE rechercherClasse(ClasseBE classe)
        {
            return classeDA.rechercher(classe);
        }

        internal SequenceBE rechercherSequence(SequenceBE sequence)
        {
            return sequenceDA.rechercher(sequence);
        }

        internal List<SanctionnerBE> listerSanctionnerClasse(string classe, string sanction, int annee, string sequence)
        {
            return sanctionnerDA.elevesSanctionner_PourUneSanction_DansUneClasse(classe, sanction, annee, sequence);
        }

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }

        internal List<DisciplineBE> listerToutDiscipline()
        {
            return disciplineDA.listerTous();
        }
    }
}
