using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    public class GestionDisciplineBL
    {
        DisciplineDA disciplineDA;
        JournalDA journalDA;

        public GestionDisciplineBL()
        {
            disciplineDA = new DisciplineDA();
            journalDA = new JournalDA();
        }

        internal List<DisciplineBE> listerToutDiscipline()
        {
            return disciplineDA.listerTous();
        }

        internal bool enregistrerDiscpline(DisciplineBE discipline)
        {
            if (disciplineDA.ajouter(discipline))
            {
                journalDA.journaliser("Enregistrement d'une discipline - " + discipline.codeSanction +" -"+ discipline.nomSanction);
                return true;
            }
            else
                return false;
        }

        internal bool modifierDiscipline(DisciplineBE old_discipline, DisciplineBE discipline)
        {
            if( disciplineDA.modifier(old_discipline,discipline))
            {
                journalDA.journaliser("Modification  d'une discipline - " + old_discipline.nomSanction + " -" + discipline.nomSanction);
                return true;
            }
            else
                return false;
        }

        internal void supprimerDiscipline(DisciplineBE discipline)
        {
            if(disciplineDA.supprimer(discipline))
            {
                journalDA.journaliser("Suppression d'une discipline - " + discipline.codeSanction + " -" + discipline.nomSanction);
            }
        }

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }

        internal List<string> listerCodeDiscipline()
        {
            return disciplineDA.listerValeursColonne("CODESANCTION");
        }
    }
}
