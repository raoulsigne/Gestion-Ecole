using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class LigneNote
    {
        public int numero { get; set; }
        public string nom { get; set; }
        public string matricule { get; set; }
        public string anonymat { get; set; }
        public string notes { get; set; }

        public LigneNote(int numero, string nom, string mat, string anonymat,decimal note)
        {
            this.numero = numero;
            this.nom = nom;
            this.matricule = mat;
            this.anonymat = anonymat;
            if (note != -1)
                this.notes = Convert.ToString(note);
            else
                this.notes = "A";
        }

        public LigneNote(int numero, string nom, string mat, string anonymat)
        {
            this.numero = numero;
            this.nom = nom;
            this.matricule = mat;
            this.anonymat = anonymat;
            this.notes = "";
        }

        public LigneNote()
        {
            this.numero = 0;
            this.notes = "A";
            this.nom = "";
            this.matricule = "";
            this.anonymat = "";
        }
    }
}
