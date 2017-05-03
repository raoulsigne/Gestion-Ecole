using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class SetarticleBE
    {
             // declaration des éléments;
        public string codesetarticle { get; set; }
        public int annee { get; set; }
        public string nomsetarticle { get; set; }
        public decimal montant { get; set; }

        public SetarticleBE()
        {
            this.codesetarticle = "";
            this.annee = 2000;
            this.nomsetarticle = "";
            this.montant = 0;

        }
            
       
        public SetarticleBE(string codeS, int annee, string nomS, decimal montant)
        {
            this.codesetarticle = codeS;
            this.annee = annee;
            this.nomsetarticle = nomS;
            this.montant = montant;
            
        }
    }
}
