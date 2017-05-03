using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class CategorieEleveBE
    {
        public CategorieEleveBE()
        {
            this.appartenirs = new HashSet<AppartenirBE>();
            codeCatEleve = "";
            nomCatEleve = "";
        }

        public CategorieEleveBE(String code, String nom)
        {
            this.appartenirs = new HashSet<AppartenirBE>();
            this.codeCatEleve = code;
            this.nomCatEleve = nom;
        }

        public string codeCatEleve { get; set; }
        public string nomCatEleve { get; set; }

        public virtual ICollection<AppartenirBE> appartenirs { get; set; }
    }
}
