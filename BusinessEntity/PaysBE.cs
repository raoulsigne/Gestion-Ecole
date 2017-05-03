using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class PaysBE
    {
        // définition des attributs -----------------------------------------
        // attribut codePays
        public String codePays { get; set; }

        // attribut nomPays
        public String nomPays { get; set; }

        // attribut nationalite
        public String nationalite { get; set; }

        // constructeur de la classe -----------------------------------------
        public PaysBE() { }

        // constructeur avec paramètres --------------------------------------
        public PaysBE(String codePays, String nomPays, String nationalite)
        {
            this.codePays = codePays;
            this.nomPays = nomPays;
            this.nationalite = nationalite;
        }
    }
}
