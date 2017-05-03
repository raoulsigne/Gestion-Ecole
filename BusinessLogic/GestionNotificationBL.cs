using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;
using Ecole.Utilitaire;
using Ecole.UI;

namespace Ecole.BusinessLogic
{
    public class GestionNotificationBL
    {
        public static string INFORMATIONS_NON_VALIDEES = "Informations non encore validées";
        public static string INFORMATIONS_VALIDEES = "Informations validées, cliquz à présent sur ENVOYER";

        EleveDA eleveDA;
        EnseignantDA enseignantDA;
        ClasseDA classeDA;
        ParametresDA parametreDA;
        SequenceDA sequenceDA;
        TrimestreDA trimestreDA;
        CycleDA cycleDA;
        NiveauDA niveauDA;
        SerieDA serieDA;
        Tools tools;

        public GestionNotificationBL()
        {
            eleveDA = new EleveDA();
            enseignantDA = new EnseignantDA();
            classeDA = new ClasseDA();
            parametreDA = new ParametresDA();
            sequenceDA = new SequenceDA();
            trimestreDA = new TrimestreDA();
            serieDA = new SerieDA();
            niveauDA = new NiveauDA();
            cycleDA = new CycleDA();
            tools = new Tools(parametreDA.getParametre().REPERTOIRE_PHOTO);
        }

        internal List<EleveBE> listerElevesDuneClasse(string codeclasse, int annee)
        {
            ClasseBE c = new ClasseBE();
            c.codeClasse = codeclasse;
            c = classeDA.rechercher(c);
            return classeDA.listeEleves(c, annee);
        }

        internal EleveBE rechercherEleve(EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
        }

        internal int anneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        internal EnseignantBE rechercherEnseignant(EnseignantBE enseignant)
        {
            return enseignantDA.rechercher(enseignant);
        }

        internal List<EnseignantBE> listerToutEnseignants()
        {
            return enseignantDA.listerTous();
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

        public Dictionary<string, string> contactParent(string matricule)
        {
            return eleveDA.contactParent(matricule);
        }

        public Dictionary<string, string> contactParents(string codeclasse)
        {
            return eleveDA.contactParents(codeclasse);
        }

        public Dictionary<string, string> contactEnseignant(string codeprof)
        {
            return enseignantDA.contactEnseignant(codeprof);
        }

        public Dictionary<string, string> contactEnseignants()
        {
            return enseignantDA.contactEnseignants();
        }

        internal List<string> listerValeurColonneSerie(string p)
        {
            return serieDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneCycle(string p)
        {
            return cycleDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneNiveau(string p)
        {
            return niveauDA.listerValeursColonne(p);
        }

        internal ParametresBE getParametre()
        {
            return parametreDA.getParametre();
        }

        internal List<EleveBE> listeEleveDuneAnnee(int annee)
        {
            return eleveDA.listeEleveDuneAnnee(annee);
        }

        internal List<EleveBE> listeEleveDuneClasse(string classe, int annee)
        {
            return eleveDA.listeEleveDuneClasse(classe, annee);
        }

        internal ResultatBE resultatSequentielleEleve(string matricule, int annee, string seq)
        {
            ResultatDA resultatDA = new ResultatDA();
            return resultatDA.resultatSequentielleEleve(matricule, annee, seq);
        }

        internal ResultatTrimestrielBE resultatTrimestrielEleve(string mat, int annee, string trimestre)
        {
            ResultatTrimestrielDA resultat = new ResultatTrimestrielDA();
            return resultat.resultatTrimestrielEleve(mat, annee, trimestre);
        }

        internal ResultatAnnuelBE resultatAnnuelDunEleve(string matricule, int annee)
        {
            ResultatAnnuelDA resultat = new ResultatAnnuelDA();
            return resultat.resultatAnnuelDunEleve(matricule, annee);
        }

        internal List<EleveBE> listeEleveDuneSerie(string code, int annee)
        {
            return eleveDA.listeEleveDuneSerie(code, annee);
        }

        internal List<EleveBE> listeEleveDunCycle(string codecycle, int annee)
        {
            return eleveDA.listeEleveDunCycle(codecycle, annee);
        }

        internal List<EleveBE> listeEleveDunNiveau(string codeniveau, int annee)
        {
            return eleveDA.listeEleveDunNiveau(codeniveau, annee);
        }

        public Dictionary<string, int> envoiSMS(string[] adresse, string message)
        {
            return tools.sendSMS(adresse, message);
        }

        internal Dictionary<string, int> envoiSMS(List<string[]> destinataireSMS)
        {
            return tools.sendSMS(destinataireSMS);
        }

        internal Dictionary<string, int> envoiSMS(List<string[]> destinataireSMS, string message)
        {
            return tools.sendSMS(destinataireSMS, message);
        }
    }
}
