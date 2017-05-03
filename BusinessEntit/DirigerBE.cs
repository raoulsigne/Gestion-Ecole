using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_Ecole.BusinessEntity
{
    class DirigerBE
    {
        public string codeClasse { get; set; }
        public string codeProf { get; set; }
        public int annee { get; set; }

        public virtual ClasseBE classe { get; set; }
        public virtual EnseignantBE enseignant { get; set; }

        public DirigerBE() 
        {
            this.codeClasse = "";
            this.codeProf = "";
            this.annee = DateTime.Today.Year;
        }

        public DirigerBE(String classe, String prof, int annee)
        {
            this.codeClasse = classe;
            this.codeProf = prof;
            this.annee = annee;
        }
    }
}
