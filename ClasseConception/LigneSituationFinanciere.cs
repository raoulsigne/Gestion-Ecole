using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class LigneSituationFinanciere
    {

        public int numero { get; set; }
        public string nom { get; set; }
        public string matricule { get; set; }
        public string categorie { get; set; }
        public string prestation { get; set; }
        public double aPayer { get; set; }
        public double paye { get; set; }
        public double remise { get; set; }
        public double resteApayer { get; set; }

        public LigneSituationFinanciere()
        {
              numero=0;
              nom = "";
              matricule = "";
              categorie = "";
              prestation = "";
              aPayer = 0;
              paye = 0;
              remise = 0;
              resteApayer = 0;
               
        }

        public LigneSituationFinanciere(int numero,string nom,string matricule,string categorie,string prestation,double aPayer,double remise, double paye,double resteApayer)
        {
            this.numero = numero;
            this.nom = nom;
            this.matricule = matricule;
            this.categorie = categorie;
            this.prestation = prestation;
            this.aPayer = aPayer;
            this.paye = paye;
            this.remise = remise;
            this.resteApayer = resteApayer;

        }
    }
}
