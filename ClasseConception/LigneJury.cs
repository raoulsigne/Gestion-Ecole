using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class LigneJury
    {
        public int numero { get; set; }
        public string  nom { get; set; }
        public string matricule { get; set; }
        public string remarque { get; set; }

        public LigneJury()
        {
            this.numero = 0;
            this.nom = "";
            this.matricule = "";
            this.remarque = "";
        }

        public LigneJury(int num, string nom, string mat, string remarque)
        {
            this.numero = num;
            this.nom = nom;
            this.matricule = mat;
            this.remarque = remarque;
        }
    }
}
