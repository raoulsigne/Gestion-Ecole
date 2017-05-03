using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class LigneSaisieAnonymat
    {
        public int numero { get; set; }
        public string nom { get; set; }
        public string matricule { get; set; }
        public string anonymat { get; set; }

        public LigneSaisieAnonymat(int numero, string nom, string mat, string anonymat)
        {
            this.numero = numero;
            this.nom = nom;
            this.matricule = mat;
            this.anonymat = anonymat;
        }

        public LigneSaisieAnonymat()
        {
            this.numero = 0;
            this.nom = "";
            this.matricule = "";
            this.anonymat = "";
        }
    }
}
