using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class GestionMoyennesAnnuellesBL
    {
        ClasseDA classeDA;
        EleveDA eleveDA;
        ResultatAnnuelDA resultatDA;
        ParametresDA parametreDA;
        JournalDA journalDA;

        public GestionMoyennesAnnuellesBL()
        {
            classeDA = new ClasseDA();
            eleveDA = new EleveDA();
            resultatDA = new ResultatAnnuelDA();
            parametreDA = new ParametresDA();
            journalDA = new JournalDA();
        }

        public List<ResultatAnnuelBE> listerSuivantCritereResultatAnnuel(string codeclasse, int annee)
        {
            journalDA.journaliser("Génération de l'état des moyennes annuelle de " + codeclasse + " de " +annee);
            return resultatDA.listerResultatsAnnuelsDesElevesDuneClasse(codeclasse,annee);
        }

        public List<string> listerValeurColonneClasse(string p)
        {
            return classeDA.listerValeursColonne(p);
        }

        public EleveBE rechercherEleve(EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
        }

        public ClasseBE rechercherClasse(ClasseBE classe)
        {
            return classeDA.rechercher(classe);
        }

        public int anneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }
    }
}
