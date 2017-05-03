using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.DataAccess;
using Ecole.BusinessEntity;

namespace Ecole.BusinessLogic
{
    class DefinirMentionBL
    {
        private MentionDA mentionDA;
        private JournalDA journalDA;

        public DefinirMentionBL()
        {
            this.mentionDA = new MentionDA();
            this.journalDA = new JournalDA();
        }

        //creer une Mention
        public bool creerMention(int id, double noteMin, double noteMax, String mention)
        {
            MentionBE m = new MentionBE(id, noteMin, noteMax, mention);
            if (mentionDA.ajouter(m))
            {
                journalDA.journaliser("enregistrement d'une mention de noteMin : " + noteMin + ", noteMax : " + noteMax + ", mention : " + mention);
                return true;
            }
            return false;
        }

        // supprimer une Mention
        public bool supprinerMention(MentionBE mention)
        {
            if (mentionDA.supprimer(mention))
            {
                journalDA.journaliser("suppression de la mention de noteMin : " + mention.noteMin + ", noteMax : " + mention.noteMax + ", mention : " + mention.mention );
                return true;
            }
            return false;
        }

        // modifier une Mention
        public bool modifierMention(MentionBE mention, MentionBE newMention)
        {
            if (mentionDA.modifier(mention, newMention))
            {
                journalDA.journaliser("suppression de la mention de noteMin : " + mention.noteMin + ", noteMax : " + mention.noteMax + ", mention : " + mention.mention + ". nouvelle noteMin : " + newMention.noteMin + ", nouvelle noteMax : " + newMention.noteMax + ", nouvelle mention : " + newMention.mention);
                return true;
            }
            return false;
        }

        // rechercher une Mention
        public MentionBE rechercherMention(MentionBE mention)
        {
            return mentionDA.rechercher(mention);
        }

        //lister toutes les Mentions
        public List<MentionBE> listerToutesLesMentions()
        {
            return mentionDA.listerTous();
        }

        // lister toutes les Mentions respectant un certain critère
        public List<MentionBE> listerMentionsSuivantCritere(string critere)
        {
            return mentionDA.listerSuivantCritere(critere);
        }
    }
}
