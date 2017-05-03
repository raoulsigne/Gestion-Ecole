using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseMetiers
{
    class LigneProgramme
    {
        public string matiere { get; set; }
        public int coeficient { get; set; }
        public string enseignant { get; set; }

        public LigneProgramme()
        {
            matiere = "";
            coeficient = 0;
            enseignant = "";
        }

        public LigneProgramme(string mat, int coef, string enseignant)
        {
            this.matiere = mat;
            this.coeficient = coef;
            this.enseignant = enseignant;
        }

        public LigneProgramme(string mat, int coef)
        {
            this.matiere = mat;
            this.coeficient = coef;
        }
    }
}
