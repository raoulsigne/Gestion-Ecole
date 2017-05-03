using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Ecole.ClasseConception
{
    class LigneHistorique
    {
        #region field and properties
        public int numero { get; set; }
        public string motif { get; set; }
        public int montant { get; set; }
        public string entree { get; set; }
        public string sortie { get; set; }
        public string date { get; set; }
        public string concerne { get; set; }
        #endregion

        public LigneHistorique()
        {
            numero = 0;
            montant = 0;
            motif = "";
            date = "";
            entree = "";
            sortie = "";
            concerne = "";
        }

        public LigneHistorique(int numero, string motif, string date, int entree, int sortie, string concerne)
        {
            this.numero = numero;
            this.motif = motif;
            if (entree > 0)
            {
                this.entree = entree.ToString();
                this.montant = entree;
            }
            else
                this.entree = "-";
            if (sortie > 0)
            {
                this.sortie = sortie.ToString();
                this.montant = sortie;
            }
            else
                this.sortie = "-";
            this.date = date;
            this.concerne = concerne;
        }
    }
}
