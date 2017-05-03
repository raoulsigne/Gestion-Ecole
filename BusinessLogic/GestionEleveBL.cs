using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;
using Ecole.ClasseConception;

namespace Ecole.BusinessLogic
{
    class GestionEleveBL
    {
        ClasseDA classeDA;
        CategorieEleveDA categorieDA;
        PaysDA paysDA;
        DepartementDA departementDA;
        RegionDA regionDA;
        EleveDA eleveDA;
        InscrireDA inscrireDA;
        AppartenirDA appartenirDA;
        ParametresDA parametreDA;
        JournalDA journalDA;

        public GestionEleveBL()
        {
            classeDA = new ClasseDA();
            categorieDA = new CategorieEleveDA();
            paysDA = new PaysDA();
            departementDA = new DepartementDA();
            regionDA = new RegionDA();
            eleveDA = new EleveDA();
            inscrireDA = new InscrireDA();
            appartenirDA = new AppartenirDA();
            parametreDA = new ParametresDA();
            journalDA = new JournalDA();
        }

        internal List<string> listerValeurColonneClasse(string p)
        {
            return classeDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneCategorie(string p)
        {
            return categorieDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonnePays(string p)
        {
            return paysDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneDepartement(string p)
        {
            return departementDA.listerValeursColonne(p);
        }

        internal List<string> listerValeurColonneRegion(string p)
        {
            return regionDA.listerValeursColonne(p);
        }

        internal BusinessEntity.EleveBE rechercherEleve(BusinessEntity.EleveBE eleve)
        {
            return eleveDA.rechercher(eleve);
        }

        internal BusinessEntity.InscrireBE rechercherInscrire(BusinessEntity.InscrireBE inscrire)
        {
            return inscrireDA.rechercherClasse(inscrire);
        }

        internal BusinessEntity.AppartenirBE rechercherAppartenir(BusinessEntity.AppartenirBE appartenir)
        {
            return appartenirDA.rechercherCategorie(appartenir);
        }

        internal void supprimerEleve(BusinessEntity.EleveBE eleve)
        {
            if (eleveDA.supprimer(eleve))
                journalDA.journaliser("Suppression d'un élève " + eleve.matricule);
        }

        internal bool enregistrerEleve(EleveBE eleve)
        {
            if (eleveDA.ajouter(eleve))
            {
                journalDA.journaliser("Enregistrement d'un élève " + eleve.matricule);
                return true;
            }
            else
                return false;
        }

        internal bool enregistrerInscrire(InscrireBE inscrire)
        {
            if(inscrireDA.ajouter(inscrire))
            {
                journalDA.journaliser("Enregistrement de l'inscription d'un élève " + inscrire.matricule + " - "+inscrire.codeClasse);
                return true;
            }
            else
                return false; 
        }

        internal bool enregistrerAppartenir(AppartenirBE appartenir)
        {
            if(appartenirDA.ajouter(appartenir))
            {
                journalDA.journaliser("Enregistrement de la categorie d'un élève " + appartenir.matricule+" - "+appartenir.codeCatEleve);
                return true;
            }
            else
                return false;
        }

        internal int AnneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        internal bool modifierEleve(EleveBE e, EleveBE eleve)
        {
            if(eleveDA.modifier(e,eleve))
            {
                journalDA.journaliser("Modification d'un élève "+eleve.matricule);
                return true;
            }
            else
                return false;
        }

        internal int obtenirAnneeInscription(EleveBE eleve)
        {
            return inscrireDA.maxAnnee(eleve.matricule);
        }

        internal string getDernierMatricule()
        {
            return eleveDA.getDernierMatricule();
        }

        //-----------------------------MOI----------------------------------------
        //------------------------------------------------------------------------
        public List<LigneInsolvable> getListeInsolvable(string classe, int annee, string date)
        {
            return eleveDA.getListeInsolvable(classe, annee, date);
        }

        internal List<EleveBE> listerElevesDuneClasse(string codeclasse, int annee)
        {
            ClasseBE c = new ClasseBE();
            c.codeClasse = codeclasse;
            c = classeDA.rechercher(c);
            return classeDA.listeEleves(c, annee);
        }

        internal void supprimerInscrire(InscrireBE inscrire)
        {
            inscrireDA.supprimer(inscrire);
        }

        internal bool estRedoublant(EleveBE eleve,ClasseBE classe, int annee)
        {
            return eleveDA.estRedoublant(eleve, classe, annee);
        }

        internal ClasseBE rechercherClasse(ClasseBE classe)
        {
            return classeDA.rechercher(classe);
        }

        internal void supprimerAppartenir(AppartenirBE appartenir)
        {
            appartenirDA.supprimer(appartenir);
        }
    }
}
