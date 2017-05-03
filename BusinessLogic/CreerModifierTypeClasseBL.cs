using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierTypeClasseBL
    {
        private TypeclasseDA typeclasseDA;
        private JournalDA journalDA;

        public CreerModifierTypeClasseBL()
        {
            this.typeclasseDA = new TypeclasseDA();
            this.journalDA = new JournalDA();
        }

        //creer une TypeClasse
        public bool creerTypeClasse(string codeTypeClasse, string nomTypeClasse, int fraisInscription)
        {
            TypeclasseBE typeclasseBE = new TypeclasseBE(codeTypeClasse, nomTypeClasse, fraisInscription);
            if (typeclasseDA.ajouter(typeclasseBE))
            {
                journalDA.journaliser("enregistrement d'un type de classe de code " + codeTypeClasse + ", nom " + nomTypeClasse + ", frais d'inscription : " + fraisInscription);
                return true;
            }
            return false;
        }

        // supprimer un TypeClasse
        public bool supprinerTypeClasse(TypeclasseBE typeClasse)
        {
            if (typeclasseDA.supprimer(typeClasse))
            {
                journalDA.journaliser("suppression du type de classe de code " + typeClasse.codetypeclasse + ", nom " + typeClasse.nomtypeclasse);
                return true;
            }
            return false;
        }

        // modifier un TypeClasse
        public bool modifierTypeClasse(TypeclasseBE typeClasse, TypeclasseBE newtypeClasse)
        {
            if (typeclasseDA.modifier(typeClasse, newtypeClasse))
            {
                journalDA.journaliser("modification du type de classe de code " + typeClasse.codetypeclasse + ". ancien nom : " + typeClasse.nomtypeclasse + ", ancien Frais : " + typeClasse.fraisinscription + ". nouveau code : " + newtypeClasse.codetypeclasse + ", nouveau nom : " + newtypeClasse.nomtypeclasse + ", nouveau Frais : " + newtypeClasse.fraisinscription);
                return true;
            }
            return false;
        }

        // modifier un TypeClasse
        public bool modifierTypeClasse(TypeclasseBE typeClasse)
        {
            if (typeclasseDA.modifier(typeClasse))
            {
                journalDA.journaliser("modification du type de classe de code " + typeClasse.codetypeclasse + ". nouveau nom : " + typeClasse.nomtypeclasse + ", nouveau Frais : " + typeClasse.fraisinscription);
                return true;
            }
            return false;
        }

        // rechercher une TypeClasse
        public TypeclasseBE rechercherTypeClasse(TypeclasseBE typeClasse)
        {
            return typeclasseDA.rechercher(typeClasse);
        }

        //lister toutes les TypeClasse
        public List<TypeclasseBE> listerTousLesTypeClasse()
        {
            return typeclasseDA.listerTous();
        }

        // lister toutes les TypeClasse respectant un certain critère
        public List<TypeclasseBE> listerTypeClasseSuivantCritere(string critere)
        {
            return typeclasseDA.listerSuivantCritere(critere);
        }

        // retourne la liste des codes de Type de Classe deja enregistré (pour le filtre)
        public List<string> getListCodeTypeClasse(List<TypeclasseBE> listTypeClasse)
        {
            List<string> listeCodeTypeClasse = new List<string>();

            listeCodeTypeClasse = new List<string>();
            listeCodeTypeClasse.Add("<Tous les Codes>");
            if (listTypeClasse != null)
            {
                for (int i = 0; i < listTypeClasse.Count; i++)
                {
                    listeCodeTypeClasse.Add(listTypeClasse.ElementAt(i).codetypeclasse);
                }
                //listeCodeTypeClasse.Add("Tous");
                return listeCodeTypeClasse;
            }
            else return null;
        }

        // retourne la liste des Noms de Type de Classe deja enregistré (pour le filtre)
        public List<string> getListNomTypeClasse(List<TypeclasseBE> listTypeClasse)
        {
            List<string> listeNomTypeClasse = new List<string>();

            listeNomTypeClasse = new List<string>();
            listeNomTypeClasse.Add("<Tous les Noms>");
            if (listTypeClasse != null)
            {
                for (int i = 0; i < listTypeClasse.Count; i++)
                {
                    listeNomTypeClasse.Add(listTypeClasse.ElementAt(i).nomtypeclasse);
                }
                //listeNomSerie.Add("Tous");
                return listeNomTypeClasse;
            }
            else return null;
        }
    }
}
