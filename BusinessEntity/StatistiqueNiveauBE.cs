using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
   public class StatistiqueNiveauBE
    {
        public string codeNiveau { get; set; }
        public int effectif { get; set; }
        public int nbAdmis { get; set; }
        public string pourcentageAdmis { get; set; }
        public int nbEchec { get; set; }
        public string pourcentageEchec { get; set; }

         public StatistiqueNiveauBE()
        {
            codeNiveau = "";
            effectif = 0;
            nbAdmis = 0;
            pourcentageAdmis = "";
            nbEchec = 0;
            pourcentageEchec = "";
        }

         public StatistiqueNiveauBE(string codeNiveau, int effectif, int nbAdmis, string pourcentageAdmis, int nbEchec, string pourcentageEchec)
        {
            this.codeNiveau = codeNiveau;
            this.effectif = effectif;
            this.nbAdmis = nbAdmis;
            this.pourcentageAdmis = pourcentageAdmis;
            this.nbEchec = nbEchec;
            this.pourcentageEchec = pourcentageEchec;
        }
    }
}
