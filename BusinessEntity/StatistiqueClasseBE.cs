using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
     public class StatistiqueClasseBE
    {
        public string codeClasse { get; set; }
        public int effectif { get; set; }
        public int nbAdmis { get; set; }
        public string pourcentageAdmis { get; set; }
        public int nbEchec { get; set; }
        public string pourcentageEchec { get; set; }

        public StatistiqueClasseBE()
        {
            codeClasse = "";
            effectif = 0;
            nbAdmis = 0;
            pourcentageAdmis = "";
            nbEchec = 0;
            pourcentageEchec = "";
        }

        public StatistiqueClasseBE(string codeClasse, int effectif, int nbAdmis, string pourcentageAdmis, int nbEchec, string pourcentageEchec)
        {
            this.codeClasse = codeClasse;
            this.effectif = effectif;
            this.nbAdmis = nbAdmis;
            this.pourcentageAdmis = pourcentageAdmis;
            this.nbEchec = nbEchec;
            this.pourcentageEchec = pourcentageEchec;
        }

    }
}
