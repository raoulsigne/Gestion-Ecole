using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    class CreerModifierClasseBL
    {
        private ClasseDA classeDA;
        private JournalDA journalDA;

        public CreerModifierClasseBL()
        {
            this.classeDA = new ClasseDA();
            this.journalDA = new JournalDA();
        }

        //creer une TypeClasse
        public bool creerClasse(String codeClasse, String codeCycle, String typeClasse, String codeSerie, String codeNiveau, String nom)
        {
            ClasseBE classeBE = new ClasseBE(codeClasse, codeCycle, typeClasse, codeSerie, codeNiveau, nom);
            if (classeDA.ajouter(classeBE))
            {
                journalDA.journaliser("enregistrement d'une classe de  code" + codeClasse + ", cycle " + codeCycle + ", de type " + typeClasse + " de niveau " + codeNiveau + " et de nom " + nom);
                return true;
            }
            return false;
        }

        // supprimer un Classe
        public bool supprinerClasse(ClasseBE Classe)
        {
            if (classeDA.supprimer(Classe))
            {
                journalDA.journaliser("suppression de la classe de  code" + Classe.codeClasse + ", cycle " + Classe.codeCycle + ", de type " + Classe.codeTypeClasse + " de niveau " + Classe.codeNiveau + " et de nom " + Classe.nomClasse);
                return true;
            }
            return false;
        }

        // modifier un Classe
        public bool modifierClasse(ClasseBE Classe, ClasseBE newClasse)
        {
            if (classeDA.modifier(Classe, newClasse))
            {
                journalDA.journaliser("modification de la classe de  code" + Classe.codeClasse + ". ancien code : " + Classe.codeClasse + " , ancien cycle : " + Classe.codeCycle + ", ancien type " + Classe.codeTypeClasse + ", ancien niveau " + Classe.codeNiveau + " ancien nom " + Classe.nomClasse + ". nouveau code : " + newClasse.codeClasse + " , nouveau cycle : " + newClasse.codeCycle + ", nouveau type " + newClasse.codeTypeClasse + ", nouveau niveau " + newClasse.codeNiveau + " nouveau nom " + newClasse.nomClasse);
                return true;
            }
            return false;
        }

        // modifier un Classe
        public bool modifierClasse(ClasseBE Classe)
        {
            if (classeDA.modifier(Classe))
            {
                journalDA.journaliser("modification de la classe de  code" + Classe.codeClasse + ". nouveau code : " + Classe.codeClasse + " , nouveau cycle : " + Classe.codeCycle + ", nouveau type " + Classe.codeTypeClasse + ", nouveau niveau " + Classe.codeNiveau + " nouveau nom " + Classe.nomClasse);
                return true;
            }
            return false;
        }

        // rechercher une Classe
        public ClasseBE rechercherClasse(ClasseBE classe)
        {
            return classeDA.rechercher(classe);
        }

        //lister toutes les Classe
        public List<ClasseBE> listerTousLesClasse()
        {
            return classeDA.listerTous();
        }

        // lister toutes les Classe respectant un certain critère
        public List<ClasseBE> listerClasseSuivantCritere(string critere)
        {
            return classeDA.listerSuivantCritere(critere);
        }

        // retourne la liste des codes de Cycle deja enregistré (pour le filtre)
        public List<string> getListCodeCycle(List<CycleBE> listCycle)
        {
            List<string> listeCodeCycle = new List<string>();

            listeCodeCycle = new List<string>();
            listeCodeCycle.Add("<Toutes les Cycles>");
            if (listCycle != null)
            {
                for (int i = 0; i < listCycle.Count; i++)
                {
                    listeCodeCycle.Add(listCycle.ElementAt(i).codeCycle);
                }
                //listeCodeCycle.Add("Tous");
                return listeCodeCycle;
            }
            else return null;
        }

        // retourne la liste des codes de Cycle deja enregistré (pour le filtre)
        public List<string> getListCodeCycle2(List<CycleBE> listCycle)
        {
            List<string> listeCodeCycle = new List<string>();

            listeCodeCycle = new List<string>();
            if (listCycle != null)
            {
                for (int i = 0; i < listCycle.Count; i++)
                {
                    listeCodeCycle.Add(listCycle.ElementAt(i).codeCycle);
                }
                //listeCodeCycle.Add("Tous");
                return listeCodeCycle;
            }
            else return null;
        }

        // retourne la liste des codes de Type de Classe deja enregistré (pour le filtre)
        public List<string> getListCodeTypeClasse(List<TypeclasseBE> listTypeClasse)
        {
            List<string> listeCodeTypeClasse = new List<string>();

            listeCodeTypeClasse = new List<string>();
            listeCodeTypeClasse.Add("<Toutes les Types>");
            if (listTypeClasse != null)
            {
                for (int i = 0; i < listTypeClasse.Count; i++)
                {
                    listeCodeTypeClasse.Add(listTypeClasse.ElementAt(i).codetypeclasse);
                }
                //listeCodeCycle.Add("Tous");
                return listeCodeTypeClasse;
            }
            else return null;
        }

        // retourne la liste des codes de Type de Classe deja enregistré (pour le filtre)
        public List<string> getListCodeTypeClasse2(List<TypeclasseBE> listTypeClasse)
        {
            List<string> listeCodeTypeClasse = new List<string>();

            listeCodeTypeClasse = new List<string>();
            if (listTypeClasse != null)
            {
                for (int i = 0; i < listTypeClasse.Count; i++)
                {
                    listeCodeTypeClasse.Add(listTypeClasse.ElementAt(i).codetypeclasse);
                }
                //listeCodeCycle.Add("Tous");
                return listeCodeTypeClasse;
            }
            else return null;
        }

        // retourne la liste des codes de Niveau deja enregistré (pour le filtre)
        public List<string> getListCodeNiveau(List<NiveauBE> listNiveau)
        {
            List<string> listeCodeNiveau = new List<string>();

            listeCodeNiveau = new List<string>();
            listeCodeNiveau.Add("<Tous les Niveaux>");
            if (listNiveau != null)
            {
                for (int i = 0; i < listNiveau.Count; i++)
                {
                    listeCodeNiveau.Add(listNiveau.ElementAt(i).codeNiveau);
                }
                //listeCodeCycle.Add("Tous");
                return listeCodeNiveau;
            }
            else return null;
        }

        // retourne la liste des codes de Niveau deja enregistré (pour le filtre)
        public List<string> getListCodeNiveau2(List<NiveauBE> listNiveau)
        {
            List<string> listeCodeNiveau = new List<string>();

            listeCodeNiveau = new List<string>();
            if (listNiveau != null)
            {
                for (int i = 0; i < listNiveau.Count; i++)
                {
                    listeCodeNiveau.Add(listNiveau.ElementAt(i).codeNiveau);
                }
                //listeCodeCycle.Add("Tous");
                return listeCodeNiveau;
            }
            else return null;
        }

        // retourne la liste des codes de Serie deja enregistré (pour le filtre)
        public List<string> getListCodeSerie(List<SerieBE> listSerie)
        {
            List<string> listeCodeSerie = new List<string>();

            listeCodeSerie = new List<string>();
            listeCodeSerie.Add("<Toutes les Series>");
            if (listSerie != null)
            {
                for (int i = 0; i < listSerie.Count; i++)
                {
                    listeCodeSerie.Add(listSerie.ElementAt(i).codeserie);
                }
                //listeCodeCycle.Add("Tous");
                return listeCodeSerie;
            }
            else return null;
        }

        // retourne la liste des codes de Serie deja enregistré (pour le filtre)
        public List<string> getListCodeSerie2(List<SerieBE> listSerie)
        {
            List<string> listeCodeSerie = new List<string>();

            listeCodeSerie = new List<string>();
            if (listSerie != null)
            {
                for (int i = 0; i < listSerie.Count; i++)
                {
                    listeCodeSerie.Add(listSerie.ElementAt(i).codeserie);
                }
                //listeCodeCycle.Add("Tous");
                return listeCodeSerie;
            }
            else return null;
        }

        // retourne la liste des codes de Classe deja enregistré (pour le filtre)
        public List<string> getListCodeClasse(List<ClasseBE> listClasse)
        {
            List<string> listeCodeClasse = new List<string>();

            listeCodeClasse = new List<string>();
            listeCodeClasse.Add("<Toutes les Classes>");
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

       
    }
}
