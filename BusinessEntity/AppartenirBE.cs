using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class AppartenirBE
    {
        public string codeCatEleve { get; set; }
        public string matricule { get; set; }
        public long annee { get; set; }

        public virtual CategorieEleveBE categorieeleve { get; set; }
        public virtual EleveBE eleve { get; set; }

        public AppartenirBE()
        {
            codeCatEleve = "";
            matricule = "";
            annee = DateTime.Today.Year;
        }

        public AppartenirBE(String code, String matricule, long annee)
        {
            this.codeCatEleve = code;
            this.matricule = matricule;
            this.annee = annee;
        }
    }
}
