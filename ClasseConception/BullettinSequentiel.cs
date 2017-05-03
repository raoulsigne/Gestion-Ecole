using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using Ecole.ClasseConception;

namespace Ecole.ClasseConception
{
    public class BullettinSequentiel
    {
        public BullettinSequentiel()
        {
            this.eleve = null;
            this.classe = null;
            this.nbEvaluation = 0;
            this.listLigneBulletinSequentiel = null;
            this.resultatSequentiel = null;
            this.ListSanction = null;
        }

        public BullettinSequentiel(EleveBE eleve, ClasseBE classe, int annee, int nbEvaluation, List<LigneBulletinSequentiel> listLigneBulletinSequentiel, ResultatBE resultatSequentiel, List<SanctionnerBE> ListSanction)
        {
            this.eleve = null;
            this.classe = null;
            this.annee = annee;
            this.nbEvaluation = nbEvaluation;
            this.listLigneBulletinSequentiel = listLigneBulletinSequentiel;
            this.resultatSequentiel = resultatSequentiel;
            this.ListSanction = ListSanction;
        }

        public EleveBE eleve { get; set; } // l'élève
        public ClasseBE classe { get; set; } // la classe de l'élève
        public int annee { get; set; } //l'année

        public int nbEvaluation { get; set; } // le nombre d'évalauation de la séquence

        public List<LigneBulletinSequentiel> listLigneBulletinSequentiel { get; set; }


        public ResultatBE resultatSequentiel { get; set; } // le résultat séquentiel de l'élève

        //la moyenne minimale des élèves de sa classe pour la séquence et l'année choisi
        public double moyenneMin { get; set; }

        //la moyenne maximale des élèves de sa classe pour la séquence et l'année choisi
        public double moyenneMax { get; set; }

        public List<SanctionnerBE> ListSanction { get; set; } //les infos sur la discipline de l'élève

    }
}
