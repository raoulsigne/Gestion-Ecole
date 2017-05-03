using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class GenererResultatsAnnuelsBL
    {
        private MoyennesTrimestrielsDA moyennesTrimestrielsDA;
        private MoyennesAnnuellesDA moyennesAnnuellesDA;
        private ClasseDA classeDA;
        private SequenceDA sequenceDA;
        private TrimestreDA trimestreDA;
        private EvaluerDA evaluerDA;
        private NotesDA notesDA;
        private ProgrammerDA programmerDA;
        private ResultatDA resultatDA;
        private ResultatAnnuelDA resultatAnnuelDA;
        private MentionDA mentionDA;
        private ParametresDA parametresDA;
        private EleveDA eleveDA;
        private JournalDA journalDA;

        public GenererResultatsAnnuelsBL()
        {
            this.resultatAnnuelDA = new ResultatAnnuelDA();
            this.moyennesTrimestrielsDA = new MoyennesTrimestrielsDA();
            this.moyennesAnnuellesDA = new MoyennesAnnuellesDA();
            this.classeDA = new ClasseDA();
            this.sequenceDA = new SequenceDA();
            this.trimestreDA =  new TrimestreDA();
            this.evaluerDA = new EvaluerDA();
            this.notesDA = new NotesDA();
            this.programmerDA = new ProgrammerDA();
            this.resultatDA = new ResultatDA();
            this.mentionDA = new MentionDA();
            this.parametresDA = new ParametresDA();
            this.eleveDA = new EleveDA();
            this.journalDA = new JournalDA();

        }

        //creer une Moyenne Annuelle
        public bool creerMoyenneAnnuelle(String codeMatiere, String matricule, Double moyenne, int annee, int rang, double moyenneClasse, string mention, double moyenneMin, double moyenneMax, string appreciation)
        {
            MoyennesAnnuellesBE moy = new MoyennesAnnuellesBE(codeMatiere, matricule, moyenne, annee, rang, moyenneClasse, mention, moyenneMin, moyenneMax, appreciation);
            if (moyennesAnnuellesDA.ajouter(moy))
            {
                journalDA.journaliser("enregistrement de la moyenne annuelle de l'élève de matricule " + matricule + ". matière : " + codeMatiere +", année : "+ annee +", moyenne : "+ moyenne);
                return true;
            }
            return false;
        }

        // supprimer une Moyenne Annuelle
        public bool supprinerMoyenneAnnuelle(MoyennesAnnuellesBE moyenne)
        {
            if (moyennesAnnuellesDA.supprimer(moyenne))
            {
                journalDA.journaliser("suppression de la moyenne annuelle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee);
                return true;
            }
            return false;
        }

        // modifier une Moyenne Annuelle
        public bool modifierMoyenneAnnuelle(MoyennesAnnuellesBE moyenne, MoyennesAnnuellesBE newMoyenne)
        {
            if (moyennesAnnuellesDA.modifier(moyenne, newMoyenne))
            {
                journalDA.journaliser("modification de la moyenne annuelle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee +". ancienne moyenne : "+moyenne.moyenne+", nouvelle moyenne : "+newMoyenne.moyenne);
                return true;
            }
            return false;
        }

        // modifier une Moyenne Annuelle
        public bool modifierMoyenneAnnuelle(MoyennesAnnuellesBE moyenne)
        {
            if (moyennesAnnuellesDA.modifier(moyenne))
            {
                journalDA.journaliser("modification de la moyenne annuelle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee + ". ancienne moyenne : " + moyenne.moyenne + ", nouvelle moyenne : " + moyenne.moyenne);
                return true;
            }
            return false;
        }

        // rechercher une Moyenne Annuelle
        public MoyennesAnnuellesBE rechercherMoyenneAnnuelle(MoyennesAnnuellesBE moyenne)
        {
            return moyennesAnnuellesDA.rechercher(moyenne);
        }

        // ajouter une Moyenne Annuelle
        public bool ajouterMoyenneAnnuelle(MoyennesAnnuellesBE moyenne)
        {
            if (moyennesAnnuellesDA.ajouter(moyenne))
            {
                journalDA.journaliser("enregistrement de la moyenne annuelle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee + ", moyenne : " + moyenne.moyenne);
                return true;
            }
            return false;
        }

        // supprimer une Moyenne Annuelle
        public bool supprimerMoyenneAnnuelle(MoyennesAnnuellesBE moyenne)
        {
            if (moyennesAnnuellesDA.supprimer(moyenne))
            {
                journalDA.journaliser("suppression de la moyenne annuelle de l'élève de matricule " + moyenne.matricule + ". matière : " + moyenne.codeMat + ", année : " + moyenne.annee);
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

        // lister toutes les Moyennes trimestriels respectant un certain critère
        public List<MoyennesTrimestrielsBE> listerMoyennesTrimestrielsSuivantCritere(string critere)
        {
            return moyennesTrimestrielsDA.listerSuivantCritere(critere);
        }

        //lister toutes les Moyennes Annuelles
        public List<MoyennesAnnuellesBE> listerToutesLesMoyennesAnnuelles()
        {
            return moyennesAnnuellesDA.listerTous();
        }

        // lister toutes les Moyennes Annuelles respectant un certain critère
        public List<MoyennesAnnuellesBE> listerMoyennesAnnuellesSuivantCritere(string critere)
        {
            return moyennesAnnuellesDA.listerSuivantCritere(critere);
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

        //Calcul de la moyenne des élèves pour une classe, une matière et une année donnée
        //il s'agit de remplir la table "moyennesAnnuelles"
        public void calculerMoyenneAnnuelles(String codeClasse, int annee)
        {
            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;

            ClasseBE classeBE = rechercherClasse(classe);

            //on liste les matières de la classe pour l'année choisi
            List<MatiereBE> LMatiere = listeDesMatieresDuneClasse(classeBE, annee);

            //on calcul les moyennes annuelles par matières
            for (int j = 0; j < LMatiere.Count; j++)
            {

                // calcul de la moyenne trimestrielle des élève sur chacune des matières individuellement
                List<String[]> List = moyennesAnnuellesDA.calculMoyenneAnnuelles(codeClasse, LMatiere.ElementAt(j).codeMat, annee);

                if (List != null && List.Count != 0)
                {
                    //on fabrique les objets de type moyennesTrimestrielles et on les mets dans le BD
                    List<MoyennesAnnuellesBE> LMoyennesmoyenneAnnuelles = new List<MoyennesAnnuellesBE>();
                    for (int i = 0; i < List.Count; i++)
                    {
                        /*
                         * List.ElementAt(i)[0] : matricule;
                           List.ElementAt(i)[1] : codeMatiere;
                           List.ElementAt(i)[2] : moyenne;
                         */
                        MoyennesAnnuellesBE moyenneAnnuelle = new MoyennesAnnuellesBE();
                        moyenneAnnuelle.codeMat = List.ElementAt(i)[1];

                        moyenneAnnuelle.matricule = List.ElementAt(i)[0];
                        moyenneAnnuelle.moyenne = Convert.ToDouble(List.ElementAt(i)[2]);
                        moyenneAnnuelle.annee = annee;
                        moyenneAnnuelle.rang = 0;
                        moyenneAnnuelle.moyenneClasse = 0;
                        if (getMention(moyenneAnnuelle.moyenne) != null)
                            moyenneAnnuelle.mention = getMention(moyenneAnnuelle.moyenne);
                        else moyenneAnnuelle.mention = "";
                        moyenneAnnuelle.moyenneMin = 0;
                        moyenneAnnuelle.moyenneMax = 0;

                        LMoyennesmoyenneAnnuelles.Add(moyenneAnnuelle);


                    }

                    //-------------- on calcule le rang, la moyenne générale de la classe, les moyennes min et max

                    double moyenneDeLaClasse = 0; // moyenne générale de la classe
                    double moyenneMin = LMoyennesmoyenneAnnuelles.ElementAt(0).moyenne; // moyenne mininale des élèves de la classe
                    double moyenneMax = LMoyennesmoyenneAnnuelles.ElementAt(0).moyenne; // moyenne maximale des élèves de la classe

                    //on trie la liste
                    LMoyennesmoyenneAnnuelles = LMoyennesmoyenneAnnuelles.OrderByDescending(o => o.moyenne).ToList();

                    MoyennesAnnuellesBE moyennePrecedente = new MoyennesAnnuellesBE();

                    for (int i = 0; i < LMoyennesmoyenneAnnuelles.Count; i++)
                    {
                        // ------------------- DEBUT détermination du rang 
                        if (i == 0)
                        { // on est sur le premier (celui qui a la plus grande note)
                            MoyennesAnnuellesBE oldMoyenne = LMoyennesmoyenneAnnuelles.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                            LMoyennesmoyenneAnnuelles.ElementAt(i).rang = 1; // le premier pour cette séquence et cette matière

                            //on met à jour le rang dans la BD
                            //modifierMoyenne(oldMoyenne, LMoyennesmoyenneAnnuelles.ElementAt(j));

                            moyennePrecedente = LMoyennesmoyenneAnnuelles.ElementAt(i);

                        }
                        else
                        {

                            if (LMoyennesmoyenneAnnuelles.ElementAt(i).moyenne == moyennePrecedente.moyenne)
                            {
                                MoyennesAnnuellesBE oldMoyenne = LMoyennesmoyenneAnnuelles.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                //alors on a un ex éco (ils ont le même rang)
                                LMoyennesmoyenneAnnuelles.ElementAt(i).rang = moyennePrecedente.rang;

                                //on met à jour le rang dans la BD
                                //modifierMoyenne(oldMoyenne, LMoyenneAvecRang.ElementAt(j));

                            }
                            else
                            {


                                MoyennesAnnuellesBE oldMoyenne = LMoyennesmoyenneAnnuelles.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                                //alors l'élève prend le rend suivant
                                //LMoyennesmoyenneAnnuelles.ElementAt(j).rang = moyennePrecedente.rang + 1;
                                LMoyennesmoyenneAnnuelles.ElementAt(i).rang = i + 1;

                                //on met à jour le rang dans la BD
                                //modifierMoyenne(oldMoyenne, LMoyenneAvecRang.ElementAt(j));

                            }

                            moyennePrecedente = LMoyennesmoyenneAnnuelles.ElementAt(i);
                        }
                        // ------------------- FIN détermination du rang 

                        // ------------------- DEBUT détermination de la moyenne de la classe

                        moyenneDeLaClasse = moyenneDeLaClasse + LMoyennesmoyenneAnnuelles.ElementAt(i).moyenne;

                        // ------------------- FIN détermination de la moyenne de la classe

                        

                        // ------------------- DEBUT détermination des moyennes minimales et maximales 

                        if (LMoyennesmoyenneAnnuelles.ElementAt(i).moyenne < moyenneMin)
                            moyenneMin = LMoyennesmoyenneAnnuelles.ElementAt(i).moyenne;

                        if (LMoyennesmoyenneAnnuelles.ElementAt(i).moyenne > moyenneMax)
                            moyenneMax = LMoyennesmoyenneAnnuelles.ElementAt(i).moyenne;

                        // ------------------- FIN détermination des moyennes minimales et maximales 
                    }

                    moyenneDeLaClasse = moyenneDeLaClasse / LMoyennesmoyenneAnnuelles.Count;

                    //------------------- DEBUT mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax

                    for (int i = 0; i < LMoyennesmoyenneAnnuelles.Count; i++)
                    {
                        LMoyennesmoyenneAnnuelles.ElementAt(i).moyenneClasse = moyenneDeLaClasse;
                        LMoyennesmoyenneAnnuelles.ElementAt(i).moyenneMin = moyenneMin;
                        LMoyennesmoyenneAnnuelles.ElementAt(i).moyenneMax = moyenneMax;

                        //on met à jour le rang dans la BD
                        if (moyennesAnnuellesDA.rechercher(LMoyennesmoyenneAnnuelles.ElementAt(i)) != null)
                            supprimerMoyenneAnnuelle(LMoyennesmoyenneAnnuelles.ElementAt(i));

                        ajouterMoyenneAnnuelle(LMoyennesmoyenneAnnuelles.ElementAt(i));

                        //modifierMoyenneTrimestriel(LMoyennesTrimestriels.ElementAt(i), LMoyennesTrimestriels.ElementAt(i));
                    }

                    //------------------- FIN mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax

                }
            }

            journalDA.journaliser("Calcul des moyennes annuelles des élèves de la classe " + codeClasse + ", année " + annee);

        }


        //Calcul du résultats des élèves
        // il s'agit du calcul de la note des élèves à une séquence pour une année donnée, en prenant en compte les notes
        // de l'élève sur toutes les évaluations des différentes matières ainsi que de leurs coéfficient

        //il s'agit de remplir la table "Annuelles"
        public void calculerResultatsAnnuelles(String codeClasse, int annee)
        {
            // calcul de la moyenne trimestrielle des élèves sur chacune des matières individuellement
            List<String[]> List = resultatAnnuelDA.calculResultatAnnuels(codeClasse, annee);
            if (List != null && List.Count != 0)
            {
                //on fabrique les objets de type ResultatAnnuelBE et on les mets dans le BD
                List<ResultatAnnuelBE> LResultatAnnuelBE = new List<ResultatAnnuelBE>();
                for (int i = 0; i < List.Count; i++)
                {
                    /*
                     * List.ElementAt(i)[0] : matricule;
                       List.ElementAt(i)[1] : annee;
                     * List.ElementAt(i)[2] : totalPoint; // le total des points de l'élève
                     * List.ElementAt(i)[3] : moyenne; // la moyenne de l'élève
                     * List.ElementAt(i)[4] : coef; // la somme des coeficients
                     */

                    ResultatAnnuelBE resultatAnnuel = new ResultatAnnuelBE();
                    resultatAnnuel.matricule = List.ElementAt(i)[0];
                    resultatAnnuel.point = Convert.ToDouble(List.ElementAt(i)[2]);
                    resultatAnnuel.coef = Convert.ToInt16(List.ElementAt(i)[4]);
                    resultatAnnuel.moyenne = Convert.ToDouble(List.ElementAt(i)[3]);
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

                    //resultatAnnuel.moyenneMin = 0;
                    //resultatAnnuel.moyenneMax = 0;

                    LResultatAnnuelBE.Add(resultatAnnuel);


                }

                //-------------- on calcule le rang, la moyenne générale de la classe, les moyennes min et max

                double moyenneDeLaClasse = 0; // moyenne générale de la classe
                //double moyenneMin = LResultatTrimestriels.ElementAt(0).moyenne; // moyenne mininale des élèves de la classe
                //double moyenneMax = LResultatTrimestriels.ElementAt(0).moyenne; // moyenne maximale des élèves de la classe

                //on trie la liste
                LResultatAnnuelBE = LResultatAnnuelBE.OrderByDescending(o => o.moyenne).ToList();

                ResultatAnnuelBE resultatPrecedent = new ResultatAnnuelBE();

                for (int i = 0; i < LResultatAnnuelBE.Count; i++)
                {
                    // ------------------- DEBUT détermination du rang 
                    if (i == 0)
                    { // on est sur le premier (celui qui a la plus grande note)
                        ResultatAnnuelBE oldResultat = LResultatAnnuelBE.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                        LResultatAnnuelBE.ElementAt(i).rang = 1; // le premier pour cette séquence et cette matière

                        //on met à jour le rang dans la BD
                        //modifierMoyenne(oldResultat, ResultatAnnuelBE.ElementAt(j));

                        resultatPrecedent = LResultatAnnuelBE.ElementAt(i);

                    }
                    else
                    {

                        if (LResultatAnnuelBE.ElementAt(i).moyenne == resultatPrecedent.moyenne)
                        {
                            ResultatAnnuelBE oldResultat = LResultatAnnuelBE.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                            //alors on a un ex éco (ils ont le même rang)
                            LResultatAnnuelBE.ElementAt(i).rang = resultatPrecedent.rang;

                            //on met à jour le rang dans la BD
                            //modifierMoyenne(oldResultat, LResultatAnnuelBE.ElementAt(j));

                        }
                        else
                        {


                            ResultatAnnuelBE oldResultat = LResultatAnnuelBE.ElementAt(i); // l'ancienne version de la moyenne (sera utilisé pour la modification)

                            //alors l'élève prend le rend suivant
                            //LResultatAnnuelBE.ElementAt(j).rang = moyennePrecedente.rang + 1;
                            LResultatAnnuelBE.ElementAt(i).rang = i + 1;

                            //on met à jour le rang dans la BD
                            //modifierMoyenne(oldResultat, LResultatAnnuelBE.ElementAt(j));

                        }

                        resultatPrecedent = LResultatAnnuelBE.ElementAt(i);
                    }
                    // ------------------- FIN détermination du rang 

                    // ------------------- DEBUT détermination de la moyenne de la classe

                    moyenneDeLaClasse = moyenneDeLaClasse + LResultatAnnuelBE.ElementAt(i).moyenne;

                    // ------------------- FIN détermination de la moyenne de la classe

                  

                    //// ------------------- DEBUT détermination des moyennes minimales et maximales 

                    //if (LResultatAnnuelBE.ElementAt(i).moyenne < moyenneMin)
                    //    moyenneMin = LResultatAnnuelBE.ElementAt(i).moyenne;

                    //if (LResultatAnnuelBE.ElementAt(i).moyenne > moyenneMax)
                    //    moyenneMax = LResultatAnnuelBE.ElementAt(i).moyenne;

                    //// ------------------- FIN détermination des moyennes minimales et maximales 
                }

                moyenneDeLaClasse = moyenneDeLaClasse / LResultatAnnuelBE.Count;

                //------------------- DEBUT mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax

                for (int i = 0; i < LResultatAnnuelBE.Count; i++)
                {
                    LResultatAnnuelBE.ElementAt(i).moyenneclasse = moyenneDeLaClasse;
                    //LResultatAnnuelBE.ElementAt(i).moyenneMin = moyenneMin;
                    //LResultatAnnuelBE.ElementAt(i).moyenneMax = moyenneMax;

                    //on met à jour le rang dans la BD
                    if (resultatAnnuelDA.rechercher(LResultatAnnuelBE.ElementAt(i)) != null)
                        resultatAnnuelDA.supprimer(LResultatAnnuelBE.ElementAt(i));

                    resultatAnnuelDA.ajouter(LResultatAnnuelBE.ElementAt(i));

                    //modifierMoyenneTrimestriel(LResultatAnnuelBE.ElementAt(i), LResultatAnnuelBE.ElementAt(i));
                }

                //------------------- FIN mise à jour des moyennes avec les infos telles que : moyenneClasse, mention, moyenneMin, moyenneMax

            }

            journalDA.journaliser("Calcul des résultats annuelles des élèves de la classe " + codeClasse + ", année " + annee);

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
