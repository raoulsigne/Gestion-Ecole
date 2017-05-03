using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class GestionMoyennesSequentielleBL
    {
        ClasseDA classeDA;
        EleveDA eleveDA;
        ResultatDA resultatDA;
        ParametresDA parametreDA;
        SequenceDA sequenceDA;
        JournalDA journalDA;

        public GestionMoyennesSequentielleBL()
        {
            classeDA = new ClasseDA();
            eleveDA = new EleveDA();
            resultatDA = new ResultatDA();
            parametreDA = new ParametresDA();
            sequenceDA = new SequenceDA();
            journalDA = new JournalDA();
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

        public List<string> listerValeurColonneSequence(string p)
        {
            return sequenceDA.listerValeursColonne(p);
        }

        public SequenceBE rechercherSequence(SequenceBE sequence)
        {
            return sequenceDA.rechercher(sequence);
        }

        public List<ResultatBE> listerSuivantCritereResultatSequentiel(string codeclasse, string codesequence, int annee)
        {
            journalDA.journaliser("Génération de l'état des moyennes de la sequence " + codesequence + " de " + codeclasse);
            return resultatDA.listerResultatsSequentielsDesElevesDuneClasse(codeclasse, codesequence, annee);
        }

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }
    }
}
