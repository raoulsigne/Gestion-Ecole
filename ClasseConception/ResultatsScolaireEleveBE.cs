using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class ResultatsScolaireEleveBE
    {
        public string matricule { get; set; } 
        public string codeClasse { get; set; }
        public string periode { get; set; }
        public int annee { get; set; }
        public Double moyenne { get; set; }
        public int rang { get; set; }
        public string mention { get; set; }
        public string decision { get; set; }

         public ResultatsScolaireEleveBE()
        {
            this.matricule = "";
            this.codeClasse = "";
            this.periode = "";
            this.annee = DateTime.Today.Year;
            this.moyenne = 0;
            this.rang = 0;
            this.mention = "";
            this.decision = "";
        }

         public ResultatsScolaireEleveBE(String matricule, String codeClasse, String periode, int annee, Double moyenne, int rang,
             string mention, string decision)
        {
            this.matricule = matricule;
            this.codeClasse = codeClasse;
            this.periode = periode;
            this.annee = annee;
            this.moyenne = moyenne;
            this.rang = rang;
            this.mention = mention;
            this.decision = decision;
        }  
    }
}
