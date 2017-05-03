using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class EffectifClasseBE
    {
        public string codeClasse { get; set; }
        public int effectif { get; set; }

        public EffectifClasseBE()
        {
            codeClasse = "";
            effectif = 0;
        }

        public EffectifClasseBE(String codeClasse, int effectif)
        {
            this.codeClasse = codeClasse;
            this.effectif = effectif;
        }
    }
}
