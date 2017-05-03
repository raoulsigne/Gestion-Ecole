using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Globalization;
using System.Threading;

using Ecole.DataAccess;
using Ecole.BusinessEntity;
using Ecole.ClasseConception;
using Ecole.Utilitaire;
using Ecole.UI;

namespace Ecole.BusinessLogic
{
    class GenererBulletinsAnnuelBL
    {
        private MoyennesTrimestrielsDA moyennesTrimestrielsDA;
        private ClasseDA classeDA;
        private SequenceDA sequenceDA;
        private TrimestreDA trimestreDA;
        private EvaluerDA evaluerDA;
        private NotesDA notesDA;
        private ProgrammerDA programmerDA;
        private ResultatDA resultatDA;
        private ResultatTrimestrielDA resultatTrimestrielDA;
        private ResultatAnnuelDA resultatAnnuelDA;
        private MentionDA mentionDA;
        private ParametresDA parametresDA;
        private EleveDA eleveDA;
        private EnseignantDA enseignantDA;
        private SanctionnerDA sanctionnerDA;
        private JournalDA journalDA;

        public GenererBulletinsAnnuelBL()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;


            this.resultatTrimestrielDA = new ResultatTrimestrielDA();
            this.resultatAnnuelDA = new ResultatAnnuelDA();
            this.moyennesTrimestrielsDA = new MoyennesTrimestrielsDA();
            this.classeDA = new ClasseDA();
            this.sequenceDA = new SequenceDA();
            this.trimestreDA =  new TrimestreDA();
            this.evaluerDA = new EvaluerDA();
            this.notesDA = new NotesDA();
            this.programmerDA = new ProgrammerDA();
            this.resultatDA = new ResultatDA();
            this.mentionDA = new MentionDA();
            this.parametresDA = new ParametresDA();
            eleveDA = new EleveDA();
            this.enseignantDA = new EnseignantDA();
            this.sanctionnerDA = new SanctionnerDA();
            this.journalDA = new JournalDA();
        }


        // rechercher une Moyenne Trimestriel
        public MoyennesTrimestrielsBE rechercherMoyenneTrimestriel(MoyennesTrimestrielsBE moyenne)
        {
            return moyennesTrimestrielsDA.rechercher(moyenne);
        }


        // rechercher un Resultat
        public ResultatBE rechercherResultat(ResultatBE resultat)
        {
            return resultatDA.rechercher(resultat);
        }

       
        // rechercher une Classe
        public ClasseBE rechercherClasse(ClasseBE classe)
        {
            return classeDA.rechercher(classe);
        }

        // rechercher une Sequence
        public SequenceBE rechercherSequence(SequenceBE sequence)
        {
            return sequenceDA.rechercher(sequence);
        }

        // rechercher un Trimestre
        public TrimestreBE rechercherTrimestre(TrimestreBE trimestre)
        {
            return trimestreDA.rechercher(trimestre);
        }

        //lister toutes les Moyennes Trimestriels
        public List<MoyennesTrimestrielsBE> listerToutesLesMoyennestrimestriels()
        {
            return moyennesTrimestrielsDA.listerTous();
        }

        // lister toutes les Moyennes trimestrielsrespectant un certain critère
        public List<MoyennesTrimestrielsBE> listerMoyennesTrimestrielsSuivantCritere(string critere)
        {
            return moyennesTrimestrielsDA.listerSuivantCritere(critere);
        }

        //lister toutes les Classes
        public List<ClasseBE> listerToutesLesClasses()
        {
            return classeDA.listerTous();
        }

        // lister toutes les Classes respectant un certain critère
        public List<ClasseBE> listerClassesSuivantCritere(string critere)
        {
            return classeDA.listerSuivantCritere(critere);
        }

        //lister toutes les Sequences
        public List<SequenceBE> listerToutesLesSequences()
        {
            return sequenceDA.listerTous();
        }

        // lister toutes les Sequences respectant un certain critère
        public List<SequenceBE> listerSequencesSuivantCritere(string critere)
        {
            return sequenceDA.listerSuivantCritere(critere);
        }

        //lister toutes les Trimestres
        public List<TrimestreBE> listerTousLesTrimestres()
        {
            return trimestreDA.listerTous();
        }

        // lister tous les Trimestres respectant un certain critère
        public List<TrimestreBE> listerTrimestresSuivantCritere(string critere)
        {
            return trimestreDA.listerSuivantCritere(critere);
        }

