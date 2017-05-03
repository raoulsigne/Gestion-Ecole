using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class CreerModifierTypeEvaluationBL
    {
        private TypeevaluationDA typeevaluationDA;
        private JournalDA journalDA;

        public CreerModifierTypeEvaluationBL()
        {
            this.typeevaluationDA = new TypeevaluationDA();
            this.journalDA = new JournalDA();
        }

        //creer un TypeEvaluation
        public bool creerTypeEvaluation(string codeTypeEvaluation, string nomTypeEvaluation)
        {
            TypeevaluationBE typeevaluationBE = new TypeevaluationBE(codeTypeEvaluation, nomTypeEvaluation);
            if (typeevaluationDA.ajouter(typeevaluationBE))
            {
                journalDA.journaliser("enregistrement d'un type d'évaluation de code " + codeTypeEvaluation + " et de nom " + nomTypeEvaluation);
                return true;
            }
            return false;
        }

        // supprimer un TypeEvaluation
        public bool supprinerTypeEvaluation(TypeevaluationBE typeEvaluation)
        {
            if (typeevaluationDA.supprimer(typeEvaluation))
            {
                journalDA.journaliser("suppression du type d'évaluation de code " + typeEvaluation.codeevaluation + " et de nom " + typeEvaluation.nomeval);
                return true;
            }
            return false;
        }

        // modifier un TypeEvaluation
        public bool modifierTypeEvaluation(TypeevaluationBE typeEvaluation, TypeevaluationBE newTypeEvaluation)
        {
            if (typeevaluationDA.modifier(typeEvaluation, newTypeEvaluation))
            {
                journalDA.journaliser("modification du type d'évaluation de code " + typeEvaluation.codeevaluation + ". ancien nom : " + typeEvaluation.nomeval + ". nouveau nom : " + newTypeEvaluation.nomeval);
                return true;
            }
            return false;
        }

        // modifier un TypeEvaluation
        public bool modifierTypeEvaluation(TypeevaluationBE typeEvaluation)
        {
            if (typeevaluationDA.modifier(typeEvaluation))
            {
                journalDA.journaliser("modification du type d'évaluation de code " + typeEvaluation.codeevaluation + ". nouveau nom : " + typeEvaluation.nomeval);
                return true;
            }
            return false;
        }

        // rechercher un TypeEvaluation
        public TypeevaluationBE rechercherTypeEvaluation(TypeevaluationBE typeEvaluation)
        {
            return typeevaluationDA.rechercher(typeEvaluation);
        }

        //lister tous les TypeEvaluation
        public List<TypeevaluationBE> listerTousLesTypeEvaluations()
        {
            return typeevaluationDA.listerTous();
        }

        // lister tous les TypeEvaluation respectant un certain critère
        public List<TypeevaluationBE> listerTypeEvaluationsSuivantCritere(string critere)
        {
            return typeevaluationDA.listerSuivantCritere(critere);
        }
    }
}
