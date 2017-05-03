using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class LigneEleve
    {
        public int numero { get; set; }
        public string nom { get; set; }
        public string matricule { get; set; }
        public string datenaissance { get; set; }
        public string telephone { get; set; }
        public string telparent { get; set; }
        public string adresse { get; set; }

        public LigneEleve(int num, string mat, string nom, string date, string telephone, string telparent, string adresse)
        {
            this.numero = num;
            this.nom = nom;
            this.matricule = mat;
            this.datenaissance = date;
            this.telephone = telephone;
            this.telparent = telparent;
            this.adresse = adresse;
        }

        public LigneEleve()
        {
            this.numero = 0;
            this.nom = "";
            this.matricule = "";
            this.datenaissance = "";
            this.telephone = "";
            this.telparent = "";
            this.adresse = "";
        }
    }
}
