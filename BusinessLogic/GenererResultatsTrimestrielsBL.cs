using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class GenererResultatsTrimestrielsBL
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
        private JournalDA journalDA;

        public GenererResultatsTrimestrielsBL()
        {
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
            this.journalDA = new JournalDA();

        }

        //creer une Moyenne Trimestriel
        public bool creerMoyenneTrimestriel(String codeMatiere, String codeTrimestre, String matricule, Double moyenne, int annee, int rang, double moyenneClasse, string mention, double moyenneMin, double moyenneMax, string appreciation)
        {
            MoyennesTrimestrielsBE moy = new MoyennesTrimestrielsBE(codeMatiere, codeTrimestre, matricule, moyenne, annee, rang, moyenneClasse, mention, moyenneMin, moyenneMax, appreciation);
            if (moyennesTrimestrielsDA.ajouter(moy))
            {
                journalDA.journaliser("enregistrement de la moyenne trimestrielle de l'élève de matricule " + matricule + ". matière : " + codeMatiere + ", année : " + annee + ", trimestre : " + codeTrimestre + ", moyenne : " + moyenne);
                return true;
            }
            return false;
        }

        // supprimer une Moyenne Trimestriel
        public bool supprinerMoyenneTrimestriel(MoyennesTrimestrielsBE moyenne)
        {
            if (moyennesTrimestrielsDA.supprimer(moyenne))
            {
                journalDA.journaliser("suppression de la moyenne trimestrielle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee + ", trimestre : " + moyenne.codeTrimestre + ", moyenne : " + moyenne.moyenne);
                return true;
            }
            return false;
        }

        // modifier une Moyenne Trimestriel
        public bool modifierMoyenneTrimestriel(MoyennesTrimestrielsBE moyenne, MoyennesTrimestrielsBE newMoyenne)
        {
            if (moyennesTrimestrielsDA.modifier(moyenne, newMoyenne))
            {
                journalDA.journaliser("modification de la moyenne trimestrielle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee + ", trimestre : " + moyenne.codeTrimestre + ". ancienne moyenne : " + moyenne.moyenne + ", nouvelle moyenne : " + newMoyenne.moyenne);
                return true;
            }
            return false;
        }

        // modifier une Moyenne Trimestriel
        public bool modifierMoyenneTrimestriel(MoyennesTrimestrielsBE moyenne)
        {
            if (moyennesTrimestrielsDA.modifier(moyenne))
            {
                journalDA.journaliser("modification de la moyenne trimestrielle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee + ", trimestre : " + moyenne.codeTrimestre + ". ancienne moyenne : " + moyenne.moyenne + ", nouvelle moyenne : " + moyenne.moyenne);
                return true;
            }
            return false;
        }

        // rechercher une Moyenne Trimestriel
        public MoyennesTrimestrielsBE rechercherMoyenneTrimestriel(MoyennesTrimestrielsBE moyenne)
        {
            return moyennesTrimestrielsDA.rechercher(moyenne);
        }

        // ajouter une Moyenne Trimestriel
        public bool ajouterMoyenneTrimestriel(MoyennesTrimestrielsBE moyenne)
        {
            if (moyennesTrimestrielsDA.ajouter(moyenne))
            {
                journalDA.journaliser("enregistrement de la moyenne trimestrielle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee + ", trimestre : " + moyenne.codeTrimestre + ", moyenne : " + moyenne.moyenne);
                return true;
            }
            return false;
        }

        // supprimer une Moyenne Trimestriel
        public bool supprimerMoyenneTrimestriel(MoyennesTrimestrielsBE moyenne)
        {
            if (moyennesTrimestrielsDA.supprimer(moyenne))
            {
                journalDA.journaliser("suppression de la moyenne trimestrielle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee + ", trimestre : " + moyenne.codeTrimestre + ", moyenne : " + moyenne.moyenne);
                return true;
            }
            return false;
        }

        // ajouter un Resultat Trimestriel
        public bool ajouterResultatTrimestriel(ResultatTrimestrielBE resultat)
        {
            if (resultatTrimestrielDA.ajouter(resultat))
            {
                journalDA.journaliser("enregistrement du résultat trimestriel de l'élève de matricule " + resultat.matricule + ", année : " + resultat.annee + ", trimestre : " + resultat.codeTrimestre + ", moyenne : " + resultat.moyenne);
                return true;
            }
            return false;
        }

        // supprimer un Resultat Trimestriel
        public bool supprimerResultatTrimestriel(ResultatTrimestrielBE resultat)
        {
            if (resultatTrimestrielDA.supprimer(resultat))
            {
                journalDA.journaliser("suppression du résultat trimestriel de l'élève de matricule " + resultat.matricule + ", année : " + resultat.annee + ", trimestre : " + resultat.codeTrimestre + ", moyenne : " + resultat.moyenne);
                return true;
            }
            return false;
        }

        // ajouter un Resultat
        public bool ajouterResultat(ResultatBE resultat)
        {
            if (resultatDA.ajouter(resultat))
            {
                journalDA.journaliser("enregistrement du résultat séquentiel de l'élève de matricule " + resultat.matricule + ", année : " + resultat.annee + ", séquence : " + resultat.codeseq + ", moyenne : " + resultat.moyenne);
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

        //Calcul de la moyenne des élèves pour une classe, une Trimestre, une matière et une année donnée
        //il s'agit de remplir la table "moyennesTrimestriels"
        public void calculerMoyenneTrimestriels(String codeClasse, String codeTrimestre, int annee) {

            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;

            ClasseBE classeBE = rechercherClasse(classe);

            //on liste les matières de la classe pour l'année choisi
            List<MatiereBE> LMatiere = listeDesMatieresDuneClasse(classeBE, annee);

            //on calcul les moyennes trimestrielles par matières
            for(int  j=0; j < LMatiere.Count; j++){
                // calcul de la moyenne trimestrielle des élève sur chacune des matières individuellement
                List<String[]> List = moyennesTrimestrielsDA.calculMoyenneTrimestrielle(codeClasse, LMatiere.ElementAt(j).codeMat, codeTrimestre, annee);

                if (List != null && List.Count != 0)
                {
                    //on fabrique les objets de type moyennesTrimestrielles et on les mets dans le BD
                    List<MoyennesTrimestrielsBE> LMoyennesTrimestriels = new List<MoyennesTrimestrielsBE>();
                    for (int i = 0; i < List.Count; i++)
                    {
                        /*
                         * List.ElementAt(i)[0] : matricule;
                           List.ElementAt(i)[1] : codeMatiere;
                           List.ElementAt(i)[2] : moyenne;
                         */
                        MoyennesTrimestrielsBE moyenneTrimestrielle = new MoyennesTrimestrielsBE();
                        moyenneTrimestrielle.codeMat = List.ElementAt(i)[1];
                        moyenneTrimestrielle.codeTrimestre = codeTrimestre;
                        moyenneTrimestrielle.matricule = List.ElementAt(i)[0];
                        moyenneTrimestrielle.moyenne = Convert.ToDouble(List.ElementAt(i)[2]);
                        moyenneTrimestrielle.annee = annee;
                        moyenneTrimestrielle.rang = 0;
                        moyenneTrimestrielle.moyenneClasse = 0;
                        if (getMention(moyenneTrimestrielle.moyenne) != null)
                            moyenneTrimestrielle.mention = getMention(moyenneTrimestrielle.moyenne);
                        else moyenneTrimestrielle.mention = "";
                        moyenneTrimestrielle.moyenneMin = 0;
                        moyenneTrimestrielle.moyenneMax = 0;

                        LMoyennesTrimestriels.Add(moyenneTrimestrielle);


                    }

                    //-------------- on calcule le rang, la moyenne générale de la classe, les moyennes min et max

                    double moyenneDeLaClasse = 0; // moyenne générale de la classe
                    double moyenneMin = LMoyennesTrimestriels.ElementAt(0).moyenne; // moyenne mininale des élèves de la classe
                    double moyenneMax = LMoyennesTrimestriels.ElementAt(0).moyenne; // moyenne maximale des élèves de la classe

                    //on trie la liste
                    LMoyennesTrimestriels = LMoyennesTrimestriels.OrderByDescending(o => o.moyenne).ToList();

                    MoyennesTrimestrielsBE moyennePrecedente = new MoyennesTrimestrielsBE();

                    for (int i = 0; i < LMoyennesTrimestriels.Count; i++)
                    {
                        // ------------------- DEBUT détermination du rang 
                        if (i == 0)
                        { // on est sur le premier (celui qui a la plus grande note)
                            MoyennesTrimestrielsBE oldMoyenne = LMoyennesTrimestriels.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                            LMoyennesTrimestriels.ElementAt(i).rang = 1; // le premier pour cette séquence et cette matière

                            //on met à jour le rang dans la BD
                            //modifierMoyenne(oldMoyenne, LMoyenneAvecRang.ElementAt(j));

                            moyennePrecedente = LMoyennesTrimestriels.ElementAt(i);

                        }
                        else
                        {

                            if (LMoyennesTrimestriels.ElementAt(i).moyenne == moyennePrecedente.moyenne)
                            {
                                MoyennesTrimestrielsBE oldMoyenne = LMoyennesTrimestriels.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                //alors on a un ex éco (ils ont le même rang)
                                LMoyennesTrimestriels.ElementAt(i).rang = moyennePrecedente.rang;

                                //on met à jour le rang dans la BD
                                //modifierMoyenne(oldMoyenne, LMoyenneAvecRang.ElementAt(j));

                            }
                            else
                            {


                                MoyennesTrimestrielsBE oldMoyenne = LMoyennesTrimestriels.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                //alors l'élève prend le rend suivant
                                //LMoyenneAvecRang.ElementAt(j).rang = moyennePrecedente.rang + 1;
                                LMoyennesTrimestriels.ElementAt(i).rang = i + 1;

                                //on met à jour le rang dans la BD
                                //modifierMoyenne(oldMoyenne, LMoyenneAvecRang.ElementAt(j));

                            }

                            moyennePrecedente = LMoyennesTrimestriels.ElementAt(i);
                        }
                        // ------------------- FIN détermination du rang 

                        // ------------------- DEBUT détermination de la moyenne de la classe

                        moyenneDeLaClasse = moyenneDeLaClasse + LMoyennesTrimestriels.ElementAt(i).moyenne;

                        // ------------------- FIN détermination de la moyenne de la classe

                        // ------------------- DEBUT détermination des moyennes minimales et maximales 

                        if (LMoyennesTrimestriels.ElementAt(i).moyenne < moyenneMin)
                            moyenneMin = LMoyennesTrimestriels.ElementAt(i).moyenne;

                        if (LMoyennesTrimestriels.ElementAt(i).moyenne > moyenneMax)
                            moyenneMax = LMoyennesTrimestriels.ElementAt(i).moyenne;

                        // ------------------- FIN détermination des moyennes minimales et maximales 
                    }

                    moyenneDeLaClasse = moyenneDeLaClasse / LMoyennesTrimestriels.Count;

                    //------------------- DEBUT mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax

                    for (int i = 0; i < LMoyennesTrimestriels.Count; i++)
                    {
                        LMoyennesTrimestriels.ElementAt(i).moyenneClasse = moyenneDeLaClasse;
                        LMoyennesTrimestriels.ElementAt(i).moyenneMin = moyenneMin;
                        LMoyennesTrimestriels.ElementAt(i).moyenneMax = moyenneMax;

                        //on met à jour le rang dans la BD
                        if (moyennesTrimestrielsDA.rechercher(LMoyennesTrimestriels.ElementAt(i)) != null)
                            supprimerMoyenneTrimestriel(LMoyennesTrimestriels.ElementAt(i));

                        ajouterMoyenneTrimestriel(LMoyennesTrimestriels.ElementAt(i));

                        //modifierMoyenneTrimestriel(LMoyennesTrimestriels.ElementAt(i), LMoyennesTrimestriels.ElementAt(i));
                    }

                    //------------------- FIN mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax

                }
            }

            journalDA.journaliser("Calcul de la moyenne trimestrielle des élèves de la classe : " + codeClasse + ", trimestre : " + codeTrimestre + ", année : " + annee);

        }


        //Calcul du résultats des élèves
        // il s'agit du calcul de la note des élèves à une séquence pour une année donnée, en prenant en compte les notes
        // de l'élève sur toutes les évaluations des différentes matières ainsi que de leurs coéfficient

        //il s'agit de remplir la table "Résultat"
        public void calculerResultatsTrimestriels(String codeClasse, String codeTrimestre, int annee)
        {
            // calcul de la moyenne trimestrielle des élèves sur chacune des matières individuellement
            List<String[]> List = resultatTrimestrielDA.calculResultatTrimestriel(codeClasse, codeTrimestre, annee);
            if (List != null && List.Count != 0)
            {
                //on fabrique les objets de type ResultatTrimestrielles et on les mets dans le BD
                List<ResultatTrimestrielBE> LResultatTrimestriels = new List<ResultatTrimestrielBE>();
                for (int i = 0; i < List.Count; i++)
                {
                    /*
                     * List.ElementAt(i)[0] : matricule;
                       List.ElementAt(i)[1] : codeTrimestre;
                       List.ElementAt(i)[2] : annee;
                     * List.ElementAt(i)[3] : totalPoint; // le total des points de l'élève
                     * List.ElementAt(i)[4] : moyenne; // la moyenne de l'élève
                     * List.ElementAt(i)[5] : coef; // la somme des coeficients
                     */

                    ResultatTrimestrielBE resultatTrimestriel = new ResultatTrimestrielBE();
                    resultatTrimestriel.codeTrimestre = codeTrimestre;
                    resultatTrimestriel.matricule = List.ElementAt(i)[0];
                    resultatTrimestriel.point = Convert.ToDouble(List.ElementAt(i)[3]);
                    resultatTrimestriel.coef = Convert.ToInt16(List.ElementAt(i)[5]);
                    resultatTrimestriel.moyenne = Convert.ToDouble(List.ElementAt(i)[4]);
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

                    LResultatTrimestriels.Add(resultatTrimestriel);


                }

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

                   

                    //// ------------------- DEBUT détermination des moyennes minimales et maximales 

                    //if (LResultatTrimestriels.ElementAt(i).moyenne < moyenneMin)
                    //    moyenneMin = LMoyennesTrimestriels.ElementAt(i).moyenne;

                    //if (LMoyennesTrimestriels.ElementAt(i).moyenne > moyenneMax)
                    //    moyenneMax = LMoyennesTrimestriels.ElementAt(i).moyenne;

                    //// ------------------- FIN détermination des moyennes minimales et maximales 
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
                        supprimerResultatTrimestriel(LResultatTrimestriels.ElementAt(i));

                    ajouterResultatTrimestriel(LResultatTrimestriels.ElementAt(i));

                    //modifierMoyenneTrimestriel(LMoyennesTrimestriels.ElementAt(i), LMoyennesTrimestriels.ElementAt(i));
                }

                //------------------- FIN mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax

            }

            journalDA.journaliser("Calcul des résultats trimestriels des élèves de la classe : " + codeClasse + ", trimestre : " + codeTrimestre + ", année : " + annee);

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
    }
}
