using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using Ecole.ClasseConception;

namespace Ecole.ClasseConception
{
    public class BulletinTrimestriel
    {
        public BulletinTrimestriel()
        {
            this.eleve = null;
            this.classe = null;
            this.nbSequence = 0;
            this.listLigneBulletinTrimestriel = null;
            this.resultattrimestriel = null;
            this.ListSanction = null;
        }

        public BulletinTrimestriel(EleveBE eleve, ClasseBE classe, int annee, int nbSequence, List<LigneBulletinTrimestriel> listLigneBulletinTrimestriel, ResultatTrimestrielBE resultattrimestriel, List<SanctionnerBE> ListSanction)
        {
            this.eleve = null;
            this.classe = null;
            this.annee = annee;
            this.nbSequence = nbSequence;
            this.listLigneBulletinTrimestriel = listLigneBulletinTrimestriel;
            this.resultattrimestriel = resultattrimestriel;
            this.ListSanction = ListSanction;
        }

        public EleveBE eleve { get; set; } // l'élève
        public ClasseBE classe { get; set; } // la classe de l'élève
        public int annee { get; set; } //l'année

        public int nbSequence { get; set; } // le nombre de séquence du trimestre

        public List<LigneBulletinTrimestriel> listLigneBulletinTrimestriel { get; set; }


        public ResultatTrimestrielBE resultattrimestriel { get; set; } // le résultat trimestriel de l'élève

        //la moyenne minimale des élèves de sa classe pour le trimestre et l'année choisi
        public double moyenneMin { get; set; }

        //la moyenne maximale des élèves de sa classe pour le trimestre et l'année choisi
        public double moyenneMax { get; set; }

        public List<SanctionnerBE> ListSanction { get; set; } //les infos sur la discipline de l'élève

    }
}
