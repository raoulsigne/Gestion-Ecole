using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class LigneRecapitulatif
    {
        public string nom { get; set; }
        public Dictionary<string, double> moyennesGroupe { get; set; }
        public double moyenne { get; set; }
        public int rang { get; set; }
        public Dictionary<string, double> moyennesSequentielles { get; set; }
        public double total { get; set; }
        public string mention { get; set; }

        public LigneRecapitulatif(string nom, Dictionary<string, double> groupes, double moyenne, int rang, Dictionary<string, double> moyennesSeq, double total, string mention)
        {
            this.nom = nom;
            this.moyennesGroupe = new Dictionary<string, double>(groupes);
            this.moyenne = moyenne;
            this.rang = rang;
            this.moyennesSequentielles = new Dictionary<string, double>(moyennesSeq);
            this.total = total;
            this.mention = mention;
        }

        public LigneRecapitulatif()
        {
            this.nom = "";
            this.moyennesGroupe = new Dictionary<string, double>();
            this.moyenne = 0;
            this.rang = 0;
            this.moyennesSequentielles = new Dictionary<string, double>();
            this.total = 0;
            this.mention = "";
        }
    }
}