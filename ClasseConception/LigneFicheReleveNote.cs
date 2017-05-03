using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
namespace Ecole.ClasseConception
{
    public class LigneFicheReleveNote
    {
        public int numero { get; set; }
        public string nom { get; set; }
        public string matricule { get; set; }
        public List<string[]> evaluation { get; set; }

        public LigneFicheReleveNote() 
        {
            numero = 0;
            nom = "";
            matricule = "";
            evaluation = null;
        }

        public LigneFicheReleveNote(int numero, string nom, string matricule, List<string[]> evaluation)
        {
            this.numero = numero;
            this.nom = nom;
            this.matricule = matricule;
            this.evaluation = evaluation;
        }
    } 
}
