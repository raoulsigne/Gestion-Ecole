using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    public class GestionPrestationBL
    {
        private EleveDA eleveDA;
        private PrestationDA prestationDA;
        private TrancheDA trancheDA;
        private PayerDA payerDA;
        private AppartenirDA appartenirDA;
        private MontantTrancheDA montanttrancheDA;
        ParametresDA parametreDA;
        TypeclasseDA typeclasseDA;
        ClasseDA classeDA;
        InscrireDA inscrireDA;
        CategorieEleveDA categorieEleveDA;
        JournalDA journalDA;

        public GestionPrestationBL()
        {
            eleveDA = new EleveDA();
            prestationDA = new PrestationDA();
            trancheDA = new TrancheDA();
            payerDA = new PayerDA();
            appartenirDA = new AppartenirDA();
            montanttrancheDA = new MontantTrancheDA();
            parametreDA = new ParametresDA();
            inscrireDA = new InscrireDA();
            classeDA = new ClasseDA();
            typeclasseDA = new TypeclasseDA();
            categorieEleveDA = new CategorieEleveDA();
            journalDA = new JournalDA();
        }

        internal List<String> listerValeursColonnePrestation(string p)
        {
            return prestationDA.listerValeursColonne(p);
        }

        internal List<String>  listerValeursColonneTranche(string p)
        {
            return trancheDA.listerValeursColonne(p);
        }

        internal EleveBE rechercherEleve(EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
        }

        internal PrestationBE rechercherPrestation(PrestationBE p)
        {
            return prestationDA.rechercher(p);
        }

        internal AppartenirBE rechercherAppartenir(AppartenirBE appartenir)
        {
            return appartenirDA.rechercher(appartenir);
        }

        internal List<MontantTrancheBE> listerSuivantCritereMontanttranches(string critere)
        {
            return montanttrancheDA.listerSuivantCritere(critere);
        }

        internal List<PayerBE> listerSuivantCriterePayer(string p)
        {
            return payerDA.listerSuivantCritere(p);
        }

        internal bool ajouterAcheterPayer(PayerBE payer)
        {
            if (payerDA.ajouter(payer))
            {
                journalDA.journaliser("Vente d'une prestation - " + payer.codePrestation + " - " + payer.matricule);
                return true;
            }
            else
                return false;
        }

        internal void supprimerPayer(PayerBE payer)
        {
            if (payerDA.supprimer(payer))
            {
                journalDA.journaliser("Annulation de la vente d'une prestation - " + payer.codePrestation + " - " + payer.matricule);
            }
        }

        internal MontantTrancheBE rechercherMontantTranche(MontantTrancheBE t)
        {
            return montanttrancheDA.rechercher(t);
        }

        //internal bool modifierPayer(PayerBE pa, PayerBE payer)
        //{
        //    if (payerDA.modifier(pa, payer))
        //    {
        //        journalDA.journaliser("Modification de la vente d'une prestation - " + payer.codePrestation + " - " + payer.matricule);
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        internal bool modifierPayer(PayerBE payer, PayerBE net_payer)
        {
            if (payerDA.modifier(payer, net_payer))
            {
                journalDA.journaliser("Modification de la vente d'une prestation - " + payer.codePrestation + " - " + payer.matricule);
                return true;
            }
            else
                return false;
        }

        internal int AnneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        internal PayerBE rechercherPayer(PayerBE payer)
        {
            return payerDA.rechercher(payer);
        }

        internal List<PayerBE> listerTrancheNonPayees(string categorie, string matricule, int annee)
        {
            return montanttrancheDA.tranchesNonReglees(categorie, matricule, annee.ToString());
        }

        internal List<PrestationBE> listerSuivantCriterePrestation(string p)
        {
            return prestationDA.listerSuivantCritere(p);
        }

        internal decimal obtenirFraisInscription(EleveBE eleve)
        {
            InscrireBE inscrire = new InscrireBE();
            inscrire.matricule = eleve.matricule;
            inscrire.annee = parametreDA.AnneeEnCours();
            inscrire = inscrireDA.rechercherClasse(inscrire);

            ClasseBE classe = new ClasseBE();
            classe.codeClasse = inscrire.codeClasse;
            classe = classeDA.rechercher(classe);

            TypeclasseBE type = new TypeclasseBE();
            type.codetypeclasse = classe.codeTypeClasse;
            type = typeclasseDA.rechercher(type);

            return type.fraisinscription;
        }

        internal decimal obtenirMontantPrestation(string p,string categorie, int annee)
        {
            return montanttrancheDA.montantPrestation(p, categorie, annee);
        }

        public AppartenirBE rechercherCategorie(AppartenirBE appartenir)
        {
            return appartenirDA.rechercherCategorie(appartenir);
        }

        internal CategorieEleveBE rechercherCategorieEleve(CategorieEleveBE cat)
        {
            return categorieEleveDA.rechercher(cat);
        }


        internal List<PrestationBE> listerTousPrestation()
        {
            return prestationDA.listerTous();
        }

        internal TrancheBE rechercherTranche(TrancheBE trancheBE)
        {
            return trancheDA.rechercher(trancheBE);
        }

        internal void journaliser(string p)
        {
            journalDA.journaliser(p);
        }

        internal string rechercherNumeroPayer(PayerBE payerBE)
        {
            return payerDA.rechercherNumeroPayer(payerBE).ToString();
        }

        internal List<string> listerValeursColonneClasse(string p)
        {
            return classeDA.listerValeursColonne(p);
        }

        internal InscrireBE rechercherInscrire(InscrireBE inscrire)
        {
            return inscrireDA.rechercherClasse(inscrire);
        }

        internal List<InscrireBE> listerSuivantCritereInscrits(string p)
        {
            return inscrireDA.listerSuivantCritere(p);
        }

        internal List<EleveBE> listerElevesDuneClasse(string codeclasse, int annee)
        {
            ClasseBE c = new ClasseBE();
            c.codeClasse = codeclasse;
            c = classeDA.rechercher(c);
            return classeDA.listeEleves(c, annee);
        }

        public bool enregistrer_versement(string matricule, string libelle, double montant, DateTime date, int annee)
        {
            return payerDA.enregistrer_versement(matricule, libelle, montant, date, annee);
        }
    }
}
