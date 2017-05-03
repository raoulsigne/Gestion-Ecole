using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Ecole.BusinessLogic;
using System.Threading;

namespace Ecole.ClasseConception
{
    public class LigneVersement
    {
        public int numero { get; set; }
        public String matricule { get; set; }
        public String libelle { get; set; }
        public double montant { get; set; }
        public String date { get; set; }
        public int annee { get; set; }
        public String nom { get; set; }
        public String classe { get; set; }
        public string smontant { get; set; }

        public LigneVersement(String matricule, String libelle, double montant, DateTime date, int annee)
        {
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            this.matricule = matricule;
            this.libelle = libelle;
            this.montant = montant;
            this.date = date.ToShortDateString();
            this.annee = annee;

            this.smontant = montant.ToString("0,0", elGR);
        }

        public LigneVersement(int numero, String matricule, String libelle, double montant, DateTime date, int annee)
        {
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            this.numero = numero;
            this.matricule = matricule;
            this.libelle = libelle;
            this.montant = montant;
            this.date = date.ToShortDateString();
            this.annee = annee;

            this.smontant = montant.ToString("0,0", elGR);
        }
    }
}
