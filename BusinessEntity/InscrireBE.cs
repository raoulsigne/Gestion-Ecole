using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class InscrireBE
    {
        // définition des attributs -----------------------------------------
        // attribut codeClasse
        public String codeClasse { get; set; }

        // attribut matricule
        public String matricule { get; set; }       

        // attribut annee 
        public int annee { get; set; }

        public EleveBE eleve { get; set; }
        public CategorieEleveBE categorieEleve { get; set; }

        // constructeur de la classe -----------------------------------------
        public InscrireBE()
        {
            this.codeClasse = "";
            this.matricule = "";
            this.annee = Convert.ToInt16(System.DateTime.Today.Year);

            this.eleve = new EleveBE();
            this.categorieEleve = new CategorieEleveBE();
        }

        // constructeur avec paramètres --------------------------------------
        public InscrireBE(String codeClasse, String matricule, int annee)
        {
            this.codeClasse = codeClasse;
            this.matricule = matricule;
            this.annee = annee;

            this.eleve = new EleveBE();
            this.categorieEleve = new CategorieEleveBE();
        }
    }
}
