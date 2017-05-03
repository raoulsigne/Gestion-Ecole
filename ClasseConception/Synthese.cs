using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class Synthese
    {
        public Dictionary<string, double> synthese_classe { get; set; }
        public Dictionary<string, double> synthese_garcon { get; set; }
        public Dictionary<string, double> synthese_fille { get; set; }
        public Dictionary<string, double> synthese_premiers { get; set; }
        public Dictionary<string, double> synthese_derniers { get; set; }

        public Synthese()
        {
            this.synthese_classe =  new Dictionary<string, double>();
            this.synthese_garcon = new Dictionary<string, double>();
            this.synthese_fille = new Dictionary<string, double>();
            this.synthese_premiers = new Dictionary<string, double>();
            this.synthese_derniers = new Dictionary<string, double>();
        }

        public Synthese(Dictionary<string, double> synthese_classe, Dictionary<string, double> synthese_garcon,
            Dictionary<string, double> synthese_fille, Dictionary<string, double> synthese_premiers, Dictionary<string, double> synthese_derniers)
        {
            this.synthese_classe = new Dictionary<string, double>(synthese_classe);
            this.synthese_garcon = new Dictionary<string, double>(synthese_garcon);
            this.synthese_fille = new Dictionary<string, double>(synthese_fille);
            this.synthese_premiers = new Dictionary<string, double>(synthese_premiers);
            this.synthese_derniers = new Dictionary<string, double>(synthese_derniers);
        }
    }
}
