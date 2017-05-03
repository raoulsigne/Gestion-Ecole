using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class LigneChangeClasse
    {
        public int numero { get; set; }
        public string nom { get; set; }
        public string matricule { get; set; }
        public string codeclasse { get; set; }

        public LigneChangeClasse()
        {
            this.numero = 0;
            this.nom = "";
            this.matricule = "";
            this.codeclasse = "";
        }

        public LigneChangeClasse(int numero, string nom, string matricule, string codeclasse)
        {
            this.numero = numero;
            this.nom = nom;
            this.matricule = matricule;
            this.codeclasse = codeclasse;
        }
    }
}
