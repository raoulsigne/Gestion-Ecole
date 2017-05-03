using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    public class GestionSanctionEleveBL
    {
        DisciplineDA disciplineDA;
        SequenceDA sequenceDA;
        ParametresDA parametreDA;
        EleveDA eleveDA;
        SanctionnerDA sanctionnerDA;
        JournalDA journalDA;
        ClasseDA classeDA;
        InscrireDA inscrireDA;

        public GestionSanctionEleveBL()
        {
            disciplineDA = new DisciplineDA();
            sequenceDA = new SequenceDA();
            parametreDA = new ParametresDA();
            eleveDA = new EleveDA();
            sanctionnerDA = new SanctionnerDA();
            journalDA = new JournalDA();
            classeDA = new ClasseDA();
            inscrireDA = new InscrireDA();
        }

        internal List<string> listerValeurColonneDiscipline(string p)
        {
            return disciplineDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneSequence(string p)
        {
            return sequenceDA.listerValeursColonne(p);
        }

        internal int anneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        internal EleveBE rechercherEleve(EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
        }

        internal List<SanctionnerBE> listerSuivantCritereSanctionner(string p)
        {
            return sanctionnerDA.listerSuivantCritere(p);
        }

        internal bool enregistrerSanctionner(SanctionnerBE sanctionner)
        {
            if (sanctionnerDA.ajouter(sanctionner))
            {
                journalDA.journaliser("Enregistrement d'une sanction - " + sanctionner.codesanction + " - " + sanctionner.matricule);
                return true;
            }
            else
                return false;
        }

        internal bool modifierSanctionner(SanctionnerBE ancien_sanction, SanctionnerBE sanctionner)
        {
            if (sanctionnerDA.modifier(ancien_sanction,sanctionner))
            {
                journalDA.journaliser("Modification d'une sanction - " + sanctionner.codesanction + " - " + sanctionner.matricule);
                return true;
            }
            else
                return false;
        }

        internal void supprimerSanctionner(SanctionnerBE sanction)
        {
            if(sanctionnerDA.supprimer(sanction))
                journalDA.journaliser("Suppression d'une sanction - " + sanction.codesanction + " - " + sanction.matricule);
        }

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }

        internal List<DisciplineBE> listerToutDiscipline()
        {
            return disciplineDA.listerTous();
        }

        internal DisciplineBE rechercherDiscipline(DisciplineBE d)
        {
            return disciplineDA.rechercher(d);
        }

        internal List<string> listerValeurColonneClasse(string p)
        {
            return classeDA.listerValeursColonne(p);
        }

        internal InscrireBE rechercherInscrire(InscrireBE inscrire)
        {
            return inscrireDA.rechercherClasse(inscrire);
        }

        internal List<EleveBE> listerElevesDuneClasse(string codeclasse, int annee)
        {
            ClasseBE c = new ClasseBE();
            c.codeClasse = codeclasse;
            c = classeDA.rechercher(c);
            return classeDA.listeEleves(c, annee);
        }
    }
}
