using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class PrestationBE
    {
        // définition des attributs -----------------------------------------
        // attribut codePrestation
        public String codePrestation { get; set; }

        // attribut nomPrestation
        public String nomPrestation { get; set; }

        //attribut priorite
        public int priorite {get; set;}

        // constructeur de la classe -----------------------------------------
        public PrestationBE() {
            this.codePrestation = "";
            this.nomPrestation = "";
            this.priorite = 0;
        }

        // constructeur avec paramètres --------------------------------------
        public PrestationBE(String codePrestation, String nomPrestation, int priorite)
        {
            this.codePrestation = codePrestation;
            this.nomPrestation = nomPrestation;
            this.priorite = priorite;
        }
    }
}
