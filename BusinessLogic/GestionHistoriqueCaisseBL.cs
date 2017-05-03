using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class GestionHistoriqueCaisseBL
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

        public GestionHistoriqueCaisseBL()
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
        }

        internal List<RealiserBE> listerSuivantCritereRealisers(string p)
        {
            return realiserDA.listerSuivantCritere(p);
        }

        internal List<AcheterBE> listerSuivantCritereAcheters(string p)
        {
            return acheterDA.listerSuivantCritere(p);
        }

        internal List<PayerBE> listerSuivantCriterePayers(string p)
        {
            return payerDA.listerSuivantCritere(p);
        }

        internal List<RealiserBE> listerTousRealiser()
        {
            return realiserDA.listerTous();
        }

        internal List<AcheterBE> listerTousAcheter()
        {
            return acheterDA.listerTous();
        }

        internal List<PayerBE> listerTousPayer()
        {
            return payerDA.listerTous();
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
    }
}
