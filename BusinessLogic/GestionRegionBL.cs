using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class GestionRegionBL
    {
        RegionDA regionDA;
        JournalDA journalDA;

        public GestionRegionBL()
        {
            regionDA = new RegionDA();
            journalDA = new JournalDA();
        }

        internal List<BusinessEntity.RegionBE> listerTousRegion()
        {
            return regionDA.listerTous();
        }

        internal bool enregistrerRegioin(BusinessEntity.RegionBE r)
        {
            if(regionDA.ajouter(r))
            {
                journalDA.journaliser("Enregistrement d'une region - " + r.coderegion + " - " + r.nomregion);
                return true;
            }
            else
                return false;
        }

        internal void supprimerRegion(BusinessEntity.RegionBE r)
        {
            if(regionDA.supprimer(r))
                journalDA.journaliser("Suppression d'une region - " + r.coderegion + " - " + r.nomregion);
        }

        internal bool modiferRegion(BusinessEntity.RegionBE objet_region, BusinessEntity.RegionBE r)
        {
            if (regionDA.modifier(objet_region, r))
            {
                journalDA.journaliser("Modification d'une region - " + objet_region.nomregion + " - " + r.nomregion);
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
