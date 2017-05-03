using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class GenererResultatsDunEleveBL
    {
        private MoyennesDA moyennesDA;
        private MoyennesTrimestrielsDA moyennesTrimestrielsDA;
        private MoyennesAnnuellesDA moyennesAnnuellesDA;
        private ClasseDA classeDA;
        private SequenceDA sequenceDA;
        private EvaluerDA evaluerDA;
        private NotesDA notesDA;
        private ProgrammerDA programmerDA;
        private ResultatDA resultatDA;
        private ResultatTrimestrielDA resultatTrimestrielDA;
        private ResultatAnnuelDA resultatAnnuelDA;
        private MentionDA mentionDA;
        private ParametresDA parametresDA;
        private EleveDA eleveDA;
        private TrimestreDA trimestreDA;
        private JournalDA journalDA;

        public GenererResultatsDunEleveBL()
        {
            this.moyennesDA = new MoyennesDA();
            this.classeDA = new ClasseDA();
            this.sequenceDA = new SequenceDA();
            this.evaluerDA = new EvaluerDA();
            this.notesDA = new NotesDA();
            this.programmerDA = new ProgrammerDA();
            this.resultatDA = new ResultatDA();
            this.resultatTrimestrielDA = new ResultatTrimestrielDA();
            this.resultatAnnuelDA = new ResultatAnnuelDA();
            this.mentionDA = new MentionDA();
            this.parametresDA = new ParametresDA();
            this.eleveDA = new EleveDA();
            this.trimestreDA = new TrimestreDA();
            this.moyennesTrimestrielsDA = new MoyennesTrimestrielsDA();
            this.moyennesAnnuellesDA = new MoyennesAnnuellesDA();
            this.journalDA = new JournalDA();
        }

        //creer une Moyenne
        public bool creerMoyenne(String codeMatiere, String codeSequence, String matricule, Double moyenne, int annee, int rang, double moyenneClasse, string mention, double moyenneMin, double moyenneMax, string appreciation)
        {
            MoyennesBE moy = new MoyennesBE(codeMatiere, codeSequence, matricule, moyenne, annee, rang, moyenneClasse, mention, moyenneMin, moyenneMax, appreciation);
            if (moyennesDA.ajouter(moy))
            {
                journalDA.journaliser("enregistrement de la moyenne séquentielle de l'élève de matricule " + matricule + ". matière : " + codeMatiere + ", année : " + annee + ", séquence : "+codeSequence+", moyenne : " + moyenne);
                return true;
            }
            return false;
        }

        // supprimer une Moyenne
        public bool supprinerMoyenne(MoyennesBE moyenne)
        {
            if (moyennesDA.supprimer(moyenne))
            {
                journalDA.journaliser("suppression de la moyenne séquentielle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee + ", séquence : " + moyenne.codeSeq + ", moyenne : " + moyenne.moyenne);
                return true;
            }
            return false;
        }

        // modifier une Moyenne
        public bool modifierMoyenne(MoyennesBE moyenne, MoyennesBE newMoyenne)
        {
            if (moyennesDA.modifier(moyenne, newMoyenne))
            {
                journalDA.journaliser("modification de la moyenne séquentielle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee + ", séquence : " + moyenne.codeSeq + ". ancienne moyenne : " + moyenne.moyenne + ", nouvelle moyenne : " + newMoyenne.moyenne);
                return true;
            }
            return false;
        }

        // modifier une Moyenne
        public bool modifierMoyenne(MoyennesBE moyenne)
        {
            if (moyennesDA.modifier(moyenne))
            {
                journalDA.journaliser("modification de la moyenne séquentielle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee + ", séquence : " + moyenne .codeSeq+ ". ancienne moyenne : " + moyenne.moyenne + ", nouvelle moyenne : " + moyenne.moyenne);
                return true;
            }
            return false;
        }

        // rechercher une Moyenne
        public MoyennesBE rechercherMoyenne(MoyennesBE moyenne)
        {
            return moyennesDA.rechercher(moyenne);
        }

        // ajouter une Moyenne
        public bool ajouterMoyenne(MoyennesBE moyenne)
        {
            if (moyennesDA.ajouter(moyenne))
            {
                journalDA.journaliser("enregistrement de la moyenne séquentielle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee + ", séquence : " + moyenne.codeSeq + ", moyenne : " + moyenne.moyenne);
                return true;
            }
            return false;
        }

        // supprimer une Moyenne
        public bool supprimerMoyenne(MoyennesBE moyenne)
        {
            if (moyennesDA.supprimer(moyenne))
            {
                journalDA.journaliser("suppression de la moyenne séquentielle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee + ", séquence : " + moyenne.codeSeq + ", moyenne : " + moyenne.moyenne);
                return true;
            }
            return false;
        }

        // ajouter un Resultat
        public bool ajouterResultat(ResultatBE resultat)
        {
            if (resultatDA.ajouter(resultat))
            {
                journalDA.journaliser("enregistrement d'un résultat séquentiel de l'élève de matricule " + resultat.matricule + ". séquence : " + resultat.codeseq + ", année : " + resultat.annee + ", moyenne : " + resultat.moyenne);
                return true;
            }
            return false;
        }

        // rechercher un Resultat
        public ResultatBE rechercherResultat(ResultatBE resultat)
        {
            return resultatDA.rechercher(resultat);
        }

        // modifier un Resultat
        public bool modifierResultat(ResultatBE resultat, ResultatBE newResultat)
        {
            if (resultatDA.modifier(resultat, newResultat))
            {
                journalDA.journaliser("modification du résultat séquentiel de l'élève de matricule " + resultat.matricule + ". séquence : " + resultat.codeseq + ", année : " + resultat.annee + ". ancienne moyenne : " + resultat.moyenne + ", nouvelle moyenne : " + newResultat.moyenne);
                return true;
            }
            return false;
        }

        // supprimer un Resultat
        public bool supprimerResultat(ResultatBE resultat)
        {
            if (resultatDA.supprimer(resultat))
            {
                journalDA.journaliser("suppression du résultat séquentiel de l'élève de matricule " + resultat.matricule + ". séquence : " + resultat.codeseq + ", année : " + resultat.annee + ", moyenne : " + resultat.moyenne);
                return true;
            }
            return false;
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

        // rechercher un élève
        public EleveBE rechercherEleve(EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
        }

        //lister toutes les Moyennes
        public List<MoyennesBE> listerToutesLesMoyennes()
        {
            return moyennesDA.listerTous();
        }

        // lister toutes les Moyennes respectant un certain critère
        public List<MoyennesBE> listerMoyennesSuivantCritere(string critere)
        {
            return moyennesDA.listerSuivantCritere(critere);
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

        //lister tous les Trimestres
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

        // retourne la liste des codes de Matière deja enregistré (pour le filtre)
        public List<string> getListCodeMatiere(List<MatiereBE> listMatiere)
        {
            List<string> listeCodeMatiere = new List<string>();

            listeCodeMatiere = new List<string>();
            if (listMatiere != null)
            {
                for (int i = 0; i < listMatiere.Count; i++)
                {
                    listeCodeMatiere.Add(listMatiere.ElementAt(i).codeMat);
                }
                return listeCodeMatiere;
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

        //liste les types d'évaluations d'une matière (avec leurs poids) à une année donnée
        public List<EvaluerBE> listeEvaluationDuneMatierePourUneClasseAUneAnnee(ClasseBE classe, MatiereBE matiere, int annee) {
            return evaluerDA.listerSuivantCritere("codeclasse = '"+classe.codeClasse+"' AND codemat = '"+matiere.codeMat+"' AND annee = '"+annee+"'");
        }

        //obtenir les notes d'un élève à une matière pour une séquence et pour une année
        public List<NotesBE> listeNotesDunEleveAUneMatierePourUneSequenceEtPourUneAnnee(String matriculeEleve, String codeMatiere, String codeSeq, int annee)
        {
            return notesDA.listerSuivantCritere("matricule ='"+matriculeEleve+"' AND codeMat = '"+codeMatiere+"' AND codeseq = '" + codeSeq + "' AND annee = '" + annee + "'");
        }
        
        //obtenir les notes d'une matière d'un élève pour une séquence et pour une année
        public List<NotesBE> listeNotesDuneSequence(String codeSeq, int annee)
        {
            return notesDA.listerSuivantCritere("codeseq = '" + codeSeq + "' AND annee = '" + annee + "'");
        }

        //obtenir le poid d'une évaluation d'une matière pour une classe et une année donné et une séquence
        public int ObtenirLePoidDuneMatierePourUneClasseEtUneAnnee(String codeClasse, String codeMatiere, String codeEvaluation, int annee, String codeSequence) {
            EvaluerBE evaluer = new EvaluerBE();
            evaluer.codeClasse = codeClasse;
            evaluer.codeMat = codeMatiere;
            evaluer.codeEvaluation = codeEvaluation;
            evaluer.codeSequence = codeSequence;

            return evaluerDA.rechercher(evaluer).poids;
            
        }

        //Calcul de la moyenne d'un élève pour une classe, une séquence, une matière et une année donnée
        //il s'agit de remplir la table "moyennes"
        public void calculerMoyenneSequentielleDunEleve(String matricule, String codeClasse, String codeSequence, int annee) {
            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;

            ClasseBE classeBE = rechercherClasse(classe);

            //on liste les matières de la classe pour l'année choisi
            List<MatiereBE> LMatiere = listeDesMatieresDuneClasse(classeBE, annee);

            if (LMatiere != null && LMatiere.Count != 0)
            {
                //on calcul les moyennes séquentielles par matières
                for (int j = 0; j < LMatiere.Count; j++)
                {
                    // calcul de la moyenne Sequentielle des élèves sur chacune des matières individuellement
                    String[] List = moyennesDA.calculMoyenneSequentielleDunEleve(matricule, codeClasse, LMatiere.ElementAt(j).codeMat, codeSequence, annee);

                    if (List != null)
                    {
                        //on fabrique les objets de type moyennes Sequentielle et on les mets dans le BD

                        /*
                         * List.ElementAt(i)[0] : matricule;
                           List.ElementAt(i)[1] : codeMatiere;
                           List.ElementAt(i)[2] : codeseq;
                         * List.ElementAt(i)[3] : moyenne;
                         */
                        MoyennesBE moyenneSequentielle = new MoyennesBE();
                        moyenneSequentielle.codeMat = List[1];
                        moyenneSequentielle.codeSeq = codeSequence;
                        moyenneSequentielle.matricule = List[0];
                        moyenneSequentielle.moyenne = Convert.ToDouble(List[3]);
                        moyenneSequentielle.annee = annee;
                        moyenneSequentielle.rang = 0;
                        moyenneSequentielle.moyenneClasse = 0;

                        if (getMention(moyenneSequentielle.moyenne) != null)
                            moyenneSequentielle.mention = getMention(moyenneSequentielle.moyenne);
                        else moyenneSequentielle.mention = "";

                        moyenneSequentielle.moyenneMin = 0;
                        moyenneSequentielle.moyenneMax = 0;


                        // on met à jour la note de l'élève qu'on vient de calculer dans la BD
                        if (moyennesDA.rechercher(moyenneSequentielle) != null)
                            moyennesDA.supprimer(moyenneSequentielle);

                        moyennesDA.ajouter(moyenneSequentielle);


                        //on charge la liste des moyennes des élèves qui sont dans la même classe pour mettre à jour 
                        // les infos tels que le rang la moyenne générale de la classe, les moyennes min et max

                        List<MoyennesBE> LMoyennesSequentielle = moyennesDA.listerMoyennesSequentielleDesElevesDuneClasse(codeClasse, LMatiere.ElementAt(j).codeMat, codeSequence, annee);

                        if (LMoyennesSequentielle != null && LMoyennesSequentielle.Count != 0)
                        {

                            //-------------- on calcule le rang, la moyenne générale de la classe, les moyennes min et max

                            double moyenneDeLaClasse = 0; // moyenne générale de la classe
                            double moyenneMin = LMoyennesSequentielle.ElementAt(0).moyenne; // moyenne mininale des élèves de la classe
                            double moyenneMax = LMoyennesSequentielle.ElementAt(0).moyenne; // moyenne maximale des élèves de la classe

                            MoyennesBE moyennePrecedente = new MoyennesBE();
                            //on trie la liste
                            LMoyennesSequentielle = LMoyennesSequentielle.OrderByDescending(o => o.moyenne).ToList();

                            for (int i = 0; i < LMoyennesSequentielle.Count; i++)
                            {
                                // ------------------- DEBUT détermination du rang 
                                if (i == 0)
                                { // on est sur le premier (celui qui a la plus grande note)
                                    MoyennesBE oldMoyenne = LMoyennesSequentielle.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                    LMoyennesSequentielle.ElementAt(i).rang = 1; // le premier pour cette séquence et cette matière

                                    //on met à jour le rang dans la BD
                                    //modifierMoyenne(oldMoyenne, LMoyennesSequentielle.ElementAt(j));

                                    moyennePrecedente = LMoyennesSequentielle.ElementAt(i);

                                }
                                else
                                {

                                    if (LMoyennesSequentielle.ElementAt(i).moyenne == moyennePrecedente.moyenne)
                                    {
                                        MoyennesBE oldMoyenne = LMoyennesSequentielle.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                        //alors on a un ex éco (ils ont le même rang)
                                        LMoyennesSequentielle.ElementAt(i).rang = moyennePrecedente.rang;

                                        //on met à jour le rang dans la BD
                                        //modifierMoyenne(oldMoyenne, LMoyenneAvecRang.ElementAt(j));

                                    }
                                    else
                                    {


                                        MoyennesBE oldMoyenne = LMoyennesSequentielle.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                        //alors l'élève prend le rend suivant
                                        //LMoyenneAvecRang.ElementAt(j).rang = moyennePrecedente.rang + 1;
                                        LMoyennesSequentielle.ElementAt(i).rang = i + 1;

                                        //on met à jour le rang dans la BD
                                        //modifierMoyenne(oldMoyenne, LMoyenneAvecRang.ElementAt(j));

                                    }

                                    moyennePrecedente = LMoyennesSequentielle.ElementAt(i);
                                }
                                // ------------------- FIN détermination du rang 

                                // ------------------- DEBUT détermination de la moyenne de la classe

                                moyenneDeLaClasse = moyenneDeLaClasse + LMoyennesSequentielle.ElementAt(i).moyenne;

                                // ------------------- FIN détermination de la moyenne de la classe

                                // ------------------- DEBUT détermination des moyennes minimales et maximales 

                                if (LMoyennesSequentielle.ElementAt(i).moyenne < moyenneMin)
                                    moyenneMin = LMoyennesSequentielle.ElementAt(i).moyenne;

                                if (LMoyennesSequentielle.ElementAt(i).moyenne > moyenneMax)
                                    moyenneMax = LMoyennesSequentielle.ElementAt(i).moyenne;

                                // ------------------- FIN détermination des moyennes minimales et maximales 
                            }

                            moyenneDeLaClasse = moyenneDeLaClasse / LMoyennesSequentielle.Count;

                            //------------------- DEBUT mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax

                            for (int i = 0; i < LMoyennesSequentielle.Count; i++)
                            {
                                LMoyennesSequentielle.ElementAt(i).moyenneClasse = moyenneDeLaClasse;
                                LMoyennesSequentielle.ElementAt(i).moyenneMin = moyenneMin;
                                LMoyennesSequentielle.ElementAt(i).moyenneMax = moyenneMax;

                                //on met à jour le rang dans la BD
                                if (moyennesDA.rechercher(LMoyennesSequentielle.ElementAt(i)) != null)
                                    moyennesDA.supprimer(LMoyennesSequentielle.ElementAt(i));

                                moyennesDA.ajouter(LMoyennesSequentielle.ElementAt(i));

                                //modifierMoyenneTrimestriel(LMoyennesTrimestriels.ElementAt(i), LMoyennesTrimestriels.ElementAt(i));
                            }

                            //------------------- FIN mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax
                        }
                    }

                }
            }

            journalDA.journaliser("Calcul de la moyenne séquentielle de l'élève de matricule : " + matricule + ", séquence : "+codeSequence+", année : " + annee);

        }


        //Calcul du résultats des élèves
        // il s'agit du calcul de la note des élèves à une séquence pour une année donnée, en prenant en compte les notes
        // de l'élève sur toutes les évaluations des différentes matières ainsi que de leurs coéfficient

        //il s'agit de remplir la table "Résultat"
        public void calculerResultatsSequentielDunEleve(String matricule, String codeClasse, String codeSequence, int annee) {
            // calcul de la moyenne Sequentielle des élèves sur chacune des matières individuellement
            String[] List = resultatDA.calculResultatSequentielDunEleve(matricule, codeClasse, codeSequence, annee);
            if (List != null)
            {
              
                    /*
                     * List[0] : matricule;
                       List[1] : codeSequence;
                       List[2] : annee;
                     * List[3] : totalPoint; // le total des points de l'élève
                     * List[4] : moyenne; // la moyenne de l'élève
                     * List[5] : coef; // la somme des coeficients
                     */

                    ResultatBE resultatSequentiel = new ResultatBE();
                    resultatSequentiel.codeseq = codeSequence;
                    resultatSequentiel.matricule = List[0];
                    resultatSequentiel.point = Convert.ToDouble(List[3]);
                    resultatSequentiel.coef = Convert.ToInt16(List[5]);
                    resultatSequentiel.moyenne = Convert.ToDouble(List[4]);
                    resultatSequentiel.annee = annee;
                    resultatSequentiel.rang = 0;
                    resultatSequentiel.moyenneclasse = 0;
                    if (getMention(resultatSequentiel.moyenne) != null)
                        resultatSequentiel.mention = getMention(resultatSequentiel.moyenne);
                    else resultatSequentiel.mention = "";
                    resultatSequentiel.remarque = "";

                    if (resultatSequentiel.moyenne >= 10) {
                        resultatSequentiel.decision = "Admis";
                    }
                    else resultatSequentiel.decision = "Echec";

                    //resultatSequentiel.moyenneMin = 0;
                    //resultatresultatSequentiel.moyenneMax = 0;

                // on met à jour le résultat de l'élève qu'on vient de calculer dans la BD

                    if (resultatDA.rechercher(resultatSequentiel) != null)
                        resultatDA.supprimer(resultatSequentiel);

                    resultatDA.ajouter(resultatSequentiel);

                //maintenant on charge la liste des résultats de tous les élèves de la classe pour mettre à jour les infos telles que
                // le rang, la moyenne de la classe, etc..

                List<ResultatBE> LResultatSequentiels = resultatDA.listerResultatsSequentielsDesElevesDuneClasse(codeClasse, codeSequence, annee);

                //-------------- on calcule le rang, la moyenne générale de la classe, les moyennes min et max

                if (LResultatSequentiels != null && LResultatSequentiels.Count != 0)
                {
                    double moyenneDeLaClasse = 0; // moyenne générale de la classe
                    //double moyenneMin = LResultatTrimestriels.ElementAt(0).moyenne; // moyenne mininale des élèves de la classe
                    //double moyenneMax = LResultatTrimestriels.ElementAt(0).moyenne; // moyenne maximale des élèves de la classe

                    //on trie la liste
                    LResultatSequentiels = LResultatSequentiels.OrderByDescending(o => o.moyenne).ToList();

                    ResultatBE resultatPrecedent = new ResultatBE();

                    for (int i = 0; i < LResultatSequentiels.Count; i++)
                    {
                        // ------------------- DEBUT détermination du rang 
                        if (i == 0)
                        { // on est sur le premier (celui qui a la plus grande note)
                            ResultatBE oldResultat = LResultatSequentiels.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                            LResultatSequentiels.ElementAt(i).rang = 1; // le premier pour cette séquence et cette matière

                            //on met à jour le rang dans la BD
                            //modifierMoyenne(oldResultat, LResultatTrimestriels.ElementAt(j));

                            resultatPrecedent = LResultatSequentiels.ElementAt(i);

                        }
                        else
                        {

                            if (LResultatSequentiels.ElementAt(i).moyenne == resultatPrecedent.moyenne)
                            {
                                ResultatBE oldResultat = LResultatSequentiels.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                //alors on a un ex éco (ils ont le même rang)
                                LResultatSequentiels.ElementAt(i).rang = resultatPrecedent.rang;

                                //on met à jour le rang dans la BD
                                //modifierMoyenne(oldResultat, LMoyenneAvecRang.ElementAt(j));

                            }
                            else
                            {


                                ResultatBE oldResultat = LResultatSequentiels.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                //alors l'élève prend le rend suivant
                                //LResultatTrimestriels.ElementAt(j).rang = moyennePrecedente.rang + 1;
                                LResultatSequentiels.ElementAt(i).rang = i + 1;

                                //on met à jour le rang dans la BD
                                //modifierMoyenne(oldResultat, LMoyenneAvecRang.ElementAt(j));

                            }

                            resultatPrecedent = LResultatSequentiels.ElementAt(i);
                        }
                        // ------------------- FIN détermination du rang 

                        // ------------------- DEBUT détermination de la moyenne de la classe

                        moyenneDeLaClasse = moyenneDeLaClasse + LResultatSequentiels.ElementAt(i).moyenne;

                        // ------------------- FIN détermination de la moyenne de la classe



                        //// ------------------- DEBUT détermination des moyennes minimales et maximales 

                        //if (LResultatTrimestriels.ElementAt(i).moyenne < moyenneMin)
                        //    moyenneMin = LMoyennesTrimestriels.ElementAt(i).moyenne;

                        //if (LMoyennesTrimestriels.ElementAt(i).moyenne > moyenneMax)
                        //    moyenneMax = LMoyennesTrimestriels.ElementAt(i).moyenne;

                        //// ------------------- FIN détermination des moyennes minimales et maximales 
                    }

                    moyenneDeLaClasse = moyenneDeLaClasse / LResultatSequentiels.Count;

                    //------------------- DEBUT mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax

                    for (int i = 0; i < LResultatSequentiels.Count; i++)
                    {
                        LResultatSequentiels.ElementAt(i).moyenneclasse = moyenneDeLaClasse;
                        //LResultatTrimestriels.ElementAt(i).moyenneMin = moyenneMin;
                        //LResultatTrimestriels.ElementAt(i).moyenneMax = moyenneMax;

                        //on met à jour le rang dans la BD
                        if (resultatDA.rechercher(LResultatSequentiels.ElementAt(i)) != null)
                            resultatDA.supprimer(LResultatSequentiels.ElementAt(i));

                        resultatDA.ajouter(LResultatSequentiels.ElementAt(i));

                        //modifierMoyenneSequentiel(LMoyennesTrimestriels.ElementAt(i), LMoyennesTrimestriels.ElementAt(i));
                    }

                    //------------------- FIN mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax
                }
            }

            journalDA.journaliser("Calcul du résultat séquentielle de l'élève de matricule  " + matricule + ", séquence : "+codeSequence+", année : " + annee);

        }


        //Calcul de la moyenne d'un élève pour une classe, un Trimestre, une matière et une année donnée
        //il s'agit de remplir la table "moyennesTrimestrielle"
        public void calculerMoyenneTrimestrielleDunEleve(String matricule, String codeClasse, String codeTrimestre, int annee)
        {
            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;

            ClasseBE classeBE = rechercherClasse(classe);

            //on liste les matières de la classe pour l'année choisi
            List<MatiereBE> LMatiere = listeDesMatieresDuneClasse(classeBE, annee);
            if (LMatiere != null && LMatiere.Count != 0)
            {
                //on calcul les moyennes séquentielles par matières
                for (int j = 0; j < LMatiere.Count; j++)
                {
                    // calcul de la moyenne Sequentielle des élèves sur chacune des matières individuellement
                    String[] List = moyennesTrimestrielsDA.calculMoyenneTrimestrielleDunEleve(matricule, codeClasse, LMatiere.ElementAt(j).codeMat, codeTrimestre, annee);

                    if (List != null)
                    {
                        //on fabrique les objets de type moyennes Sequentielle et on les mets dans le BD

                        /*
                         * List.ElementAt(i)[0] : matricule;
                           List.ElementAt(i)[1] : codeMatiere;
                           List.ElementAt(i)[2] : codeTrimestre;
                         * List.ElementAt(i)[3] : moyenne;
                         */
                        MoyennesTrimestrielsBE moyenneTrimestrielle = new MoyennesTrimestrielsBE();
                        moyenneTrimestrielle.codeMat = List[1];
                        moyenneTrimestrielle.codeTrimestre = codeTrimestre;
                        moyenneTrimestrielle.matricule = List[0];
                        moyenneTrimestrielle.moyenne = Convert.ToDouble(List[3]);
                        moyenneTrimestrielle.annee = annee;
                        moyenneTrimestrielle.rang = 0;
                        moyenneTrimestrielle.moyenneClasse = 0;

                        if (getMention(moyenneTrimestrielle.moyenne) != null)
                            moyenneTrimestrielle.mention = getMention(moyenneTrimestrielle.moyenne);
                        else moyenneTrimestrielle.mention = "";

                        moyenneTrimestrielle.moyenneMin = 0;
                        moyenneTrimestrielle.moyenneMax = 0;


                        // on met à jour la note de l'élève qu'on vient de calculer dans la BD
                        if (moyennesTrimestrielsDA.rechercher(moyenneTrimestrielle) != null)
                            moyennesTrimestrielsDA.supprimer(moyenneTrimestrielle);

                        moyennesTrimestrielsDA.ajouter(moyenneTrimestrielle);


                        //on charge la liste des moyennes des élèves qui sont dans la même classe pour mettre à jour 
                        // les infos tels que le rang la moyenne générale de la classe, les moyennes min et max

                        List<MoyennesTrimestrielsBE> LMoyennesTrimestrielle = moyennesTrimestrielsDA.listerMoyennesTrimestrielleDesElevesDuneClasse(codeClasse, LMatiere.ElementAt(j).codeMat, codeTrimestre, annee);

                        if (LMoyennesTrimestrielle != null && LMoyennesTrimestrielle.Count != 0)
                        {

                            //-------------- on calcule le rang, la moyenne générale de la classe, les moyennes min et max

                            double moyenneDeLaClasse = 0; // moyenne générale de la classe
                            double moyenneMin = LMoyennesTrimestrielle.ElementAt(0).moyenne; // moyenne mininale des élèves de la classe
                            double moyenneMax = LMoyennesTrimestrielle.ElementAt(0).moyenne; // moyenne maximale des élèves de la classe

                            MoyennesTrimestrielsBE moyennePrecedente = new MoyennesTrimestrielsBE();
                            //on trie la liste
                            LMoyennesTrimestrielle = LMoyennesTrimestrielle.OrderByDescending(o => o.moyenne).ToList();

                            for (int i = 0; i < LMoyennesTrimestrielle.Count; i++)
                            {
                                // ------------------- DEBUT détermination du rang 
                                if (i == 0)
                                { // on est sur le premier (celui qui a la plus grande note)
                                    MoyennesTrimestrielsBE oldMoyenne = LMoyennesTrimestrielle.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                    LMoyennesTrimestrielle.ElementAt(i).rang = 1; // le premier pour cette séquence et cette matière

                                    //on met à jour le rang dans la BD
                                    //modifierMoyenne(oldMoyenne, LMoyennesSequentielle.ElementAt(j));

                                    moyennePrecedente = LMoyennesTrimestrielle.ElementAt(i);

                                }
                                else
                                {

                                    if (LMoyennesTrimestrielle.ElementAt(i).moyenne == moyennePrecedente.moyenne)
                                    {
                                        MoyennesTrimestrielsBE oldMoyenne = LMoyennesTrimestrielle.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                        //alors on a un ex éco (ils ont le même rang)
                                        LMoyennesTrimestrielle.ElementAt(i).rang = moyennePrecedente.rang;

                                        //on met à jour le rang dans la BD
                                        //modifierMoyenne(oldMoyenne, LMoyenneAvecRang.ElementAt(j));

                                    }
                                    else
                                    {


                                        MoyennesTrimestrielsBE oldMoyenne = LMoyennesTrimestrielle.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                        //alors l'élève prend le rend suivant
                                        //LMoyenneAvecRang.ElementAt(j).rang = moyennePrecedente.rang + 1;
                                        LMoyennesTrimestrielle.ElementAt(i).rang = i + 1;

                                        //on met à jour le rang dans la BD
                                        //modifierMoyenne(oldMoyenne, LMoyenneAvecRang.ElementAt(j));

                                    }

                                    moyennePrecedente = LMoyennesTrimestrielle.ElementAt(i);
                                }
                                // ------------------- FIN détermination du rang 

                                // ------------------- DEBUT détermination de la moyenne de la classe

                                moyenneDeLaClasse = moyenneDeLaClasse + LMoyennesTrimestrielle.ElementAt(i).moyenne;

                                // ------------------- FIN détermination de la moyenne de la classe

                                // ------------------- DEBUT détermination des moyennes minimales et maximales 

                                if (LMoyennesTrimestrielle.ElementAt(i).moyenne < moyenneMin)
                                    moyenneMin = LMoyennesTrimestrielle.ElementAt(i).moyenne;

                                if (LMoyennesTrimestrielle.ElementAt(i).moyenne > moyenneMax)
                                    moyenneMax = LMoyennesTrimestrielle.ElementAt(i).moyenne;

                                // ------------------- FIN détermination des moyennes minimales et maximales 
                            }

                            moyenneDeLaClasse = moyenneDeLaClasse / LMoyennesTrimestrielle.Count;

                            //------------------- DEBUT mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax

                            for (int i = 0; i < LMoyennesTrimestrielle.Count; i++)
                            {
                                LMoyennesTrimestrielle.ElementAt(i).moyenneClasse = moyenneDeLaClasse;
                                LMoyennesTrimestrielle.ElementAt(i).moyenneMin = moyenneMin;
                                LMoyennesTrimestrielle.ElementAt(i).moyenneMax = moyenneMax;

                                //on met à jour le rang dans la BD
                                if (moyennesTrimestrielsDA.rechercher(LMoyennesTrimestrielle.ElementAt(i)) != null)
                                    moyennesTrimestrielsDA.supprimer(LMoyennesTrimestrielle.ElementAt(i));

                                moyennesTrimestrielsDA.ajouter(LMoyennesTrimestrielle.ElementAt(i));

                                //modifierMoyenneTrimestriel(LMoyennesTrimestriels.ElementAt(i), LMoyennesTrimestriels.ElementAt(i));
                            }

                            //------------------- FIN mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax
                        }
                    }

                }
            }

            journalDA.journaliser("Calcul des moyennes Trimestriemlles de l'élève de matricule : " + matricule + ", trimestre : " + codeTrimestre + ", année : " + annee);

        }


        //Calcul du résultats trimestriel d'un élève
        // il s'agit du calcul de la note de l'élève à un trimestre pour une année donnée

        //il s'agit de remplir la table "RésultatTrimestriel"
        public void calculerResultatsTrimestrielDunEleve(String matricule, String codeClasse, String codeTrimestre, int annee)
        {
            // calcul du resultat Trimestriel de l'élève sur chacune des matières individuellement
            String[] List = resultatTrimestrielDA.calculResultatTrimestrielDunEleve(matricule, codeClasse, codeTrimestre, annee);
            if (List != null)
            {
                /*
                 * List[0] : matricule;
                   List[1] : codeTrimestre;
                   List[2] : annee;
                 * List[3] : totalPoint; // le total des points de l'élève
                 * List[4] : moyenne; // la moyenne de l'élève
                 * List[5] : coef; // la somme des coeficients
                 */

                ResultatTrimestrielBE resultatTrimestriel = new ResultatTrimestrielBE();
                resultatTrimestriel.codeTrimestre = codeTrimestre;
                resultatTrimestriel.matricule = List[0];
                resultatTrimestriel.point = Convert.ToDouble(List[3]);
                resultatTrimestriel.coef = Convert.ToInt16(List[5]);
                resultatTrimestriel.moyenne = Convert.ToDouble(List[4]);
                resultatTrimestriel.annee = annee;
                resultatTrimestriel.rang = 0;
                resultatTrimestriel.moyenneclasse = 0;
                if (getMention(resultatTrimestriel.moyenne) != null)
                    resultatTrimestriel.mention = getMention(resultatTrimestriel.moyenne);
                else resultatTrimestriel.mention = "";
                resultatTrimestriel.remarque = "";

                if (resultatTrimestriel.moyenne >= 10)
                {
                    resultatTrimestriel.decision = "Admis";
                }
                else resultatTrimestriel.decision = "Echec";

                //resultatTrimestriel.moyenneMin = 0;
                //resultatTrimestriel.moyenneMax = 0;

                // on met à jour le résultat de l'élève qu'on vient de calculer dans la BD

                if (resultatTrimestrielDA.rechercher(resultatTrimestriel) != null)
                    resultatTrimestrielDA.supprimer(resultatTrimestriel);

                resultatTrimestrielDA.ajouter(resultatTrimestriel);

                //maintenant on charge la liste des résultats de tous les élèves de la classe pour mettre à jour les infos telles que
                // le rang, la moyenne de la classe, etc..

                List<ResultatTrimestrielBE> LResultatTrimestriels = resultatTrimestrielDA.listerResultatsTrimestrielDesElevesDuneClasse(codeClasse, codeTrimestre, annee);

                if (LResultatTrimestriels != null && LResultatTrimestriels.Count != 0)
                {
                    //-------------- on calcule le rang, la moyenne générale de la classe, les moyennes min et max

                    double moyenneDeLaClasse = 0; // moyenne générale de la classe
                    //double moyenneMin = LResultatTrimestriels.ElementAt(0).moyenne; // moyenne mininale des élèves de la classe
                    //double moyenneMax = LResultatTrimestriels.ElementAt(0).moyenne; // moyenne maximale des élèves de la classe

                    //on trie la liste
                    LResultatTrimestriels = LResultatTrimestriels.OrderByDescending(o => o.moyenne).ToList();

                    ResultatTrimestrielBE resultatPrecedent = new ResultatTrimestrielBE();

                    for (int i = 0; i < LResultatTrimestriels.Count; i++)
                    {
                        // ------------------- DEBUT détermination du rang 
                        if (i == 0)
                        { // on est sur le premier (celui qui a la plus grande note)
                            ResultatTrimestrielBE oldResultat = LResultatTrimestriels.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                            LResultatTrimestriels.ElementAt(i).rang = 1; // le premier pour cette séquence et cette matière

                            //on met à jour le rang dans la BD
                            //modifierMoyenne(oldResultat, LResultatTrimestriels.ElementAt(j));

                            resultatPrecedent = LResultatTrimestriels.ElementAt(i);

                        }
                        else
                        {

                            if (LResultatTrimestriels.ElementAt(i).moyenne == resultatPrecedent.moyenne)
                            {
                                ResultatTrimestrielBE oldResultat = LResultatTrimestriels.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                //alors on a un ex éco (ils ont le même rang)
                                LResultatTrimestriels.ElementAt(i).rang = resultatPrecedent.rang;

                                //on met à jour le rang dans la BD
                                //modifierMoyenne(oldResultat, LMoyenneAvecRang.ElementAt(j));

                            }
                            else
                            {
                                ResultatTrimestrielBE oldResultat = LResultatTrimestriels.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                //alors l'élève prend le rend suivant
                                //LResultatTrimestriels.ElementAt(j).rang = moyennePrecedente.rang + 1;
                                LResultatTrimestriels.ElementAt(i).rang = i + 1;

                                //on met à jour le rang dans la BD
                                //modifierMoyenne(oldResultat, LMoyenneAvecRang.ElementAt(j));

                            }

                            resultatPrecedent = LResultatTrimestriels.ElementAt(i);
                        }
                        // ------------------- FIN détermination du rang 

                        // ------------------- DEBUT détermination de la moyenne de la classe

                        moyenneDeLaClasse = moyenneDeLaClasse + LResultatTrimestriels.ElementAt(i).moyenne;

                        // ------------------- FIN détermination de la moyenne de la classe

                    }

                    moyenneDeLaClasse = moyenneDeLaClasse / LResultatTrimestriels.Count;

                    //------------------- DEBUT mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax

                    for (int i = 0; i < LResultatTrimestriels.Count; i++)
                    {
                        LResultatTrimestriels.ElementAt(i).moyenneclasse = moyenneDeLaClasse;
                        //LResultatTrimestriels.ElementAt(i).moyenneMin = moyenneMin;
                        //LResultatTrimestriels.ElementAt(i).moyenneMax = moyenneMax;

                        //on met à jour le rang dans la BD
                        if (resultatTrimestrielDA.rechercher(LResultatTrimestriels.ElementAt(i)) != null)
                            resultatTrimestrielDA.supprimer(LResultatTrimestriels.ElementAt(i));

                        resultatTrimestrielDA.ajouter(LResultatTrimestriels.ElementAt(i));

                    }

                    //------------------- FIN mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax
                }
            }

            journalDA.journaliser("Calcul du résultat trimestriel de l'élève de matricule  " + matricule + ", trimestre : " + codeTrimestre + ", année : " + annee);

        }

        //Calcul de la moyenne d'un élève pour une classe, une matière et une année donnée
        //il s'agit de remplir la table "moyennesAnuelle"
        public void calculerMoyenneAnnuelleDunEleve(String matricule, String codeClasse, int annee)
        {
            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;

            ClasseBE classeBE = rechercherClasse(classe);

            //on liste les matières de la classe pour l'année choisi
            List<MatiereBE> LMatiere = listeDesMatieresDuneClasse(classeBE, annee);

            if (LMatiere != null && LMatiere.Count != 0)
            {
                //on calcul les moyennes séquentielles par matières
                for (int j = 0; j < LMatiere.Count; j++)
                {
                    // calcul de la moyenne Sequentielle des élèves sur chacune des matières individuellement
                    String[] List = moyennesAnnuellesDA.calculMoyenneAnnuellesDunEleve(matricule, codeClasse, LMatiere.ElementAt(j).codeMat, annee);

                    if (List != null)
                    {
                        //on fabrique les objets de type moyennes Sequentielle et on les mets dans le BD

                        /*
                         * List.ElementAt(i)[0] : matricule;
                           List.ElementAt(i)[1] : codeMatiere;
                         * List.ElementAt(i)[2] : moyenne;
                         */
                        MoyennesAnnuellesBE moyenneAnnuelle = new MoyennesAnnuellesBE();
                        moyenneAnnuelle.codeMat = List[1];
                        moyenneAnnuelle.matricule = List[0];
                        moyenneAnnuelle.moyenne = Convert.ToDouble(List[2]);
                        moyenneAnnuelle.annee = annee;
                        moyenneAnnuelle.rang = 0;
                        moyenneAnnuelle.moyenneClasse = 0;

                        if (getMention(moyenneAnnuelle.moyenne) != null)
                            moyenneAnnuelle.mention = getMention(moyenneAnnuelle.moyenne);
                        else moyenneAnnuelle.mention = "";

                        moyenneAnnuelle.moyenneMin = 0;
                        moyenneAnnuelle.moyenneMax = 0;


                        // on met à jour la note de l'élève qu'on vient de calculer dans la BD
                        if (moyennesAnnuellesDA.rechercher(moyenneAnnuelle) != null)
                            moyennesAnnuellesDA.supprimer(moyenneAnnuelle);

                        moyennesAnnuellesDA.ajouter(moyenneAnnuelle);


                        //on charge la liste des moyennes des élèves qui sont dans la même classe pour mettre à jour 
                        // les infos tels que le rang la moyenne générale de la classe, les moyennes min et max

                        List<MoyennesAnnuellesBE> LMoyennesAnnuelle = moyennesAnnuellesDA.listerMoyennesAnnuellesDesElevesDuneClasse(codeClasse, LMatiere.ElementAt(j).codeMat, annee);

                        if (LMoyennesAnnuelle != null && LMoyennesAnnuelle.Count != 0)
                        {
                            //-------------- on calcule le rang, la moyenne générale de la classe, les moyennes min et max

                            double moyenneDeLaClasse = 0; // moyenne générale de la classe
                            double moyenneMin = LMoyennesAnnuelle.ElementAt(0).moyenne; // moyenne mininale des élèves de la classe
                            double moyenneMax = LMoyennesAnnuelle.ElementAt(0).moyenne; // moyenne maximale des élèves de la classe

                            MoyennesAnnuellesBE moyennePrecedente = new MoyennesAnnuellesBE();
                            //on trie la liste
                            LMoyennesAnnuelle = LMoyennesAnnuelle.OrderByDescending(o => o.moyenne).ToList();

                            for (int i = 0; i < LMoyennesAnnuelle.Count; i++)
                            {
                                // ------------------- DEBUT détermination du rang 
                                if (i == 0)
                                { // on est sur le premier (celui qui a la plus grande note)
                                    MoyennesAnnuellesBE oldMoyenne = LMoyennesAnnuelle.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                    LMoyennesAnnuelle.ElementAt(i).rang = 1; // le premier pour cette séquence et cette matière

                                    //on met à jour le rang dans la BD
                                    //modifierMoyenne(oldMoyenne, LMoyennesAnnuelle.ElementAt(j));

                                    moyennePrecedente = LMoyennesAnnuelle.ElementAt(i);

                                }
                                else
                                {

                                    if (LMoyennesAnnuelle.ElementAt(i).moyenne == moyennePrecedente.moyenne)
                                    {
                                        MoyennesAnnuellesBE oldMoyenne = LMoyennesAnnuelle.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                        //alors on a un ex éco (ils ont le même rang)
                                        LMoyennesAnnuelle.ElementAt(i).rang = moyennePrecedente.rang;

                                        //on met à jour le rang dans la BD
                                        //modifierMoyenne(oldMoyenne, LMoyenneAvecRang.ElementAt(j));

                                    }
                                    else
                                    {


                                        MoyennesAnnuellesBE oldMoyenne = LMoyennesAnnuelle.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                        //alors l'élève prend le rend suivant
                                        //LMoyenneAvecRang.ElementAt(j).rang = moyennePrecedente.rang + 1;
                                        LMoyennesAnnuelle.ElementAt(i).rang = i + 1;

                                        //on met à jour le rang dans la BD
                                        //modifierMoyenne(oldMoyenne, LMoyenneAvecRang.ElementAt(j));

                                    }

                                    moyennePrecedente = LMoyennesAnnuelle.ElementAt(i);
                                }
                                // ------------------- FIN détermination du rang 

                                // ------------------- DEBUT détermination de la moyenne de la classe

                                moyenneDeLaClasse = moyenneDeLaClasse + LMoyennesAnnuelle.ElementAt(i).moyenne;

                                // ------------------- FIN détermination de la moyenne de la classe

                                // ------------------- DEBUT détermination des moyennes minimales et maximales 

                                if (LMoyennesAnnuelle.ElementAt(i).moyenne < moyenneMin)
                                    moyenneMin = LMoyennesAnnuelle.ElementAt(i).moyenne;

                                if (LMoyennesAnnuelle.ElementAt(i).moyenne > moyenneMax)
                                    moyenneMax = LMoyennesAnnuelle.ElementAt(i).moyenne;

                                // ------------------- FIN détermination des moyennes minimales et maximales 
                            }

                            moyenneDeLaClasse = moyenneDeLaClasse / LMoyennesAnnuelle.Count;

                            //------------------- DEBUT mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax

                            for (int i = 0; i < LMoyennesAnnuelle.Count; i++)
                            {
                                LMoyennesAnnuelle.ElementAt(i).moyenneClasse = moyenneDeLaClasse;
                                LMoyennesAnnuelle.ElementAt(i).moyenneMin = moyenneMin;
                                LMoyennesAnnuelle.ElementAt(i).moyenneMax = moyenneMax;

                                //on met à jour le rang dans la BD
                                if (moyennesAnnuellesDA.rechercher(LMoyennesAnnuelle.ElementAt(i)) != null)
                                    moyennesAnnuellesDA.supprimer(LMoyennesAnnuelle.ElementAt(i));

                                moyennesAnnuellesDA.ajouter(LMoyennesAnnuelle.ElementAt(i));

                                //modifierMoyenneTrimestriel(LMoyennesTrimestriels.ElementAt(i), LMoyennesTrimestriels.ElementAt(i));
                            }

                            //------------------- FIN mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax
                        }
                    }

                }
            }

            journalDA.journaliser("Calcul des moyennes Annuelles de l'élève de matricule : " + matricule + ", année : " + annee);

        }


        //Calcul du résultats Annuel d'un élève
        // il s'agit du calcul de la note de l'élève pour une année donnée

        //il s'agit de remplir la table "RésultatAnnuel"
        public void calculerResultatsAnnuelDunEleve(String matricule, String codeClasse, int annee)
        {
            // calcul du resultat Trimestriel de l'élève sur chacune des matières individuellement
            String[] List = resultatAnnuelDA.calculResultatAnnuelsDunEleve(matricule, codeClasse, annee);
            if (List != null)
            {

                /*
                 * List[0] : matricule;
                   List[1] : annee;
                 * List[2] : totalPoint; // le total des points de l'élève
                 * List[3] : moyenne; // la moyenne de l'élève
                 * List[4] : coef; // la somme des coeficients
                 */

                ResultatAnnuelBE resultatAnnuel = new ResultatAnnuelBE();
                resultatAnnuel.matricule = List[0];
                resultatAnnuel.point = Convert.ToDouble(List[2]);
                resultatAnnuel.coef = Convert.ToInt16(List[4]);
                resultatAnnuel.moyenne = Convert.ToDouble(List[3]);
                resultatAnnuel.annee = annee;
                resultatAnnuel.rang = 0;
                resultatAnnuel.moyenneclasse = 0;
                if (getMention(resultatAnnuel.moyenne) != null)
                    resultatAnnuel.mention = getMention(resultatAnnuel.moyenne);
                else resultatAnnuel.mention = "";
                resultatAnnuel.remarque = "";

                if (resultatAnnuel.moyenne >= 10)
                {
                    resultatAnnuel.decision = "Admis";
                }
                else resultatAnnuel.decision = "Echec";

                EleveBE eleve = new EleveBE();
                eleve.matricule = resultatAnnuel.matricule;

                if (resultatAnnuel.moyenne >= 10)
                {
                    if (eleveDA.rechercherNiveau(eleve, annee) != -1)
                        resultatAnnuel.newNiveau = eleveDA.rechercherNiveau(eleve, annee) + 1;
                    else
                        resultatAnnuel.newNiveau = eleveDA.rechercherNiveau(eleve, annee);
                }
                else resultatAnnuel.newNiveau = eleveDA.rechercherNiveau(eleve, annee);

                //resultatTrimestriel.moyenneMin = 0;
                //resultatTrimestriel.moyenneMax = 0;

                // on met à jour le résultat de l'élève qu'on vient de calculer dans la BD

                if (resultatAnnuelDA.rechercher(resultatAnnuel) != null)
                    resultatAnnuelDA.supprimer(resultatAnnuel);

                resultatAnnuelDA.ajouter(resultatAnnuel);

                //maintenant on charge la liste des résultats de tous les élèves de la classe pour mettre à jour les infos telles que
                // le rang, la moyenne de la classe, etc..

                List<ResultatAnnuelBE> LResultatAnnuels = resultatAnnuelDA.listerResultatsAnnuelsDesElevesDuneClasse(codeClasse, annee);

                if (LResultatAnnuels != null && LResultatAnnuels.Count != 0)
                {
                    //-------------- on calcule le rang, la moyenne générale de la classe, les moyennes min et max

                    double moyenneDeLaClasse = 0; // moyenne générale de la classe
                    //double moyenneMin = LResultatAnnuels.ElementAt(0).moyenne; // moyenne mininale des élèves de la classe
                    //double moyenneMax = LResultatAnnuels.ElementAt(0).moyenne; // moyenne maximale des élèves de la classe

                    //on trie la liste
                    LResultatAnnuels = LResultatAnnuels.OrderByDescending(o => o.moyenne).ToList();

                    ResultatAnnuelBE resultatPrecedent = new ResultatAnnuelBE();

                    for (int i = 0; i < LResultatAnnuels.Count; i++)
                    {
                        // ------------------- DEBUT détermination du rang 
                        if (i == 0)
                        { // on est sur le premier (celui qui a la plus grande note)
                            ResultatAnnuelBE oldResultat = LResultatAnnuels.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                            LResultatAnnuels.ElementAt(i).rang = 1; // le premier pour cette séquence et cette matière

                            //on met à jour le rang dans la BD
                            //modifierMoyenne(oldResultat, LResultatAnnuels.ElementAt(j));

                            resultatPrecedent = LResultatAnnuels.ElementAt(i);

                        }
                        else
                        {

                            if (LResultatAnnuels.ElementAt(i).moyenne == resultatPrecedent.moyenne)
                            {
                                ResultatAnnuelBE oldResultat = LResultatAnnuels.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                //alors on a un ex éco (ils ont le même rang)
                                LResultatAnnuels.ElementAt(i).rang = resultatPrecedent.rang;

                                //on met à jour le rang dans la BD
                                //modifierMoyenne(oldResultat, LMoyenneAvecRang.ElementAt(j));

                            }
                            else
                            {
                                ResultatAnnuelBE oldResultat = LResultatAnnuels.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                //alors l'élève prend le rend suivant
                                //LResultatAnnuels.ElementAt(j).rang = moyennePrecedente.rang + 1;
                                LResultatAnnuels.ElementAt(i).rang = i + 1;

                                //on met à jour le rang dans la BD
                                //modifierMoyenne(oldResultat, LMoyenneAvecRang.ElementAt(j));

                            }

                            resultatPrecedent = LResultatAnnuels.ElementAt(i);
                        }
                        // ------------------- FIN détermination du rang 

                        // ------------------- DEBUT détermination de la moyenne de la classe

                        moyenneDeLaClasse = moyenneDeLaClasse + LResultatAnnuels.ElementAt(i).moyenne;

                        // ------------------- FIN détermination de la moyenne de la classe

                    }

                    moyenneDeLaClasse = moyenneDeLaClasse / LResultatAnnuels.Count;

                    //------------------- DEBUT mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax

                    for (int i = 0; i < LResultatAnnuels.Count; i++)
                    {
                        LResultatAnnuels.ElementAt(i).moyenneclasse = moyenneDeLaClasse;
                        //LResultatTrimestriels.ElementAt(i).moyenneMin = moyenneMin;
                        //LResultatTrimestriels.ElementAt(i).moyenneMax = moyenneMax;

                        //on met à jour le rang dans la BD
                        if (resultatAnnuelDA.rechercher(LResultatAnnuels.ElementAt(i)) != null)
                            resultatAnnuelDA.supprimer(LResultatAnnuels.ElementAt(i));

                        resultatAnnuelDA.ajouter(LResultatAnnuels.ElementAt(i));

                    }

                    //------------------- FIN mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax
                }
            }

            journalDA.journaliser("Calcul du résultat Annuel de l'élève de matricule  " + matricule + ", année : " + annee);

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

        public String getClasseEleve(String matricule, int annee)
        {
            return classeDA.getClasseEleve(matricule, annee);
        }

        public List<MatiereBE> ListeMatiereDuneClasse(ClasseBE classe, int annee)
        {
            return classeDA.ListeMatiereDuneClasse(classe, annee);
        }

        //fonction qui liste les élèves d'une classe
        // liste les élèves d'une classe à une année
        public List<EleveBE> listeEleves(ClasseBE classe, int annee)
        {
            return classeDA.listeEleves(classe, annee);
        }

    }
}
