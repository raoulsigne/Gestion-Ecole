using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    class GestionStatistiqueBL
    {
        ParametresDA parametreDA;
        ClasseDA classeDA;
        SequenceDA sequenceDA;
        TrimestreDA trimestreDA;
        MoyennesDA moyenneDA;
        MoyennesTrimestrielsDA moyenneTrimestrielDA;
        MoyennesAnnuellesDA moyenneAnnuelleDA;

        public GestionStatistiqueBL()
        {
            parametreDA = new ParametresDA();
            classeDA = new ClasseDA();
            sequenceDA = new SequenceDA();
            trimestreDA = new TrimestreDA();
            moyenneDA = new MoyennesDA();
            moyenneAnnuelleDA = new MoyennesAnnuellesDA();
            moyenneTrimestrielDA = new MoyennesTrimestrielsDA();
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

        internal List<KeyValuePair<string, int>> effectifValidationMatiereAnnuelClasse(string codeclasse, int annee)
        {
            return moyenneAnnuelleDA.effectifValidationMatiereAnnuelClasse(codeclasse, annee);
        }

        internal List<KeyValuePair<string, int>> effectifValidationMatiereTrimestrielClasse(string codeclasse, int annee, string codetrimestre)
        {
            return moyenneTrimestrielDA.effectifValidationMatiereTrimestrielClasse(codeclasse, annee, codetrimestre);
        }

        internal List<KeyValuePair<string, int>> effectifValidationMatiereSequentielClasse(string codeclasse, int annee, string codesequence)
        {
            return moyenneDA.effectifValidationMatiereSequentielClasse(codeclasse, annee, codesequence);
        }

        internal ClasseBE rechercherClasse(ClasseBE classe)
        {
            return classeDA.rechercher(classe);
        }

        internal List<KeyValuePair<string, int>> effectifValidationResultatAnnuelClasse(int annee)
        {
            return moyenneAnnuelleDA.effectifValidationResultatAnnuelClasse(annee);
        }

        internal List<KeyValuePair<string, int>> effectifValidationResultatTrimestrielClasse(int annee, string codetrimestre)
        {
            return moyenneTrimestrielDA.effectifValidationResultatTrimestrielClasse(annee, codetrimestre);
        }

        internal List<KeyValuePair<string, int>> effectifValidationResultatSequentielClasse(int annee, string codesequence)
        {
            return moyenneDA.effectifValidationResultatSequentielClasse(annee, codesequence);
        }

        internal List<KeyValuePair<string, int>> effectifValidationResultatAnnuelNiveau(int annee)
        {
            return moyenneAnnuelleDA.effectifValidationResultatAnnuelNiveau(annee);
        }

        internal List<KeyValuePair<string, int>> effectifValidationResultatTrimestrielNiveau(int annee, string codetrimestre)
        {
            return moyenneTrimestrielDA.effectifValidationResultatTrimestrielNiveau(annee, codetrimestre);
        }

        internal List<KeyValuePair<string, int>> effectifValidationResultatSequentielNiveau(int annee, string codesequence)
        {
            return moyenneDA.effectifValidationResultatSequentielNiveau(annee, codesequence);
        }

        internal List<KeyValuePair<string, int>> progressionTrimestrielClasse(string codeclasse, int annee)
        {
            return moyenneTrimestrielDA.progressionTrimestrielClasse(codeclasse, annee);
        }

        internal List<KeyValuePair<string, int>> progressionSequentielClasse(string codeclasse, int annee)
        {
            return moyenneDA.progressionSequentielClasse(codeclasse, annee);
        }

        /**
         * cette fonction permet de recencer pour une classe le professeur titulaire, l'effectif le taux de reussite
         * */
        public Dictionary<string, string> syntheseClasse(string codeclasse, int annee, string periode)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            StatistiqueClasseBL statBL = new StatistiqueClasseBL();
            StatistiqueClasseBE statBE = new StatistiqueClasseBE();
            GestionRecapitulatifAnnuelBL recapAnnuel = new GestionRecapitulatifAnnuelBL();
            GestionRecapitulatifSequentielBL recapSeq = new GestionRecapitulatifSequentielBL();
            GestionRecapitulatifTrimestrielBL recapTrim = new GestionRecapitulatifTrimestrielBL();
            List<string> sequences = listerValeurColonneSequence("codeseq");
            List<string> trimestres = listerValeurColonneTrimestre("codetrimestre");
            double moyenne = 0;
            string prof = "";
            
            if (sequences.Contains(periode))
            {
                moyenne = recapSeq.obtenirMoyenneClasse(codeclasse, periode, annee);
                prof = recapSeq.obtenirProfTitulaire(codeclasse, annee);
                statBE = statBL.getStatistiqueDuneSequence(codeclasse, annee, periode);
            }
            else if (trimestres.Contains(periode))
            {
                moyenne = recapTrim.obtenirMoyenneClasse(codeclasse, periode, annee);
                prof = recapTrim.obtenirProfTitulaire(codeclasse, annee);
                statBE = statBL.getStatistiqueDunTrimestre(codeclasse, annee, periode);
            }
            else
            {
                moyenne = recapAnnuel.obtenirMoyenneClasse(codeclasse, annee);
                prof = recapAnnuel.obtenirProfTitulaire(codeclasse, annee);
                statBE = statBL.getStatistiqueDuneAnnee(codeclasse, annee);
            }

            result.Add("prof", prof);
            result.Add("effectif", statBE.effectif.ToString());
            result.Add("moyenne", moyenne.ToString());
            result.Add("taux", statBE.pourcentageAdmis.ToString());
                        
            return result;
        }
    }
}
