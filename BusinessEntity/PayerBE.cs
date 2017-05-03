using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class PayerBE
    {
        // définition des attributs -----------------------------------------
        // attribut matricule
        public String matricule { get; set; }

        // attribut login
        public String login { get; set; }

        // attribut codePrestation
        public String codePrestation { get; set; }

         // attribut codeTranche
        public String codeTranche { get; set; }

         // attribut montant
        public Double montant { get; set; }

         // attribut datePaiement
        public DateTime datePaiement { get; set; }

         // attribut annee
        public int annee { get; set; }

         // attribut observation
        public String observation { get; set; }

        public decimal remise { get; set; }

        // constructeur de la classe -----------------------------------------
        public PayerBE() { }

        // constructeur avec paramètres --------------------------------------
        public PayerBE(String matricule, String login, String codePrestation, String codeTranche, Double montant,
            DateTime datePaiement, int annee, String observation, decimal remise)
        {
            this.matricule = matricule;
            this.login = login;
            this.codePrestation = codePrestation;
            this.codeTranche = codeTranche;
            this.montant = montant;
            this.datePaiement = datePaiement;
            this.annee = annee;
            this.observation = observation;
            this.remise = remise;
        }
    }
}
