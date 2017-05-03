using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Ecole.ClasseConception
{
    public class LigneInsolvable
    {

        public int numero { get; set; }
        public string nom { get; set; }
        public string matricule { get; set; }
        public string categorie { get; set; }
        public double aPayer { get; set; }
        public double paye { get; set; }
        public double remise { get; set; }
        public double resteApayer { get; set; }
        public string observation { get; set; }

        public string SaPayer { get; set; }
        public string Spaye { get; set; }
        public string Sremise { get; set; }
        public string SresteApayer { get; set; }

        public LigneInsolvable()
        {
              numero=0;
              nom = "";
              matricule = "";
              categorie = "";
              aPayer = 0;
              paye = 0;
              remise = 0;
              resteApayer = 0;
              observation = "";
              SaPayer = "";
              Spaye = "";
              Sremise = "";
              SresteApayer = "";
               
        }

        public LigneInsolvable(int numero, string nom, string matricule, string categorie, double aPayer, double remise, double paye, double resteApayer, string observation)
        {
            CultureInfo ci_GR = CultureInfo.CreateSpecificCulture("el-GR");

            this.numero = numero;
            this.nom = nom;
            this.matricule = matricule;
            this.categorie = categorie;
            this.aPayer = aPayer;
            this.paye = paye;
            this.remise = remise;
            this.resteApayer = resteApayer;
            this.observation = observation;

            //decimal temp = decimal.Parse("123579,124");
            //MessageBox.Show(String.Format("{0:n}", temp));

            this.SaPayer = aPayer.ToString("0,0", ci_GR);
            this.Spaye = paye.ToString("0,0", ci_GR);
            this.Sremise = remise.ToString("0,0", ci_GR);
            this.SresteApayer = resteApayer.ToString("0,0", ci_GR);

            //this.SaPayer =String.Format("{0:n}", aPayer);
            //this.Spaye = String.Format("{0:n}", paye);
            //this.Sremise = String.Format("{0:n}", remise);
            //this.SresteApayer = String.Format("{0:n}", resteApayer);


        }
    }
}