        //lister tous les Resultats
        public List<ResultatBE> listerTousLesResultats()
        {
            return resultatDA.listerTous();
        }

        // lister tous les Resultats respectant un certain critère
        public List<ResultatBE> listerResultatsSuivantCritere(string critere)
        {
            return resultatDA.listerSuivantCritere(critere);
        }

        // retourne l'année du système
        public int getAnneeEnCours()
        {
            return parametresDA.AnneeEnCours();
        }

        // retourne la liste des codes de Classe deja enregistré (pour le filtre)
        public List<string> getListCodeClasse(List<ClasseBE> listClasse)
        {
            List<string> listeCodeClasse = new List<string>();

            listeCodeClasse = new List<string>();
            if (listClasse != null)
            {
                for (int i = 0; i < listClasse.Count; i++)
                {
                    listeCodeClasse.Add(listClasse.ElementAt(i).codeClasse);
                }
                //listeCodeCycle.Add("Tous");
                return listeCodeClasse;
            }
            else return null;
        }

        // retourne la liste des codes de Classe deja enregistré (pour le filtre)
        public List<string> getListCodeClasse2(List<ClasseBE> listClasse)
        {
            List<string> listeCodeClasse = new List<string>();

            listeCodeClasse = new List<string>();
            listeCodeClasse.Add("<Toutes Les Classes>");
            if (listClasse != null)
            {
                for (int i = 0; i < listClasse.Count; i++)
                {
                    listeCodeClasse.Add(listClasse.ElementAt(i).codeClasse);
                }
                //listeCodeCycle.Add("Tous");
                return listeCodeClasse;
            }
            else return null;
        }

        // retourne la liste des codes de Séquence deja enregistré (pour le filtre)
        public List<string> getListCodeSequence(List<SequenceBE> listSequence)
        {
            List<string> listeCodeSequence = new List<string>();

            listeCodeSequence = new List<string>();
            if (listSequence != null)
            {
                for (int i = 0; i < listSequence.Count; i++)
                {
                    listeCodeSequence.Add(listSequence.ElementAt(i).codeseq);
                }
                return listeCodeSequence;
            }
            else return null;
        }

        // retourne la liste des codes de Trimestre deja enregistré (pour le filtre)
        public List<string> getListCodeTrimestre(List<TrimestreBE> listTrimestre)
        {
            List<string> listeCodeTrimestre = new List<string>();

            listeCodeTrimestre = new List<string>();
            if (listTrimestre != null)
            {
                for (int i = 0; i < listTrimestre.Count; i++)
                {
                    listeCodeTrimestre.Add(listTrimestre.ElementAt(i).codetrimestre);
                }
                return listeCodeTrimestre;
            }
            else return null;
        }

        // retourne la liste des codes de Trimestre deja enregistré (pour le filtre)
        public List<string> getListCodeTrimestre2(List<TrimestreBE> listTrimestre)
        {
            List<string> listeCodeTrimestre = new List<string>();

            listeCodeTrimestre = new List<string>();
            listeCodeTrimestre.Add("<Tous Les Trimestres>");
            if (listTrimestre != null)
            {
                for (int i = 0; i < listTrimestre.Count; i++)
                {
                    listeCodeTrimestre.Add(listTrimestre.ElementAt(i).codetrimestre);
                }
                return listeCodeTrimestre;
            }
            else return null;
        }

        // liste les matières d'une classe
        public List<MatiereBE> listeDesMatieresDuneClasse(ClasseBE classe, int annee) {
            return classeDA.ListeMatiereDuneClasse(classe, annee);
        }

        //lister les élèves  inscrit à une classe pour une année
        public List<EleveBE> ListeDesElevesDuneClasse(ClasseBE classe, int annee)
        {
            return classeDA.listeEleves(classe, annee);
        }

       
        //retourne le coefficient d'une matière pour une classe et pour une année donnée
        public int getCoefficientMatiere(String codeClasse, String codeMatiere, int annee) {
            List<ProgrammerBE> LProgrammer = programmerDA.listerSuivantCritere("codeclasse = '"+codeClasse+"' AND codeMat = '"+codeMatiere+"' AND annee = '"+annee+"'");
            if (LProgrammer != null && LProgrammer.Count != 0)
                return LProgrammer.ElementAt(0).coef;
            else return 0;
        }

