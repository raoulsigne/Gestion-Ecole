using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class RealiserBE
    {
        // declaration des éléments;
        public string codeop { get; set; }
        public string login { get; set; }
        public string numeroop { get; set; }
        public string motif { get; set; }
        public decimal montant { get; set; }
        public DateTime dateop { get; set; }
        public string concerne { get; set; }
        public string date { get; set; }

        public RealiserBE()
        {
            this.codeop = "";
            this.login = "";
            this.numeroop = "";
            this.motif = "";
            this.montant = 0;
            this.dateop = DateTime.Today;
            this.date = DateTime.Today.ToShortDateString();
            this.concerne = "";
        }
       
        public RealiserBE(string codeO, string login, string numeroop, string motif, decimal montant, DateTime dateop, string concerne)
        {
            this.codeop = codeO;
            this.login = login;
            this.numeroop = numeroop;
            this.motif = motif;
            this.montant = montant;
            this.dateop = dateop;
            this.concerne = concerne;
            this.date = dateop.ToShortDateString();
        }
    }
}
