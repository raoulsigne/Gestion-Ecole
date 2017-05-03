using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    class GestionDepartementBL
    {

        DepartementDA departementDA;
        JournalDA journalDA;

        public GestionDepartementBL()
        {
            departementDA = new DepartementDA();
            journalDA = new JournalDA();
        }

        internal List<BusinessEntity.DepartementBE> listerToutDepartement()
        {
            return departementDA.listerTous();
        }

        internal bool enregistrerDepartement(BusinessEntity.DepartementBE d)
        {
            if (departementDA.ajouter(d))
            {
                journalDA.journaliser("Enregistrement d'un departement - " + d.codeDept+ " - " + d.nomDept);
                return true;
            }
            else
                return false;
        }

        internal void supprimerDepartement(BusinessEntity.DepartementBE d)
        {
            if(departementDA.supprimer(d))
            {
                journalDA.journaliser("Suppression d'un departement - "+d.nomDept);
            }
        }

        internal bool modifierDepartement(BusinessEntity.DepartementBE objet_departement, BusinessEntity.DepartementBE d)
        {
            if (departementDA.modifier(objet_departement, d))
            {
                journalDA.journaliser("Modification d'un departement - " + objet_departement.nomDept + " - " + d.nomDept);
                return true;
            }
            else
                return false;
        }

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }
    }
}
