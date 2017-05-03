using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Ecole.ClasseConception
{
    class LigneBilan
    {
        public string prestation { get; set; }
        public double APayer { get; set; }
        public double paye { get; set; }
        public double remise { get; set; }
        public double reste { get; set; }
        public int numero { get; set; }

        public string SAPayer { get; set; }
        public string Spaye { get; set; }
        public string Sremise { get; set; }
        public string Sreste { get; set; }

        public LigneBilan()
        {
            this.numero = 0;
            this.prestation = "";
            this.APayer = 0;
            this.paye = 0;
            this.remise = 0;
            this.reste = 0;

            this.SAPayer = "";
            this.Spaye = "";
            this.Sremise = "";
            this.Sreste = "";
        }

        public LigneBilan(int num, string prest, double apayer, double paye, double remise, double reste)
        {
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR"); 
            this.numero = num;
            this.prestation = prest;
            this.APayer = apayer;
            this.paye = paye;
            this.remise = remise;
            this.reste = reste;

            this.SAPayer = apayer.ToString("0,0", elGR);
            this.Spaye = paye.ToString("0,0", elGR);
            this.Sremise = remise.ToString("0,0", elGR);
            this.Sreste = reste.ToString("0,0", elGR);
        }
    }
}
