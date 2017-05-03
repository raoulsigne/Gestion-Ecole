using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class MagasinBE
    {
        // définition des attributs -----------------------------------------
        // attribut codeMagasin
        public String codeMagasin { get; set; }
      
        // attribut nomMagasin
        public String nomMagasin { get; set; }      

        // constructeur de la classe -----------------------------------------
        public MagasinBE() { 
        }

        // constructeur avec paramètres --------------------------------------
        public MagasinBE(String codeMagasin, String nomMagasin)
        {
            this.codeMagasin = codeMagasin;
            this.nomMagasin = nomMagasin;
        }
    }
}
