using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;
using Ecole.ClasseConception;
using Ecole.Utilitaire;

namespace Ecole.BusinessLogic
{
    class GenererprofilAcademiqueBL
    {
        private InscrireDA inscrireDA;
        private EleveDA eleveDA;
        private AppartenirDA appartenirDA;
        private ClasseDA classeDA;
        private CategorieEleveDA categorieEleveDA;
        private ParametresDA parametreDA;
        private SequenceDA sequenceDA;
        private MoyennesDA moyennesDA;
        private TrimestreDA trimestreDA;
        private MoyennesTrimestrielsDA moyennesTrimestrielsDA;
        private ResultatDA resultatDA;
        private ResultatTrimestrielDA resultatTrimestrielDA;
        private ResultatAnnuelDA resultatAnnuelDA;
        private JournalDA journalDA;

        public GenererprofilAcademiqueBL()
        {
            this.inscrireDA = new InscrireDA();
            this.eleveDA = new EleveDA();
            this.appartenirDA = new AppartenirDA();
            this.classeDA = new ClasseDA();
            this.categorieEleveDA = new CategorieEleveDA();
            this.parametreDA = new ParametresDA();
            this.sequenceDA = new SequenceDA();
            this.moyennesDA = new MoyennesDA();
            this.trimestreDA = new TrimestreDA();
            this.moyennesTrimestrielsDA = new MoyennesTrimestrielsDA();
            this.resultatDA = new ResultatDA();
            this.resultatTrimestrielDA = new ResultatTrimestrielDA();
            this.resultatAnnuelDA = new ResultatAnnuelDA();
            this.journalDA = new JournalDA();
        }

        // rechercher une Inscription
        public InscrireBE rechercherInscrire(InscrireBE inscrire)
        {
            return inscrireDA.rechercher(inscrire);
        }

        // rechercher un Inscrire
        public InscrireBE rechercherInscrireSuivantCritere(String critere)
        {
            List<InscrireBE> LInscrire = inscrireDA.listerSuivantCritere(critere);
            if (LInscrire != null)
                if (LInscrire.Count != 0)
                    return LInscrire[0];
                else
                    return null;
            else return null;
        }

        // rechercher un Appartenir
        public AppartenirBE rechercherAppartenir(AppartenirBE appartenir)
        {
            return appartenirDA.rechercher(appartenir);
        }

        // rechercher un Appartenir
        public AppartenirBE rechercherAppartenirSuivantCritere(String critere)
        {
            List<AppartenirBE> LAppartenir = appartenirDA.listerSuivantCritere(critere);
            if (LAppartenir != null)
                if (LAppartenir.Count != 0)
                    return LAppartenir[0];
                else
                    return null;
            else return null;
        }

        // rechercher un CategorieEleve
        public CategorieEleveBE rechercherCategorieEleve(CategorieEleveBE categorieEleve)
        {
            return categorieEleveDA.rechercher(categorieEleve);
        }

        // rechercher un élève
        public EleveBE rechercherEleve(EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
        }

        //lister toutes les Inscriptions
        public List<InscrireBE> listerTousLesInscrires()
        {
            return inscrireDA.listerTous();
        }

        // lister toutes les Inscriptions respectant un certain critère
        public List<InscrireBE> listerInscrireSuivantCritere(string critere)
        {
            return inscrireDA.listerSuivantCritere(critere);
        }

        //lister toutes les Séquences
        public List<SequenceBE> listerToutesLesSequences()
        {
            return sequenceDA.listerTous();
        }

        // lister toutes les Séquences respectant un certain critère
        public List<SequenceBE> listerSequencesSuivantCritere(string critere)
        {
            return sequenceDA.listerSuivantCritere(critere);
        }

        //lister tous les Trimestres
        public List<TrimestreBE> listerTousLesTrimestres()
        {
            return trimestreDA.listerTous();
        }

        // lister toutes les Trimestres respectant un certain critère
        public List<TrimestreBE> listerTrimestresSuivantCritere(string critere)
        {
            return trimestreDA.listerSuivantCritere(critere);
        }

        //lister toutes les Classe
        public List<ClasseBE> listerToutesLesClasses()
        {
            return classeDA.listerTous();
        }

        //lister toutes les Catgories d'élève
        public List<CategorieEleveBE> listerToutesLesCategoriesELeves()
        {
            return categorieEleveDA.listerTous();
        }

        // retourne la liste des Codes de Classe deja enregistrés
        public List<string> getListCodeClasse(List<ClasseBE> listClasse)
        {
            List<string> listeCodeClasse = new List<string>();

            listeCodeClasse = new List<string>();
            listeCodeClasse.Add("<Toutes les classes>");
            if (listClasse != null)
            {
                for (int i = 0; i < listClasse.Count; i++)
                {
                    listeCodeClasse.Add(listClasse.ElementAt(i).codeClasse);
                }
                return listeCodeClasse;
            }
            else return null;
        }

        // retourne la liste des Codes de Séquence deja enregistrés
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


