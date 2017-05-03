using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    class AssistanceCreationClasseBL
    {
        public static string CHOIX_NOUVEAU = "Nouvelle entrée";

        CycleDA cycleDA;
        TypeclasseDA typeclasseDA;
        NiveauDA niveauDA;
        SerieDA serieDA;
        ClasseDA classeDA;

        public AssistanceCreationClasseBL()
        {
            serieDA = new SerieDA();
            niveauDA = new NiveauDA();
            typeclasseDA = new TypeclasseDA();
            cycleDA = new CycleDA();
            classeDA = new ClasseDA();
        }

        internal List<string> listerValeurColonneCycle(string p)
        {
            return cycleDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneTypeclasse(string p)
        {
            return typeclasseDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneNiveau(string p)
        {
            return niveauDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneSerie(string p)
        {
            return serieDA.listerValeursColonne(p);
        }

        internal CycleBE rechercherCycle(CycleBE cycle)
        {
            return cycleDA.rechercher(cycle);
        }

        internal NiveauBE rechercherNiveau(NiveauBE niveau)
        {
            return niveauDA.rechercher(niveau);
        }

        internal TypeclasseBE rechercherTypeClasse(TypeclasseBE type)
        {
            return typeclasseDA.rechercher(type);
        }

        internal SerieBE rechercherSerie(SerieBE serie)
        {
            return serieDA.rechercher(serie);
        }

        internal bool enregistrerCycle(CycleBE cycle)
        {
            return cycleDA.ajouter(cycle);
        }

        internal bool enregistrerNiveau(NiveauBE niveau)
        {
            return niveauDA.ajouter(niveau);
        }

        internal bool enregistrerTypeClasse(TypeclasseBE type)
        {
            return typeclasseDA.ajouter(type);
        }

        internal bool enregistrerSerie(SerieBE serie)
        {
            return serieDA.ajouter(serie);
        }

        internal bool enregistrerClasse(ClasseBE classe)
        {
            return classeDA.ajouter(classe);
        }
    }
}
