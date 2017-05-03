using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    class LigneProgrammer
    {
        public string matiere { get; set; }
        public int coeficient { get; set; }
        public string codegroupe { get; set; }
        public string codeprof { get; set; }
        public string nomprof { get; set; }

        public LigneProgrammer()
        {
            matiere = "";
            coeficient = 0;
            codegroupe = "";
            codeprof = "";
            nomprof = "";
        }

        public LigneProgrammer(string mat, int coef, string codegroupe, string codeprof, string nomprof)
        {
            this.matiere = mat;
            this.coeficient = coef;
            this.codegroupe = codegroupe;
            this.codeprof = codeprof;
            this.nomprof = nomprof;
        }
    }
}
