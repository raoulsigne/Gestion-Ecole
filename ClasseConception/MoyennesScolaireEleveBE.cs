using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class MoyennesScolaireEleveBE
    {
       public string matricule { get; set; } 
        public string codeMatiere { get; set; }
        public string periode { get; set; }
        public int annee { get; set; }
        public Double moyenne { get; set; }
        public int rang { get; set; }
        public string mention { get; set; }

         public MoyennesScolaireEleveBE()
        {
            this.matricule = "";
            this.codeMatiere = "";
            this.periode = "";
            this.annee = DateTime.Today.Year;
            this.moyenne = 0;
            this.rang = 0;
            this.mention = "";
        }

         public MoyennesScolaireEleveBE(String matricule, String codeMatiere, String periode, int annee, Double moyenne, int rang,
             string mention)
        {
            this.matricule = matricule;
            this.codeMatiere = codeMatiere;
            this.periode = periode;
            this.annee = annee;
            this.moyenne = moyenne;
            this.rang = rang;
            this.mention = mention;
        }  
    }
}
