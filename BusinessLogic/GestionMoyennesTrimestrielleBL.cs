using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class GestionMoyennesTrimestrielleBL
    {
        ClasseDA classeDA;
        EleveDA eleveDA;
        ResultatTrimestrielDA resultatDA;
        ParametresDA parametreDA;
        TrimestreDA trimestreDA;
        JournalDA journalDA;

        public GestionMoyennesTrimestrielleBL()
        {
            classeDA = new ClasseDA();
            eleveDA = new EleveDA();
            resultatDA = new ResultatTrimestrielDA();
            parametreDA = new ParametresDA();
            trimestreDA = new TrimestreDA();
            journalDA = new JournalDA();
        }

        public List<string> listerValeurColonneTrimestre(string p)
        {
            return trimestreDA.listerValeursColonne(p);
        }

        public TrimestreBE rechercherTrimestre(TrimestreBE trimestre)
        {
            return trimestreDA.rechercher(trimestre);
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

        internal List<ResultatTrimestrielBE> listerSuivantCritereResultatTrimestriel(string codeclasse, string codetrimestre, int annee)
        {
            journalDA.journaliser("Génération de l'état des moyennes du trimestre " +codetrimestre+ " de "+codeclasse);
            return resultatDA.listerResultatsTrimestrielDesElevesDuneClasse(codeclasse, codetrimestre, annee);
        }


        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }
    }
}
