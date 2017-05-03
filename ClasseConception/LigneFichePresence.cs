using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class LigneFichePresence
    {
        public int numero { get; set; }
        public string nom { get; set; }
        public string matricule { get; set; }

        public LigneFichePresence()
        {
            numero = 0;
            nom = "";
            matricule = "";
        }

        public LigneFichePresence(int numero, string nom, string matricule)
        {
            this.numero = numero;
            this.nom = nom;
            this.matricule = matricule;
        }
    }
}