        //obtenir la mention d'un élève, connaissant son résultat
        public String getMention(Double moyenne){
            String mention = mentionDA.getMention(moyenne);
            return mention;
        }

        //fonction qui liste les élèves d'une classe
        // liste les élèves d'une classe à une année
        public List<EleveBE> listeEleves(ClasseBE classe, int annee) {
            return classeDA.listeEleves(classe, annee);
        }

        //méthode qui génère le bulletin Annuel d'un élève
        public void genererBulletinAnnuelDunEleve(String matricule, int annee, String codeClasse, string photo) {

            List<LigneBulletinAnnuel> ListLigneBulletinAnnuel = resultatAnnuelDA.genererBulletinAnnuelDunEleve(matricule, annee, codeClasse);


            BulletinAnnuel bulletinAnnuel = new BulletinAnnuel();
            EleveBE elv = new EleveBE();
            elv.matricule = matricule;

            bulletinAnnuel.eleve = eleveDA.rechercher(elv);
            CreerEtat etat = new CreerEtat();
            string nom;
            if (bulletinAnnuel.eleve.nom.Length > 50)
                nom = bulletinAnnuel.eleve.nom.Substring(0, 49);
            else
                nom = bulletinAnnuel.eleve.nom;
            etat.docname = ConnexionUI.DOSSIER_BULLETINS + annee + "-" + codeClasse + "-" + nom + ".pdf";
            etat.title = "BULLETIN ANNUEL";

            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;

            bulletinAnnuel.classe = classeDA.rechercher(classe);

            bulletinAnnuel.annee = annee;

            //on recherche le nombre de trimestre de l'année
            bulletinAnnuel.nbTrimestre = trimestreDA.getNombreTrimestre();

            //on recherche le nombre de séquence de l'année
            bulletinAnnuel.nbSequence = sequenceDA.getNombreSequence();

            bulletinAnnuel.listLigneBulletinAnnuel = ListLigneBulletinAnnuel;
            //on liste les résultats Annuels de l'élève pour l'année
            List<ResultatAnnuelBE> ListResultatsAnnuel = resultatAnnuelDA.listerSuivantCritere("annee = '" + annee + "' AND matricule = '" + matricule + "'");

            if (ListResultatsAnnuel != null && ListResultatsAnnuel.Count != 0)
                bulletinAnnuel.resultatannuel = ListResultatsAnnuel.ElementAt(0);

            //on charge les moyennes Annuelles min et max des élèves de la classe choisit
            bulletinAnnuel.moyenneMin = resultatAnnuelDA.getMoynenneAnnuelleMinimaleDuneClasse(codeClasse, annee);
            bulletinAnnuel.moyenneMax = resultatAnnuelDA.getMoynenneAnnuelleMaximaleDuneClasse(codeClasse, annee);

            //on Charge la liste des code de trimestre de l'année
            //List<TrimestreBE> LTrimestre = trimestreDA.listerTous();
            List<String> ListCodeSequence = sequenceDA.getListCodeSequence(); 
            //List<String> ListCodeTrimestre = getListCodeTrimestre(LTrimestre); 

            //************************ on charge les disciplines de l'élève

            // on recherche l'effectif de la classe
            int effectifClasse = classeDA.getEffectifClasse(codeClasse, annee);

            //on recherche le professeur titulaire de la classe
            String codeProf = classeDA.getCodeProfTitulaireDuneClasse(codeClasse, annee);

            EnseignantBE profTitulaire = new EnseignantBE();
            if (codeProf != null)
            {
                profTitulaire.codeProf = codeProf;
                profTitulaire = enseignantDA.rechercher(profTitulaire);
            }

            //on charge les infos sur les paramètres
            ParametresBE parametre = parametresDA.getParametre();

            //************************ on charge les disciplines de l'élève
            bulletinAnnuel.ListSanction = sanctionnerDA.getListSanctionAnuelleEleve(matricule, annee);

            etat.etatBulletinAnnuelEleve(bulletinAnnuel, ListCodeSequence, effectifClasse, profTitulaire, parametre, codeClasse, photo);

            journalDA.journaliser("génération du bulletin annuel ("+annee+") de l'élève de matricule " + matricule);
        }





        internal EleveBE rechercherEleve(EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
        }
    }
}