        // retourne la catégorie d'un élève pour une année
        public String getCategorieEleve(String matricule, int annee)
        {
            List<AppartenirBE> LAppartenir = appartenirDA.listerSuivantCritere("matricule ='" + matricule + "' AND annee ='" + annee + "'");
            if (LAppartenir != null)
            {
                if (LAppartenir.Count != 0)
                {
                    return LAppartenir.ElementAt(0).codeCatEleve;
                }
                else return null;
            }
            else return null;
        }

        //retouner les paramètres
        public ParametresBE getParametres()
        {
            List<ParametresBE> LParametre = parametreDA.listerTous();
            if (LParametre != null)
            {
                if (LParametre.Count != 0)
                {
                    return LParametre.ElementAt(0);
                }
                else return null;
            }
            else return null;
        }

        // retourne l'année du système
        public int getAnneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        //recherche et retourne la liste des moyennes séquentielle d'un élève pour une année
        public List<MoyennesBE> moyennesSequentiellesEleve(String matricule, int annee, String codeSequence)
        {
            return moyennesDA.moyennesSequentiellesEleve(matricule, annee, codeSequence);
        }

        //recherche et retourne la liste de toutes les moyennes séquentielle d'un élève
        public List<MoyennesBE> listeMoyennesEleve(String matricule)
        {
            return moyennesDA.listeMoyennesEleve(matricule);
        }

        //recherche et retourne la liste des moyennes Trimestrielles d'un élève
        public List<MoyennesTrimestrielsBE> moyennesTrimestriellesEleve(String matricule, int annee, String codeTrimestre)
        {
            return moyennesTrimestrielsDA.moyennesTrimestriellesEleve(matricule, annee, codeTrimestre);
        }

        //recherche et retourne la liste des resultats Séquentiels d'un élève
        public List<ResultatBE> resultatsSequentiellesEleve(String matricule, int annee, String codeSequence)
        {
            return resultatDA.resultatsSequentiellesEleve(matricule, annee, codeSequence);
        }

        //recherche et retourne la liste des resultats Séquentiels d'un élève
        public List<ResultatTrimestrielBE> resultatsTrimestrielsEleve(String matricule, int annee, String codeTrimestre)
        {
            return resultatTrimestrielDA.resultatsTrimestrielsEleve(matricule, annee, codeTrimestre);
        }


        public String getClasseEleve(String matricule, int annee)
        {
            return classeDA.getClasseEleve(matricule, annee);
        }

        //recherche le dernier résultat annuel d'un élève
        public ResultatAnnuelBE RechercherResultatsAnnuelsDunEleve(String matricule)
        {
            return resultatAnnuelDA.RechercherResultatsAnnuelsDunEleve(matricule);
        }

        // rechercher une classe en fonction d'un niveau
        public List<String> getCodeClasseByNiveau(int niveau)
        {
            return classeDA.getCodeClasseByNiveau(niveau);
        }

        //fonction qui liste les élèves d'une classe
        // liste les élèves d'une classe à une année
        public List<EleveBE> listeEleves(ClasseBE classe, int annee)
        {
            return classeDA.listeEleves(classe, annee);
        }

        //retourne le profil académique d'un élève
        //public List<ProfilAcademiqueBE> getProfilAcademiqueEleve(String matricule) { 
        //    return eleveDA.
        //}

        //fonction qui génère le profil académique d'un élève
        public void genererProfilAcademiqueDunEleve(String matricule) {
            List<LigneProfilAcademique> LLigneProfilAcademique = moyennesDA.infosProfilAcademique(matricule);
            EleveBE eleve = new EleveBE();
            eleve.matricule = matricule;
            eleve = eleveDA.rechercher(eleve);

            ClasseBE classe = null;

            //on recherche la classe actuelle de l'élève
            ParametresBE param = parametreDA.getParametre();
            if(param != null){
                List<InscrireBE> LInscrire = inscrireDA.listerSuivantCritere("matricule = '"+matricule+"' AND annee = '"+param.annee+"'");
                
                if(LInscrire != null && LInscrire.Count != 0){
                    classe = new ClasseBE();
                    classe.codeClasse = LInscrire.ElementAt(0).codeClasse;
                    classe = classeDA.rechercher(classe);
                    
                }
            }

            if (eleve != null)
            {
                CreerEtat etat = new CreerEtat("Profil_Scolaire - " + eleve.matricule, "PROFIL SCOLAIRE ");
                etat.etatProfilAcademiqueDunEleve(LLigneProfilAcademique, eleve, classe);
                
                journalDA.journaliser("impression du profil académique de l'élève de matricule " + eleve.matricule);
            }
        }

        //fonction qui recherche les élèves inscrits à une classe pour une année
        public List<EleveBE> ListeDesElevesDuneClasse(String codeClasse, int annee) {
            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;
            classe = classeDA.rechercher(classe);
            if (classe != null)
                return classeDA.listeEleves(classe, annee);
            else return null;
        }

    }
}
