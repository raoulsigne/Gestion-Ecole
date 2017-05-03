using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class MatiereBE
    {
        // définition des attributs -----------------------------------------
        // attribut codeMatiere
        public String codeMat { get; set; }

        // attribut nomMatière
        public String nomMat { get; set; }

        // attribut nameMatière (le nom de la matière en anglais)
        public String nameMat { get; set; }
       
        // attribut annee
        public int annee { get; set; }

        // constructeur de la classe -----------------------------------------
        public MatiereBE() { }

        // constructeur avec paramètres --------------------------------------
        public MatiereBE(String codeMatiere, String nomMatiere, String nameMatiere, int annee)
        {
            this.codeMat = codeMatiere;
            this.nomMat = nomMatiere;
            this.nameMat = nameMatiere;
            this.annee = annee;
        }
    }
}
