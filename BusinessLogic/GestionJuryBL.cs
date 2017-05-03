using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.DataAccess;
using Ecole.BusinessEntity;
using Ecole.UI;

namespace Ecole.BusinessLogic
{
    public class GestionJuryBL
    {
        ClasseDA classeDA;
        SequenceDA sequenceDA;
        InscrireDA inscrireDA;
        ParametresDA parametreDA;
        EleveDA eleveDA;
        TrimestreDA trimestreDA;
        ResultatAnnuelDA resultatAnnuelDA;
        ResultatTrimestrielDA resultatTrimestrielDA;
        ResultatDA resultatDA;
        List<string> sequences;
        JournalDA journalDA;

        public GestionJuryBL()
        {
            classeDA = new ClasseDA();
            sequenceDA = new SequenceDA();
            inscrireDA = new InscrireDA();
            parametreDA = new ParametresDA();
            eleveDA = new EleveDA();
            trimestreDA = new TrimestreDA();
            resultatAnnuelDA = new ResultatAnnuelDA();
            resultatTrimestrielDA = new ResultatTrimestrielDA();
            resultatDA = new ResultatDA();
            journalDA = new JournalDA();
            sequences = new List<string>();

            sequences = this.listerValeurColonneSequence("codeseq");
        }

        internal int anneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        internal List<string> listerValeurColonneClasse(string p)
        {
            return classeDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneSequence(string p)
        {
            return sequenceDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneTrimestre(string p)
        {
            return trimestreDA.listerValeursColonne(p);
        }

        internal EleveBE rechercherEleve(EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
        }

        internal List<InscrireBE> listerSuivantCritereInscrire(string p)
        {
            return inscrireDA.listerSuivantCritere(p);
        }

        internal List<ResultatBE> listerSuivantCritereResultatSequentiel(string codeclasse, string codeseq, int annee)
        {
            return resultatDA.listerResultatsSequentielsDesElevesDuneClasse(codeclasse, codeseq, annee);
        }

        internal List<ResultatTrimestrielBE> listerSuivantCritereResultatTrimestriel(string codeclasse, string codetrimestre, int annee)
        {
            return resultatTrimestrielDA.listerResultatsTrimestrielDesElevesDuneClasse(codeclasse, codetrimestre, annee);
        }

        internal List<ResultatAnnuelBE> listerSuivantCritereResultatAnnuel(string codeclasse, int annee)
        {
            return resultatAnnuelDA.listerResultatsAnnuelsDesElevesDuneClasse(codeclasse, annee);
        }

        internal bool enregistrerRemarque(string matricule, int annee, string remarque, string jury)
        {
            if (jury == JuryUI.ANNUEL)
            {
                journalDA.journaliser("Enregistrement des remarques du jury annuel - "+matricule);
                return resultatAnnuelDA.enregistrerRemarque(matricule, annee, remarque);
            }
            else if (sequences.Contains(jury))
            {
                journalDA.journaliser("Enregistrement des remarques du jury sequentiel - " + matricule);
                return resultatDA.enregistrerRemarque(matricule, annee, jury, remarque);
            }
            else
            {
                journalDA.journaliser("Enregistrement des remarques du jury trimestriel - " + matricule);
                return resultatTrimestrielDA.enregistrerRemarque(matricule, annee, jury, remarque);
            }
        }


        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }
    }
}
