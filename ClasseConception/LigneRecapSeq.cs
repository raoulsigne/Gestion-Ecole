using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.ClasseConception
{
    public class LigneRecapSeq
    {
        public string nom { get; set; }
        public string sexe_redoub { get; set; }
        public Dictionary<string, double> moyennesSequentielles { get; set; }
        public double total { get; set; }
        public double moyenne { get; set; }
        public int rang { get; set; }
        public Dictionary<string, int> sanctions { get; set; }
        public int nb_sous_moyenne { get; set; }
        public string mention { get; set; }

        public LigneRecapSeq(string nom, string sexe_redoub, Dictionary<string, double> moyennesSeq, double total, double moyenne, int rang, Dictionary<string, int> sanctions, int nbsousmoyennes, string mention)
        {
            this.nom = nom;
            this.sexe_redoub = sexe_redoub;
            this.moyenne = moyenne;
            this.rang = rang;
            this.moyennesSequentielles = new Dictionary<string, double>(moyennesSeq);
            this.sanctions = new Dictionary<string, int>(sanctions);
            this.total = total;
            this.nb_sous_moyenne = nbsousmoyennes;
            this.mention = mention;
        }

        public LigneRecapSeq()
        {
            this.nom = "";
            this.sexe_redoub = "";
            this.moyenne = 0;
            this.rang = 0;
            this.moyennesSequentielles = new Dictionary<string, double>();
            this.sanctions = new Dictionary<string, int>();
            this.total = 0;
            this.nb_sous_moyenne = 0;
            this.mention = "";
        }
    }
}
