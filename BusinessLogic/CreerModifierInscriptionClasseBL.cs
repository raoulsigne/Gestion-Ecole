using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;
using Ecole.ClasseConception;

namespace Ecole.BusinessLogic
{
    class CreerModifierInscriptionClasseBL
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

        public CreerModifierInscriptionClasseBL()
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

        //creer une Inscription
        public bool creerInscrire(string codeClasse, string matricule, int annee)
        {
            InscrireBE inscrireBE = new InscrireBE(codeClasse, matricule, annee);
            if (inscrireDA.ajouter(inscrireBE))
            {
                journalDA.journaliser("inscription de l'élève de matricule " + matricule + " dans la classe " + codeClasse + " pour l'année "+annee);
                return true;
            }
            return false;
        }

        //creer une ligne dans la table Appartenir
        public bool creerAppartenir(string code, string matricule, int annee)
        {
            AppartenirBE appartenirBE = new AppartenirBE(code, matricule, annee);
            if (appartenirDA.ajouter(appartenirBE))
            {
                journalDA.journaliser("enregistrement de la catégorie "+ code +" de l'élève de matricule " + matricule + " pour l'année " + annee);
                return true;
            }
            return false;
        }

        // supprimer une Inscription
        public bool supprinerInscrire(InscrireBE inscrire)
        {
            if (inscrireDA.supprimer(inscrire))
            {
                journalDA.journaliser("suppression de l'inscription de l'élève de matricule " + inscrire.matricule + " dans la classe " + inscrire.codeClasse + " pour l'année " + inscrire.annee);
                return true;
            }
            return false;
        }

        // supprimer une Appartenir
        public bool supprinerAppartenir(AppartenirBE appartenir)
        {
            if (appartenirDA.supprimer(appartenir))
            {
                journalDA.journaliser("suppression de la catégorie " + appartenir.codeCatEleve + " de l'élève de matricule " + appartenir.matricule + " pour l'année " + appartenir.annee);
                return true;
            }
            return false;
        }

        // modifier une Inscription
        public bool modifierInscrire(InscrireBE inscrire, InscrireBE newInscrire)
        {
            if (inscrireDA.modifier(inscrire, newInscrire))
            {
                journalDA.journaliser("modification de l'inscription de l'élève de matricule " + inscrire.matricule + " pour l'année " + inscrire.annee + ". anicenne classe : " +inscrire.codeClasse+ " nouvelle classe : " + newInscrire.codeClasse);
                return true;
            }
            return false;
        }

        // modifier un élément de la table appartenir
        public bool modifierAppartenir(AppartenirBE appartenir, AppartenirBE newAppartenir)
        {
            if (appartenirDA.modifier(appartenir, newAppartenir))
            {
                journalDA.journaliser("modification de la catégorie de l'élève de matricule " + appartenir.matricule + " pour l'année " + appartenir.annee + ". anicenne catégorie : " + appartenir.codeCatEleve + " nouvelle catégorie : " + newAppartenir.codeCatEleve);
                return true;
            }
            return false;
        }

        // modifier une Inscription
        //public bool modifierInscrire(InscrireBE inscrire)
        //{
        //    return inscrireDA.modifier(inscrire);
        //}

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

        // retourne la liste des Codes de trimestre deja enregistrés
        public List<string> getListCodeTrimestre(List<TrimestreBE> listTrimestr)
        {
            List<string> listeCodeTrimestre = new List<string>();

            listeCodeTrimestre = new List<string>();
            listeCodeTrimestre.Add("<Tous Les Trimestres>");

            if (listTrimestr != null)
            {
                for (int i = 0; i < listTrimestr.Count; i++)
                {
                    listeCodeTrimestre.Add(listTrimestr.ElementAt(i).codetrimestre);
                }
                return listeCodeTrimestre;
            }
            else return null;
        }

        // retourne la liste des Codes de Catégorie d'élèves deja enregistrés
        public List<string> getListCodeCategorieEleve(List<CategorieEleveBE> listCatEleve)
        {
            List<string> listeCodeCatElele = new List<string>();

            listeCodeCatElele = new List<string>();
            if (listCatEleve != null)
            {
                for (int i = 0; i < listCatEleve.Count; i++)
                {
                    listeCodeCatElele.Add(listCatEleve.ElementAt(i).codeCatEleve);
                }
                return listeCodeCatElele;
            }
            else return null;
        }

        // retourne la catégorie d'un élève pour une année
        public String getCategorieEleve(String matricule, int annee) {
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

        //recherche et retourne la liste des moyennes séquentielle d'un élève pour une année
        public List<MoyennesBE> moyennesSequentiellesEleve(String matricule, int annee, String codeSequence) {
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


        public String getClasseEleve(String matricule, int annee) {
            return classeDA.getClasseEleve(matricule, annee);
        }

        //recherche le dernier résultat annuel d'un élève
        public ResultatAnnuelBE RechercherResultatsAnnuelsDunEleve(String matricule) {
            return resultatAnnuelDA.RechercherResultatsAnnuelsDunEleve(matricule);
        }

         // rechercher une classe en fonction d'un niveau
        public List<String> getCodeClasseByNiveau(int niveau) {
            return classeDA.getCodeClasseByNiveau(niveau);
        }

        //retourne le profil académique d'un élève
        //public List<ProfilAcademiqueBE> getProfilAcademiqueEleve(String matricule) { 
        //    return eleveDA.
        //}


        internal List<EleveBE> listerElevesDuneClasse(string codeclasse, int annee)
        {
            ClasseBE c = new ClasseBE();
            c.codeClasse = codeclasse;
            c = classeDA.rechercher(c);
            return classeDA.listeEleves(c, annee);
        }

        internal List<string> listerValeurColonneClasse(string p)
        {
            return classeDA.listerValeursColonne(p);
        }

        // suprimer un élève
        public bool supprimerEleve(EleveBE eleve)
        {
            if (eleveDA.supprimer(eleve))
            {
                journalDA.journaliser("suppresion de l'élève de matricule " + eleve.matricule);
                return true;
            }
            return false;
        }

        // enegistrer un élève
        public bool enregistrerEleve(EleveBE eleve)
        {
            if (eleveDA.ajouter(eleve))
            {
                journalDA.journaliser("Enregistrement d'un élève de matricule " + eleve.matricule);
                return true;
            }
            return false;
        }

    }
}
