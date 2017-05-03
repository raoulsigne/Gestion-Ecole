using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class GenererResultatsSequentielsBL
    {
        private MoyennesDA moyennesDA;
        private ClasseDA classeDA;
        private SequenceDA sequenceDA;
        private EvaluerDA evaluerDA;
        private NotesDA notesDA;
        private ProgrammerDA programmerDA;
        private ResultatDA resultatDA;
        private MentionDA mentionDA;
        private ParametresDA parametresDA;
        private JournalDA journalDA;
        private EleveDA eleveDA;
             
        public GenererResultatsSequentielsBL()
        {
            this.moyennesDA = new MoyennesDA();
            this.classeDA = new ClasseDA();
            this.sequenceDA = new SequenceDA();
            this.evaluerDA = new EvaluerDA();
            this.notesDA = new NotesDA();
            this.programmerDA = new ProgrammerDA();
            this.resultatDA = new ResultatDA();
            this.mentionDA = new MentionDA();
            this.parametresDA = new ParametresDA();
            this.journalDA = new JournalDA();
            eleveDA = new EleveDA();
        }

        //creer une Moyenne
        public bool creerMoyenne(String codeMatiere, String codeSequence, String matricule, Double moyenne, int annee, int rang, double moyenneClasse, string mention, double moyenneMin, double moyenneMax, string appreciation)
        {
            MoyennesBE moy = new MoyennesBE(codeMatiere, codeSequence, matricule, moyenne, annee, rang, moyenneClasse, mention, moyenneMin, moyenneMax, appreciation);
            if (moyennesDA.ajouter(moy))
            {
                journalDA.journaliser("enregistrement de la moyenne séquentielle de l'élève de matricule " + matricule + ". matière : " + codeMatiere + ", année : " + annee + ", séquence : " + codeSequence + ", moyenne : " + moyenne);
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
                journalDA.journaliser("modification de la moyenne séquentielle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee + ", séquence : " + moyenne.codeSeq + ". ancienne moyenne : " + moyenne.moyenne + ", nouvelle moyenne : " + moyenne.moyenne);
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

        //Calcul de la moyenne des élèves pour une classe, une séquence, une matière et une année donnée
        //il s'agit de remplir la table "moyennes"
        public void calculerMoyenne(String codeClasse, String codeSequence, int annee) {
            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;

            ClasseBE classeBE = rechercherClasse(classe);

            //on liste les matières de la classe pour l'année choisi
            List<MatiereBE> LMatiere = listeDesMatieresDuneClasse(classeBE, annee);

            //on calcul les moyennes trimestrielles par matières
            for (int j = 0; j < LMatiere.Count; j++)
            {
                string codemat = LMatiere.ElementAt(j).codeMat;
                // calcul de la moyenne Sequentielle des élèves sur chacune des matières individuellement
                List<String[]> List = moyennesDA.calculMoyenneSequentielle(codeClasse, LMatiere.ElementAt(j).codeMat, codeSequence, annee);
                if (List != null && List.Count != 0)
                {
                    //on fabrique les objets de type moyennes Sequentielle et on les mets dans le BD
                    List<MoyennesBE> LMoyennesSequentielle = new List<MoyennesBE>();
                    for (int i = 0; i < List.Count; i++)
                    {
                        /*
                         * List.ElementAt(i)[0] : matricule;
                           List.ElementAt(i)[1] : codeMatiere;
                           List.ElementAt(i)[2] : codeseq;
                         * List.ElementAt(i)[3] : moyenne;
                         */
                        MoyennesBE moyenneSequentielle = new MoyennesBE();
                        moyenneSequentielle.codeMat = List.ElementAt(i)[1];
                        moyenneSequentielle.codeSeq = codeSequence;
                        moyenneSequentielle.matricule = List.ElementAt(i)[0];
                        moyenneSequentielle.moyenne = Convert.ToDouble(List.ElementAt(i)[3]);
                        moyenneSequentielle.annee = annee;
                        moyenneSequentielle.rang = 0;
                        moyenneSequentielle.moyenneClasse = 0;

                        if (getMention(moyenneSequentielle.moyenne) != null)
                            moyenneSequentielle.mention = getMention(moyenneSequentielle.moyenne);
                        else moyenneSequentielle.mention = "";

                        moyenneSequentielle.moyenneMin = 0;
                        moyenneSequentielle.moyenneMax = 0;

                        LMoyennesSequentielle.Add(moyenneSequentielle);


                    }

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

            journalDA.journaliser("Calcul de la moyenne séquentielle des élèves de la classe : " + codeClasse + ", séquence : " + codeSequence + ", année : " + annee);

        }


        //Calcul du résultats des élèves
        // il s'agit du calcul de la note des élèves à une séquence pour une année donnée, en prenant en compte les notes
        // de l'élève sur toutes les évaluations des différentes matières ainsi que de leurs coéfficient

        //il s'agit de remplir la table "Résultat"
        public void calculerResultats(String codeClasse, String codeSequence, int annee) {
            List<EleveBE> listeEleve = eleveDA.listeEleveDuneClasse(codeClasse, annee);
            GenererBulletinsSequentielBL genererBulletinBL = new GenererBulletinsSequentielBL();
            foreach (EleveBE e in listeEleve)
                genererBulletinBL.DefinirLesAvertissementsEtBlamesDunEleve(e.matricule, codeSequence, annee);

            // calcul de la moyenne Sequentielle des élèves sur chacune des matières individuellement
            List<String[]> List = resultatDA.calculResultatSequentiel(codeClasse, codeSequence, annee);
            if (List != null && List.Count != 0)
            {
                //on fabrique les objets de type Resultat Sequentiel et on les mets dans le BD
                List<ResultatBE> LResultatSequentiels = new List<ResultatBE>();
                for (int i = 0; i < List.Count; i++)
                {
                    /*
                     * List.ElementAt(i)[0] : matricule;
                       List.ElementAt(i)[1] : codeSequence;
                       List.ElementAt(i)[2] : annee;
                     * List.ElementAt(i)[3] : totalPoint; // le total des points de l'élève
                     * List.ElementAt(i)[4] : moyenne; // la moyenne de l'élève
                     * List.ElementAt(i)[5] : coef; // la somme des coeficients
                     */

                    ResultatBE resultatSequentiel = new ResultatBE();
                    resultatSequentiel.codeseq = codeSequence;
                    resultatSequentiel.matricule = List.ElementAt(i)[0];
                    resultatSequentiel.point = Convert.ToDouble(List.ElementAt(i)[3]);
                    resultatSequentiel.coef = Convert.ToInt16(List.ElementAt(i)[5]);
                    resultatSequentiel.moyenne = Convert.ToDouble(List.ElementAt(i)[4]);
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

                    LResultatSequentiels.Add(resultatSequentiel);


                }

                //-------------- on calcule le rang, la moyenne générale de la classe, les moyennes min et max

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

            journalDA.journaliser("Calcul des résultats séquentiels des élèves de la classe : " + codeClasse + ", séquence : " + codeSequence + ", année : " + annee);

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
        public List<EleveBE> listeEleves(ClasseBE classe, int annee)
        {
            return classeDA.listeEleves(classe, annee);
        }
    }
}
