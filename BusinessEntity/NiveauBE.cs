using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class NiveauBE
    {
        // définition des attributs -----------------------------------------
        // attribut codeNiveau
        public String codeNiveau { get; set; }      

        // attribut nomNiveau
        public String nomNiveau { get; set; }      

        // attribut niveau
        public int niveau { get; set; }     

        // constructeur de la classe -----------------------------------------
        public NiveauBE() { }

        // constructeur avec paramètres --------------------------------------
        public NiveauBE(String codeNiveau, String nomNiveau, int niveau)
        {
            this.codeNiveau = codeNiveau;
            this.nomNiveau = nomNiveau;
            this.niveau = niveau;
        }
    }
}
