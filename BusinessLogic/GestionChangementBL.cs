using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    public class GestionChangementBL
    {

        ClasseDA classeDA;
        EleveDA eleveDA;
        InscrireDA inscrireDA;
        ParametresDA parametreDA;

        public GestionChangementBL()
        {
            classeDA = new ClasseDA();
            eleveDA = new EleveDA();
            inscrireDA = new InscrireDA();
            parametreDA = new ParametresDA();
        }

        internal List<BusinessEntity.EleveBE> listerElevesDuneClasse(string codeclasse, int annee)
        {
            return eleveDA.listeEleveDuneClasse(codeclasse, annee);
        }

        internal List<string> listerValeursColonneClasse(string p)
        {
            return classeDA.listerValeursColonne(p);
        }

        internal int AnneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        internal Boolean modifierInscrire(BusinessEntity.InscrireBE inscrire)
        {
            return inscrireDA.modifier(inscrire);
        }

        internal string obtenirNiveau(string codeclasse)
        {
            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeclasse;
            classe = classeDA.rechercher(classe);
            return classe.codeNiveau;
        }

        internal List<string> classeDunNiveau(string codeniveau)
        {
            List<ClasseBE> classes = classeDA.listerSuivantCritere("codeniveau = " + "'" + codeniveau + "'");
            List<string> resultat = new List<string>();
            if (classes != null)
                foreach (ClasseBE c in classes)
                    resultat.Add(c.codeClasse);
                
            return resultat;

        }
    }
}
