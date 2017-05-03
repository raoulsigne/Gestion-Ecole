using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessLogic;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    class GestionAffectationMatiereBL
    {
        ClasseDA classeDA;
        MatiereDA matiereDA;
        EnseignantDA enseignantDA;
        ProgrammerDA programmerDA;
        GroupeMatiereDA groupeMatiereDA;
        JournalDA journalDA;
        ParametresDA parametreDA;

        public GestionAffectationMatiereBL()
        {
            classeDA = new ClasseDA();
            matiereDA = new MatiereDA();
            enseignantDA = new EnseignantDA();
            programmerDA = new ProgrammerDA();
            groupeMatiereDA = new GroupeMatiereDA();
            journalDA = new JournalDA();
            parametreDA = new ParametresDA();
        }

        internal List<string> listerValeurColonneClasse(string p)
        {
            return classeDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneMatiere(string p)
        {
            return matiereDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneEnseignant(string p)
        {
            return enseignantDA.listerValeursColonne(p);
        }

        internal List<BusinessEntity.ProgrammerBE> listerToutProgrammer()
        {
            return programmerDA.listerTous();
        }

        internal List<BusinessEntity.ProgrammerBE> listerSuivantCritereProgrammer(string p)
        {
            return programmerDA.listerSuivantCritere(p);
        }

        internal bool enregistrerProgrammer(BusinessEntity.ProgrammerBE p)
        {
            if(programmerDA.modifier(p))
            {
                journalDA.journaliser("Enregistrement d'un programme - " + p.ToString());
                return true;
            }
            else
                return false;
        }

        internal void supprimerProgrammer(BusinessEntity.ProgrammerBE p)
        {
            if(programmerDA.supprimer(p))
            {
                journalDA.journaliser("Suppression d'un programme - " +p.ToString());
            }
        }

        internal List<string> listerValeurColonneGroupe(string p)
        {
            return groupeMatiereDA.listerValeursColonne(p);
        }

        internal BusinessEntity.ProgrammerBE rechercherProgrammer(BusinessEntity.ProgrammerBE program)
        {
            return programmerDA.rechercher(program);
        }

        internal bool modifierProgrammer(BusinessEntity.ProgrammerBE program, BusinessEntity.ProgrammerBE p)
        {
            if(programmerDA.modifier(program,p))
            {
                journalDA.journaliser("Modification d'un programme - " + program.ToString()+ " - " + p.ToString());
                return true;
            }
            else
                return false;
        }

        internal List<BusinessEntity.EnseignantBE> listerToutEnseignants()
        {
            return enseignantDA.listerTous();
        }

        internal int anneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }
    }
}
