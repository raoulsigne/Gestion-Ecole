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
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Ecole.BusinessLogic
{
    class GenererBulletinsSequentielBL
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
        private MentionDA mentionDA;
        private ParametresDA parametresDA;
        private EleveDA eleveDA;
        private EnseignantDA enseignantDA;
        private SanctionnerDA sanctionnerDA;
        private DisciplineDA disciplineDA;
        private JournalDA journalDA;

        public GenererBulletinsSequentielBL()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;


            this.resultatTrimestrielDA = new ResultatTrimestrielDA();
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
            this.disciplineDA = new DisciplineDA();
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

        // retourne la liste des codes de Séquence deja enregistré (pour le filtre)
        public List<string> getListCodeSequence2(List<SequenceBE> listSequence)
        {
            List<string> listeCodeSequence = new List<string>();

            listeCodeSequence = new List<string>();
            listeCodeSequence.Add("<Toutes Les Séquences>");
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

        //méthode qui génère le bulletin séquentiel d'un élève
        public void genererBulletinSequentielDunEleve(String matricule, int annee, String codeClasse, String codeSequence, string photo)
        {

            List<LigneBulletinSequentiel> ListLigneBulletinSequentiel = resultatDA.genererBulletinSequentielDunEleve(matricule, annee, codeClasse, codeSequence);
            SequenceBE sequence = new SequenceBE();
            sequence.codeseq = codeSequence;
            sequence = sequenceDA.rechercher(sequence);

            BullettinSequentiel bulletinSequentiel = new BullettinSequentiel();
            EleveBE elv = new EleveBE();
            elv.matricule = matricule;

            bulletinSequentiel.eleve = eleveDA.rechercher(elv);

            CreerEtat etat = new CreerEtat();
            string nom;
            if (bulletinSequentiel.eleve.nom.Length > 50)
                nom = bulletinSequentiel.eleve.nom.Substring(0, 49);
            else
                nom = bulletinSequentiel.eleve.nom;
            etat.docname = ConnexionUI.DOSSIER_BULLETINS + annee + "-" + codeClasse + "-" + codeSequence + "-" + nom + ".pdf";
            etat.title = "BULLETIN DE NOTES DE LA " + sequence.nomseq;

            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;

            bulletinSequentiel.classe = classeDA.rechercher(classe);

            bulletinSequentiel.annee = annee;

            //on recherche le nombre d'évaluation de la séquence
            bulletinSequentiel.nbEvaluation = sequenceDA.getNombreEvaluationDuneSequence(codeClasse, codeSequence, annee);
            bulletinSequentiel.listLigneBulletinSequentiel = ListLigneBulletinSequentiel;
            //on liste les résultats trimestriels de l'élève pour l'année
            List<ResultatBE> ListResultatsSequentiel = resultatDA.listerSuivantCritere("annee = '" + annee + "' AND codeSeq = '" + codeSequence + "' AND matricule = '" + matricule + "'");

            if (ListResultatsSequentiel != null && ListResultatsSequentiel.Count != 0)
                bulletinSequentiel.resultatSequentiel = ListResultatsSequentiel.ElementAt(0);

            //on charge les moyennes séquentielles min et max des élèves de la classe choisit
            bulletinSequentiel.moyenneMin = resultatDA.getMoynenneSequentielleMinimaleDuneClasse(codeClasse, codeSequence, annee);
            bulletinSequentiel.moyenneMax = resultatDA.getMoynenneSequentielleMaximaleDuneClasse(codeClasse, codeSequence, annee);

            //on Charge la liste des codes d'évaluation de la séquence
            List<String> ListCodeEvaluation = sequenceDA.getListCodeEvaluationDuneSequence(codeClasse, codeSequence, annee); 

            

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

            //on appelle la fonction qui défini les avertissements et les blâmes de l'élève
            DefinirLesAvertissementsEtBlamesDunEleve(matricule, codeSequence, annee);

            //************************ on charge les disciplines de l'élève
            bulletinSequentiel.ListSanction = sanctionnerDA.getListSanctionSequentielleEleve(matricule, annee, codeSequence);

            etat.etatBulletinSequentielEleve(bulletinSequentiel, ListCodeEvaluation, effectifClasse, profTitulaire, parametre, photo);

            journalDA.journaliser("génération du bulletin séquentiel (Séquence "+ codeSequence+") de l'élève de matricule " + matricule);

        }

        public void genererBulletinSequentielDunEleve(Document doc, CreerEtat etat, PdfWriter writer, String matricule, int annee, String codeClasse, String codeSequence, string photo)
        {

            List<LigneBulletinSequentiel> ListLigneBulletinSequentiel = resultatDA.genererBulletinSequentielDunEleve(matricule, annee, codeClasse, codeSequence);
            SequenceBE sequence = new SequenceBE();
            sequence.codeseq = codeSequence;
            sequence = sequenceDA.rechercher(sequence);

            BullettinSequentiel bulletinSequentiel = new BullettinSequentiel();
            EleveBE elv = new EleveBE();
            elv.matricule = matricule;

            bulletinSequentiel.eleve = eleveDA.rechercher(elv);

            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;

            bulletinSequentiel.classe = classeDA.rechercher(classe);

            bulletinSequentiel.annee = annee;

            //on recherche le nombre d'évaluation de la séquence
            bulletinSequentiel.nbEvaluation = sequenceDA.getNombreEvaluationDuneSequence(codeClasse, codeSequence, annee);
            bulletinSequentiel.listLigneBulletinSequentiel = ListLigneBulletinSequentiel;
            //on liste les résultats trimestriels de l'élève pour l'année
            List<ResultatBE> ListResultatsSequentiel = resultatDA.listerSuivantCritere("annee = '" + annee + "' AND codeSeq = '" + codeSequence + "' AND matricule = '" + matricule + "'");

            if (ListResultatsSequentiel != null && ListResultatsSequentiel.Count != 0)
                bulletinSequentiel.resultatSequentiel = ListResultatsSequentiel.ElementAt(0);

            //on charge les moyennes séquentielles min et max des élèves de la classe choisit
            bulletinSequentiel.moyenneMin = resultatDA.getMoynenneSequentielleMinimaleDuneClasse(codeClasse, codeSequence, annee);
            bulletinSequentiel.moyenneMax = resultatDA.getMoynenneSequentielleMaximaleDuneClasse(codeClasse, codeSequence, annee);

            //on Charge la liste des codes d'évaluation de la séquence
            List<String> ListCodeEvaluation = sequenceDA.getListCodeEvaluationDuneSequence(codeClasse, codeSequence, annee);



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

            //on appelle la fonction qui défini les avertissements et les blâmes de l'élève
            DefinirLesAvertissementsEtBlamesDunEleve(matricule, codeSequence, annee);

            //************************ on charge les disciplines de l'élève
            bulletinSequentiel.ListSanction = sanctionnerDA.getListSanctionSequentielleEleve(matricule, annee, codeSequence);

            etat.etatBulletinSequentielEleve(doc, writer, bulletinSequentiel, ListCodeEvaluation, effectifClasse, profTitulaire, parametre, photo);

            journalDA.journaliser("génération du bulletin séquentiel (Séquence " + codeSequence + ") de l'élève de matricule " + matricule);

        }

        //fonction qui défini les avertissements et les blâmes d'un élève en fontion de son nbre d'heure d'absence
        /*
         * avec un nbre d'absence injustifiées dans l'intervalle [7h, 15h[, l'élève à un avertissement
         * avec un nbre d'absence injustifiées dans l'intervalle [15h, 30h[, l'élève à une blâme
         */
        public void DefinirLesAvertissementsEtBlamesDunEleve(String matricule, String codeSequence, int annee) {
            //on liste les sanctions de l'élève
            List<SanctionnerBE> ListSanction = sanctionnerDA.getListSanctionSequentielleEleve(matricule, annee, codeSequence);

            int nbAbsenceNonJustifiees = 0; // le nombre d"absence non justifiées de l'élève
            int nbAbsenceJustifiees = 0; // le nombre d"absence non justifiées de l'élève
            for (int i = 0; i < ListSanction.Count; i++) { 
                DisciplineBE discipline = new DisciplineBE();
                discipline.codeSanction = ListSanction.ElementAt(i).codesanction;
                discipline = disciplineDA.rechercher(discipline);
                if (discipline.nomSanction.ToLower().Contains("absence") && ListSanction.ElementAt(i).etat.Equals("NON JUSTIFIEE"))
                    nbAbsenceNonJustifiees = nbAbsenceNonJustifiees + ListSanction.ElementAt(i).quantité;
                else if (discipline.nomSanction.ToLower().Contains("absence") && ListSanction.ElementAt(i).etat.Equals("JUSTIFIEE"))
                    nbAbsenceJustifiees = nbAbsenceJustifiees + ListSanction.ElementAt(i).quantité;
            }

            nbAbsenceNonJustifiees = nbAbsenceNonJustifiees - nbAbsenceJustifiees;

            //********************************** Si on lui avait deja defini des avertissement / blâmes / blame, on les supprimes dabord
            SanctionnerBE s2 = new SanctionnerBE();
            DisciplineBE discipline2 = disciplineDA.rechercherByNom("avertissement");
            if (discipline2 != null)
            {
                s2.codesanction = discipline2.codeSanction;
                s2.matricule = matricule;
                s2.annee = annee;
                s2.quantité = 1;
                s2.datesanction = System.DateTime.Today.Date;
                s2.sequence = codeSequence;
                s2.etat = "NON JUSTIFIEE";

                if (testSiDejaDefiniBlameOuAvertissement(s2))
                    sanctionnerDA.supprimerSuivantTouslesCriteresSaufLaDate(s2);
            }

            s2 = new SanctionnerBE();
                discipline2 = disciplineDA.rechercherByNom("blame");
                if (discipline2 != null)
                {
                    s2.codesanction = discipline2.codeSanction;
                    s2.matricule = matricule;
                    s2.annee = annee;
                    s2.quantité = 1;
                    s2.datesanction = System.DateTime.Today.Date;
                    s2.sequence = codeSequence;
                    s2.etat = "NON JUSTIFIEE";

                    if (testSiDejaDefiniBlameOuAvertissement(s2))
                        sanctionnerDA.supprimerSuivantTouslesCriteresSaufLaDate(s2);
                }

                discipline2 = disciplineDA.rechercherByNom("blâme");

                if (discipline2 != null)
                {
                    s2.codesanction = discipline2.codeSanction;
                    s2.matricule = matricule;
                    s2.annee = annee;
                    s2.quantité = 1;
                    s2.datesanction = System.DateTime.Today.Date;
                    s2.sequence = codeSequence;
                    s2.etat = "NON JUSTIFIEE";

                    if (testSiDejaDefiniBlameOuAvertissement(s2))
                        sanctionnerDA.supprimerSuivantTouslesCriteresSaufLaDate(s2);
                }

                //********************FIN suppression des avertissement / blâmes / blame précédemment enregistrées

            if (nbAbsenceNonJustifiees >= 7 && nbAbsenceNonJustifiees < 15) { 
                //on ajoute un avertissement à l'élève
                SanctionnerBE s = new SanctionnerBE();
                DisciplineBE discipline = disciplineDA.rechercherByNom("avertissement");
                if (discipline != null)
                {
                    s.codesanction = discipline.codeSanction;
                    s.matricule = matricule;
                    s.annee = annee;
                    s.quantité = 1;
                    s.datesanction = System.DateTime.Today.Date;
                    s.sequence = codeSequence;
                    s.etat = "NON JUSTIFIEE";

                    if (testSiDejaDefiniBlameOuAvertissement(s))
                        sanctionnerDA.supprimerSuivantTouslesCriteresSaufLaDate(s);
                    sanctionnerDA.ajouter(s);
                }
            }
            else if (nbAbsenceNonJustifiees >= 15 && nbAbsenceNonJustifiees < 30) {
                //on ajoute une blâme à l'élève
                SanctionnerBE s = new SanctionnerBE();
                DisciplineBE discipline = disciplineDA.rechercherByNom("blame");
                if (discipline != null)
                {
                    s.codesanction = discipline.codeSanction;
                    s.matricule = matricule;
                    s.annee = annee;
                    s.quantité = 1;
                    s.datesanction = System.DateTime.Today.Date;
                    s.sequence = codeSequence;
                    s.etat = "NON JUSTIFIEE";

                    if (testSiDejaDefiniBlameOuAvertissement(s))
                        sanctionnerDA.supprimerSuivantTouslesCriteresSaufLaDate(s);
                    sanctionnerDA.ajouter(s);
                }
                else {
                    discipline = disciplineDA.rechercherByNom("blâme");

                    if (discipline != null)
                    {
                        s.codesanction = discipline.codeSanction;
                        s.matricule = matricule;
                        s.annee = annee;
                        s.quantité = 1;
                        s.datesanction = System.DateTime.Today.Date;
                        s.sequence = codeSequence;
                        s.etat = "NON JUSTIFIEE";

                        if (testSiDejaDefiniBlameOuAvertissement(s))
                            sanctionnerDA.supprimerSuivantTouslesCriteresSaufLaDate(s);
                        sanctionnerDA.ajouter(s);
                    }
                }

                //on ajoute un avertissement à l'élève
                 s = new SanctionnerBE();
                 discipline = disciplineDA.rechercherByNom("avertissement");
                if (discipline != null)
                {
                    s.codesanction = discipline.codeSanction;
                    s.matricule = matricule;
                    s.annee = annee;
                    s.quantité = 1;
                    s.datesanction = System.DateTime.Today.Date;
                    s.sequence = codeSequence;
                    s.etat = "NON JUSTIFIEE";

                    if (testSiDejaDefiniBlameOuAvertissement(s))
                        sanctionnerDA.supprimerSuivantTouslesCriteresSaufLaDate(s);
                    sanctionnerDA.ajouter(s);
                }

            }
        }

        //fonction qui teste si une blame ou un avertissement à déja été défini pour un élève
        public bool testSiDejaDefiniBlameOuAvertissement(SanctionnerBE sanction){
            SanctionnerDA sanctionDA = new SanctionnerDA();
            SanctionnerBE s = sanctionDA.rechercherSuivantTousLesChampsSaufLaDate(sanction);

            if (s != null) {
                return true;
            }
            else return false;
        }


        internal EleveBE rechercherEleve(EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
        }
    }
}
