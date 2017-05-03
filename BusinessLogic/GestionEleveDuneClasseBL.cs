using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    class GestionEleveDuneClasseBL
    {
        ClasseDA classeDA;
        EleveDA eleveDA;
        ParametresDA parametreDA;
        InscrireDA inscrireDA;
        JournalDA journalDA;

        public GestionEleveDuneClasseBL()
        {
            classeDA = new ClasseDA();
            eleveDA = new EleveDA();
            parametreDA = new ParametresDA();
            inscrireDA = new InscrireDA();
            journalDA = new JournalDA();
        }

        public List<string> listerValeursColonneClasse(string p)
        {
            return classeDA.listerValeursColonne("codeclasse");
        }

        public int anneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        public List<InscrireBE> listerSuivantCritereInscrire(string codeclasse, int annee)
        {
            return inscrireDA.listerSuivantCritere(" codeclasse = " + "'" + codeclasse + "' and annee = " + "'" + annee + "'");
        }

        public EleveBE rechercherEleve(EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
        }

        public ClasseBE rechercherClasse(ClasseBE classe)
        {
            return classeDA.rechercher(classe);
        }

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }
    }
}
