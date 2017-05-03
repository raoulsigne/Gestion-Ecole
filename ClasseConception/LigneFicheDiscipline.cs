using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
namespace Ecole.ClasseConception
{
    public class LigneFicheDiscipline
    {
        public int numero { get; set; }
        public string nom { get; set; }
        public string matricule { get; set; }
        public List<DisciplineBE> discipline { get; set; }

        public LigneFicheDiscipline() 
        {
            numero = 0;
            nom = "";
            matricule = "";
            discipline = null;
        }

        public LigneFicheDiscipline(int numero, string nom, string matricule, List<DisciplineBE> discipline)
        {
            this.numero = numero;
            this.nom = nom;
            this.matricule = matricule;
            this.discipline = discipline;
        }
    } 
}
