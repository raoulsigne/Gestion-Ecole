using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    class GestionPaysBL
    {
        PaysDA paysDA;
        JournalDA journalDA;

        public GestionPaysBL()
        {
            paysDA = new PaysDA();
            journalDA = new JournalDA();
        }

        internal List<BusinessEntity.PaysBE> listerToutPays()
        {
            return paysDA.listerTous();
        }

        internal bool enregistrerPays(BusinessEntity.PaysBE p)
        {
            if (paysDA.ajouter(p))
            {
                journalDA.journaliser("Enregistrement d'un pays - " + p.codePays + " - " + p.nomPays);
                return true;
            }
            else
                return false;
        }

        internal void supprimerPays(BusinessEntity.PaysBE p)
        {
            if (paysDA.supprimer(p))
            {
                journalDA.journaliser("Suppression d'un pays - " + p.codePays + " - " + p.nomPays);
            }
        }

        internal bool modifierPays(PaysBE objet_pays, PaysBE p)
        {
            if (paysDA.modifier(objet_pays, p))
            {
                journalDA.journaliser("Modification d'un pays - " + objet_pays.nomPays + " - " + p.nomPays);
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
