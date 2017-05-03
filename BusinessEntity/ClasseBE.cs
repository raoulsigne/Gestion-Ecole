using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class ClasseBE
    {
        public ClasseBE()
        {
            this.dirigers = new HashSet<DirigerBE>();
            this.evaluers = new HashSet<EvaluerBE>();
            this.codeClasse = "";
            this.codeCycle = "";
            this.codeTypeClasse = "";
            this.codeSerie = "";
            this.codeNiveau = "";
            this.nomClasse = "";
        }

        public ClasseBE(String classe, String cycle, String typeClasse, String serie, String niveau, String nom)
        {
            this.dirigers = new HashSet<DirigerBE>();
            this.evaluers = new HashSet<EvaluerBE>();
            this.codeClasse = classe;
            this.codeCycle = cycle;
            this.codeTypeClasse = typeClasse;
            this.codeSerie = serie;
            this.codeNiveau = niveau;
            this.nomClasse = nom;
        }
    
        public string codeClasse { get; set; }
        public string codeTypeClasse { get; set; }
        public string codeCycle { get; set; }
        public string codeSerie { get; set; }
        public string codeNiveau { get; set; }
        public string nomClasse { get; set; }

        public virtual CycleBE cycle { get; set; }
        public virtual ICollection<DirigerBE> dirigers { get; set; }
        public virtual ICollection<EvaluerBE> evaluers { get; set; }
    }
}
