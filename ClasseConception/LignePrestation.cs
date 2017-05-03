using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Ecole.ClasseConception
{
    class LignePrestation
    {
        public string prestation{ get; set; }
        public string tranche{ get; set; }
        public decimal montant { get; set; }
        public string observation { get; set; }
        public decimal reste { get; set; }
        public decimal remise { get; set; }
        public string dateop { get; set; }

        public string Smontant { get; set; }

        public LignePrestation()
        {
            this.prestation = "";
            this.tranche = "";
            this.montant = 0;
            this.observation = "";
            this.reste = 0;
            this.remise = 0;
            this.dateop = "";

            this.Smontant = "";
        }

        public LignePrestation(string prestation,string tranche,decimal montant,string observation,decimal reste,decimal remise, string dateop)
        {
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");

            this.prestation = prestation;
            this.tranche = tranche;
            this.montant = montant;
            this.observation = observation;
            this.reste = reste;
            this.remise = remise;
            this.dateop = dateop;

            this.Smontant = montant.ToString("0,0", elGR);
        }
    }
}
