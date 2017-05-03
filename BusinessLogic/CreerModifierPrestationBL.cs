using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierPrestationBL
    {
        private PrestationDA prestationDA;
        private JournalDA journalDA;

        public CreerModifierPrestationBL()
        {
            this.prestationDA = new PrestationDA();
            this.journalDA = new JournalDA();
        }

        //creer une Prestation
        public bool creerPrestation(string codeP, string nomP, int priorite)
        {
            PrestationBE prestation = new PrestationBE(codeP, nomP, priorite);
            if (prestationDA.ajouter(prestation))
            {
                journalDA.journaliser("enregistrement d'une prestation de code " + codeP + " et de nom " + nomP);
                return true;
            }
            return false;
        }

        // supprimer une Prestation
        public bool supprinerPrestation(PrestationBE Pre)
        {
            if (prestationDA.supprimer(Pre))
            {
                journalDA.journaliser("suppression de la prestation de code " + Pre.codePrestation + " et de nom " + Pre.nomPrestation);
                return true;
            }
            return false;
        }

        // modifier une Prestation
        public bool modifierPrestation(PrestationBE Pre, PrestationBE newPre)
        {
            if (prestationDA.modifier(Pre, newPre))
            {
                journalDA.journaliser("modification de la prestation de code " + Pre.codePrestation + ". ancien nom : " + Pre.nomPrestation + ". nouveau code : " + newPre.codePrestation + ", nouveau nom : " + newPre.nomPrestation);
                return true;
            }
            return false;
        }

        // modifier une Prestation
        public bool modifierPrestation(PrestationBE p)
        {
            if (prestationDA.modifier(p))
            {
                journalDA.journaliser("modification de la prestation de code " + p.codePrestation + ". ancien nom : " + p.nomPrestation + ", nouveau nom : " + p.nomPrestation);
                return true;
            }
            return false;
        }

        // rechercher une Prestation
        public PrestationBE rechercherPrestation(PrestationBE p)
        {
            return prestationDA.rechercher(p);
        }

        //lister toutes les Prestations
        public List<PrestationBE> listerToutesLesPrestation()
        {
            return prestationDA.listerTous();
        }

        // lister tous les Prestation respectant un certain critère
        public List<PrestationBE> listerPrestationSuivantCritere(string critere)
        {
            return prestationDA.listerSuivantCritere(critere);
        }
    }
}
