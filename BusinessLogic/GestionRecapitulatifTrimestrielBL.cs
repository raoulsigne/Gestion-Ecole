using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.DataAccess;
using Ecole.BusinessEntity;
using Ecole.ClasseConception;

namespace Ecole.BusinessLogic
{

    class GestionRecapitulatifTrimestrielBL
    {
        ClasseDA classeDA;
        EleveDA eleveDA;
        MoyennesTrimestrielsDA moyennesDA;
        TrimestreDA trimestreDA;
        EnseignantDA enseignantDA;
        ParametresDA parametreDA;
        ProgrammerDA programmerDA;
        JournalDA journalDA;
        ResultatTrimestrielDA resultatDA;

        public GestionRecapitulatifTrimestrielBL()
        {
            classeDA = new ClasseDA();
            eleveDA = new EleveDA();
            moyennesDA = new MoyennesTrimestrielsDA();
            trimestreDA = new TrimestreDA();
            parametreDA = new ParametresDA();
            enseignantDA = new EnseignantDA();
            programmerDA = new ProgrammerDA();
            journalDA = new JournalDA();
            resultatDA = new ResultatTrimestrielDA();
        }

        public LigneRecapitulatif recapitulatifTrimestrielEleve(EleveBE eleve, string codeclasse, string codestrimestre, int annee)
        {
            journalDA.journaliser("Saisie du recapitulatif trimestriel du " +codestrimestre+ " de " + eleve.matricule);
            return moyennesDA.recapitulatifTrimestrielEleve(eleve, codeclasse, codestrimestre, annee);
        }

        public int anneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        public List<EleveBE> listeEleveDuneClasse(ClasseBE classe, int annee)
        {
            return classeDA.listeEleves(classe, annee);
        }

        public ClasseBE rechercherClasse(ClasseBE classe)
        {
            return classeDA.rechercher(classe);
        }

        public TrimestreBE rechercherTrimestre(TrimestreBE trimestre)
        {
            return trimestreDA.rechercher(trimestre);
        }

        public List<string> listerValeurColonneClasse(string p)
        {
            return classeDA.listerValeursColonne(p);
        }

        public List<string> listerValeurColonneTrimestre(string p)
        {
            return trimestreDA.listerValeursColonne(p);
        }

        public string obtenirProfTitulaire(string p, int annee)
        {
            string codeprof = classeDA.getCodeProfTitulaireDuneClasse(p, annee);
            EnseignantBE enseignant = new EnseignantBE();
            enseignant.codeProf = codeprof;
            enseignant = enseignantDA.rechercher(enseignant);

            if (enseignant != null)
                return enseignant.nomProf;
            else
                return "";
        }

        public List<string> listeCodeMatiereDuneClasse(string codeclasse, int annee)
        {
            return programmerDA.listeCodeMatiereDuneClasse(codeclasse, annee);
        }

        public List<string> listeCodeGroupeDuneClasse(string codeclasse, int annee)
        {
            return programmerDA.listeCodeGroupeDuneClasse(codeclasse, annee);
        }

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }

        internal double obtenirMoyenneClasse(string codeclasse, string codetrim, int annee)
        {
            return resultatDA.obtenirMoyenneTrimestrielleDuneClasse(codeclasse, codetrim, annee);
        }
    }

}
