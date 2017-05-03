using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class MontantTrancheBE
    {
        // définition des attributs -----------------------------------------
        // attribut codeTranche
        public String codeTranche { get; set; }     

        // attribut codeCatEleve
        public String codeCatEleve { get; set; }      

        // attribut codePrestation
        public String codePrestation { get; set; }      

        // attribut montant
        public Double montant { get; set; }     

        // attribut annee
        public int annee { get; set; }       

        // attribut delai     
        public DateTime delai { get; set; }

        public string dateDelai { get; set; }

         // constructeur de la classe -----------------------------------------
        public MontantTrancheBE() { }

        // constructeur avec paramètres --------------------------------------
        public MontantTrancheBE(String codeTranche, String codeCatEleve, String codePrestation, Double montant, int annee, DateTime delai) {
            this.codeTranche = codeTranche;
            this.codeCatEleve = codeCatEleve;
            this.codePrestation = codePrestation;
            this.montant = montant;
            this.annee = annee;
            this.delai = delai;
            this.dateDelai = this.delai.ToShortDateString();
        }
    }
}
