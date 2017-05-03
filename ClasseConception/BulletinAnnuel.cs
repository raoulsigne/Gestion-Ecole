using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using Ecole.ClasseConception;

namespace Ecole.ClasseConception
{
    public class BulletinAnnuel
    {
         public BulletinAnnuel()
        {
            this.eleve = null;
            this.classe = null;
            this.nbTrimestre = 0;
             this.nbSequence = 0;
            this.listLigneBulletinAnnuel = null;
            this.resultatannuel = null;
            this.ListSanction = null;
        }

         public BulletinAnnuel(EleveBE eleve, ClasseBE classe, int annee, int nbTrimestre, int nbSequence, List<LigneBulletinAnnuel> listLigneBulletinAnnuel, ResultatAnnuelBE resultatannuel, List<SanctionnerBE> ListSanction)
        {
            this.eleve = null;
            this.classe = null;
            this.annee = annee;
            this.nbTrimestre = nbTrimestre;
            this.nbSequence = nbSequence;
            this.listLigneBulletinAnnuel = listLigneBulletinAnnuel;
            this.resultatannuel = resultatannuel;
            this.ListSanction = ListSanction;
        }

        public EleveBE eleve { get; set; } // l'élève
        public ClasseBE classe { get; set; } // la classe de l'élève
        public int annee { get; set; } //l'année

        public int nbTrimestre { get; set; } // le nombre de trimestre de l'année

        public int nbSequence { get; set; } // le nombre de trimestre de l'année

        public List<LigneBulletinAnnuel> listLigneBulletinAnnuel { get; set; }


        public ResultatAnnuelBE resultatannuel { get; set; } // le résultat annuel de l'élève

        //la moyenne minimale des élèves de sa classe pour l'année choisi
        public double moyenneMin { get; set; }

        //la moyenne maximale des élèves de sa classe pour l'année choisi
        public double moyenneMax { get; set; }

        public List<SanctionnerBE> ListSanction { get; set; } //les infos sur la discipline de l'élève
    }
}
