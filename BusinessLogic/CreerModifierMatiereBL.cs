using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierMatiereBL
    {
        private MatiereDA matiereDA;
        private ParametresDA parametreDA;
        private JournalDA journalDA;

        public CreerModifierMatiereBL()
        {
            this.matiereDA = new MatiereDA();
            this.parametreDA = new ParametresDA();
            this.journalDA = new JournalDA();
        }

        //creer une MatiereBE
        public bool creerMatiere(string codeMatiere, string nomMatiere, string nameMatiere, int annee)
        {
            MatiereBE matiereBE = new MatiereBE(codeMatiere, nomMatiere, nameMatiere, annee);
            if (matiereDA.ajouter(matiereBE))
            {
                journalDA.journaliser("enregistrement d'une matière de code " + codeMatiere + " et de nom " + nomMatiere);
                return true;
            }
            return false;
        }

        // supprimer une MatiereBE
        public bool supprinerMatiere(MatiereBE matiere)
        {
            if (matiereDA.supprimer(matiere))
            {
                journalDA.journaliser("suppression de la matière de code " + matiere.codeMat + " et de nom " + matiere.nomMat);
                return true;
            }
            return false;
        }

        // modifier une MatiereBE
        public bool modifierMatiere(MatiereBE matiere, MatiereBE newMatiere)
        {
            if (matiereDA.modifier(matiere, newMatiere))
            {
                journalDA.journaliser("modification de la matière de code " + matiere.codeMat + ". ancien nom : " + matiere.nomMat + ". nouveau code : " + newMatiere.codeMat + ", nouveau nom : " + newMatiere.nomMat);
                return true;
            }
            return false;
        }

        // modifier une MatiereBE
        public bool modifierMatiere(MatiereBE matiere)
        {
            if (matiereDA.modifier(matiere))
            {
                journalDA.journaliser("modification de la matière de code " + matiere.codeMat + ". nouveau nom : " + matiere.nomMat);
                return true;
            }
            return false;
        }

        // rechercher une MatiereBE
        public MatiereBE rechercherMatiere(MatiereBE matiere)
        {
            return matiereDA.rechercher(matiere);
        }

        //lister toutes les Matieres
        public List<MatiereBE> listerToutesLesMatieres()
        {
            return matiereDA.listerTous();
        }

        // lister toutes les Matieres respectant un certain critère
        public List<MatiereBE> listerMatieresSuivantCritere(string critere)
        {
            return matiereDA.listerSuivantCritere(critere);
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
    }
}
