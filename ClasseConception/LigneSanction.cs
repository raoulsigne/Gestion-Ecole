using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;

namespace Ecole.ClasseConception
{
    public class LigneSanction
    {
        public int numero { get; set; }
        public string nom { get; set; }
        public string matricule { get; set; }
        public int nombre { get; set; }
        public string etat { get; set; }
        public string date { get; set; }

        public LigneSanction()
        {
            this.numero = 0;
            this.nom = "";
            this.matricule = "";
            this.nombre = 0;
            this.etat = "NON JUSTIFIEE";
            this.date = "";
        }

        public LigneSanction(int numero, string nom, string mat, int nombre, string etat, DateTime date)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            this.numero = numero;
            this.nom = nom;
            this.matricule = mat;
            this.nombre = nombre;
            this.etat = etat;
            this.date = date.ToShortDateString();
        }
    }
}
