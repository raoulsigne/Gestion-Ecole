using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;
using System.Globalization;
using System.Threading;
using Ecole.ClasseConception;

namespace Ecole.BusinessLogic
{
    public class GestionCaisseBL
    {
        private OperationDA operationDA;
        private TypeoperationDA typeoperationDA;
        private RealiserDA realiserDA;
        private AcheterDA acheterDA;
        private PayerDA payerDA;
        private EleveDA eleveDA;
        private PrestationDA prestationDA;
        private TrancheDA trancheDA;
        private SetarticleDA setArticleDA;
        private JournalDA journalDA;
        InscrireDA inscrireDA;

        public GestionCaisseBL()
        {
            operationDA = new OperationDA();
            typeoperationDA = new TypeoperationDA();
            realiserDA = new RealiserDA();
            acheterDA = new AcheterDA();
            payerDA = new PayerDA();
            eleveDA = new EleveDA();
            prestationDA = new PrestationDA();
            trancheDA = new TrancheDA();
            setArticleDA = new SetarticleDA();
            journalDA = new JournalDA();
            inscrireDA = new InscrireDA();
        }

        internal string getNumeroSuivant()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "ddMMyy";
            Thread.CurrentThread.CurrentCulture = ci;

            String[] tab;
            String jour = "";
            String numero = "";
            int num;
            String date = DateTime.Today.ToShortDateString();
            tab = realiserDA.rechercherMaxNumeroOP();
            jour = tab[0];
            Console.WriteLine("date -- jour--numeromax |" + date +"--" + jour + "--" + tab[1]);
            if (date == jour)
            {
                numero = tab[1];
                num = Convert.ToInt32(numero) + 1;
                if (num / 10 == 0)
                { numero = "000" + num;  }
                else if (num / 100 == 0)
                { numero = "00" + num; }
                else if (num / 1000 == 0)
                { numero = "00" + num; }
                else if (num / 10000 == 0)
                { numero = "0" + num; }
                else
                { numero = "" + num; }
            }
            else
            {
                jour = date;
                numero = "0001";
            }

            return jour+""+numero;
        }

        internal List<TypeoperationBE> listerTypeOperation()
        {
            return typeoperationDA.listerTous();
        }

        internal List<string> listerValeursColonneTypeOperation(string p)
        {
            return typeoperationDA.listerValeursColonne(p);
        }

        internal List<RealiserBE> listerTousRealiser()
        {
            return realiserDA.listerTous();
        }

        internal bool enregistrerRealiser(RealiserBE realiserBE)
        {
            if (realiserDA.ajouter(realiserBE))
            {
                journalDA.journaliser("Enregistrement d'une opération de caisse - " + realiserBE.numeroop);
                return true;
            }
            else
                return false;
        }

        internal List<string> listerValeursColonneOperation(string p)
        {
            return operationDA.listerValeursColonne(p);
        }

        internal List<RealiserBE> listerSuivantCritereRealisers(string p)
        {
            return realiserDA.listerSuivantCritere(p);
        }

        internal void supprimerRealiser(RealiserBE realiser)
        {
            if(realiserDA.supprimer(realiser))
            {
                journalDA.journaliser("Suppression  d'une opération de caisse - " + realiser.numeroop);
            }
        }

        internal RealiserBE rechercherByNumeroRealiser(RealiserBE realiser)
        {
            return realiserDA.rechercherParNumero(realiser);
        }

        internal bool modifierRealiser(RealiserBE realiser)
        {
            if(realiserDA.modifier(realiser))
            {
                journalDA.journaliser("Modification d'une opération de caisse - " + realiser.numeroop);
                return true;
            }
            else
                return false;
        }

        internal OperationBE rechercherOperation(OperationBE operation)
        {
            return operationDA.rechercher(operation);
        }

        internal List<AcheterBE> listerSuivantCritereAcheters(string p)
        {
            return acheterDA.listerSuivantCritere(p);
        }

        internal List<PayerBE> listerSuivantCriterePayers(string p)
        {
            return payerDA.listerSuivantCritere(p);
        }

        internal List<PayerBE> listerSuivantCriterePayers_historique(string p)
        {
            return payerDA.listerSuivantCritere_historique(p);
        }

        internal List<LigneVersement> listerSuivantCriterePayers_versement(string p)
        {
            return payerDA.listerSuivantCriterePayers_versement(p);
        }

        internal string obtenirNomSetArticle(string p)
        {
            SetarticleBE set = new SetarticleBE();
            set.codesetarticle = p;
            set = setArticleDA.rechercher(set);
            return set.nomsetarticle;
        }

        internal string obtenirNomEleve(string p)
        {
            EleveBE eleve = new EleveBE();
            eleve.matricule = p;
            eleve = eleveDA.rechercher(eleve);
            return eleve.nom;
        }

        internal string obtenirNomTranche(string p)
        {
            TrancheBE tranche = new TrancheBE();
            tranche.codetranche = p;
            tranche = trancheDA.rechercher(tranche);
            return tranche.nomtranche;
        }

        internal string obtenirNomPrestation(string p)
        {
            PrestationBE prestation = new PrestationBE();
            prestation.codePrestation = p;
            prestation = prestationDA.rechercher(prestation);
            return prestation.nomPrestation;
        }

        internal List<string> listerValeursColonneOperationParType(string stype)
        {
            List<OperationBE> operations = new List<OperationBE>();
            List<string> resultats = new List<string>();

            operations = operationDA.listerSuivantCritere("codetypeop LIKE " +"'"+ stype+"'");
            foreach (OperationBE op in operations)
                resultats.Add(op.codeOp);

            return resultats;
        }

        internal List<AcheterBE> listerTousAcheter()
        {
            return acheterDA.listerTous();
        }

        internal List<PayerBE> listerTousPayer()
        {
            return payerDA.listerTous();
        }

        internal string obtenirClasse(string matricule)
        {
            int annee = inscrireDA.maxAnnee(matricule);
            InscrireBE i = new InscrireBE();
            i.matricule = matricule;
            i.annee = annee;
            try
            {
                i = inscrireDA.rechercherClasse(i);
                return i.codeClasse;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
