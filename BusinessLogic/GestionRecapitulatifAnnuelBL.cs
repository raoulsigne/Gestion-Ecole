using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.DataAccess;
using Ecole.BusinessEntity;
using Ecole.ClasseConception;

namespace Ecole.BusinessLogic
{
    class GestionRecapitulatifAnnuelBL
    {
        ClasseDA classeDA;
        EleveDA eleveDA;
        MoyennesAnnuellesDA moyennesDA;
        EnseignantDA enseignantDA;
        ParametresDA parametreDA;
        JournalDA journalDA;
        ProgrammerDA programmerDA;
        ResultatAnnuelDA resultatDA;

        public GestionRecapitulatifAnnuelBL()
        {
            classeDA = new ClasseDA();
            eleveDA = new EleveDA();
            moyennesDA = new MoyennesAnnuellesDA();
            parametreDA = new ParametresDA();
            enseignantDA = new EnseignantDA();
            journalDA = new JournalDA();
            programmerDA = new ProgrammerDA();
            resultatDA = new ResultatAnnuelDA();
        }

        public LigneRecapitulatif recapitulatifAnnuelEleve(EleveBE eleve, string codeclasse, int annee)
        {
            journalDA.journaliser("Saisie du recapitulatif annuel de " + eleve.matricule);
            return moyennesDA.recapitulatifAnnuelEleve(eleve, codeclasse, annee);
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

        public List<string> listerValeurColonneClasse(string p)
        {
            return classeDA.listerValeursColonne(p);
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

        internal double obtenirMoyenneClasse(string p, int annee)
        {
            return resultatDA.obtenirMoyenneAnnuelleDuneClasse(p, annee);
        }
    }
}
