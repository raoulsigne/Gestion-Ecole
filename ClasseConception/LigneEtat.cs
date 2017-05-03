using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Ecole.ClasseConception
{
    class LigneEtat
    {
        
        #region field and properties
        public string type { get; set; }
        public string motif { get; set; }
        public string date { get; set; }
        public decimal montant { get; set; }
        public string concerne { get; set; }
        public string classe { get; set; }
        public string Smontant { get; set; }
        public string dispense { get; set; }
        #endregion

        public LigneEtat()
        {
            type = "";
            motif = "";
            date = "";
            montant = 0;
            concerne = "";
            dispense = "";
        }

        public LigneEtat(string type, string motif, string date, decimal montant, string concerne)
        {
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");

            this.type = type;
            this.motif = motif;
            this.date = date;
            this.montant = montant;
            this.concerne = concerne;

            this.Smontant = montant.ToString("0,0", elGR);
        }

        public LigneEtat(string type, string motif, string date, decimal montant, string concerne, string classe)
        {
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");

            this.type = type;
            this.motif = motif;
            this.date = date;
            this.montant = montant;
            this.concerne = concerne;
            this.classe = classe;
            this.Smontant = montant.ToString("0,0", elGR);
        }

        public LigneEtat(string type, string motif, string date, decimal montant, string concerne, string classe, decimal dispense)
        {
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");

            this.type = type;
            this.motif = motif;
            this.date = date;
            this.montant = montant;
            this.concerne = concerne;
            this.classe = classe;
            this.Smontant = montant.ToString("0,0", elGR);
            this.dispense = dispense.ToString("0,0", elGR);
        }
    }
}
