using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using Ecole.DataAccess;
using Ecole.ClasseConception ;


namespace Ecole.BusinessLogic
{
    class GestionJournalBL
    {
         private JournalDA journalDA;
         private UtilisateurDA utilisateurDA;
         //------constructeur----------------------------------------------------
         public GestionJournalBL()
        {
            this.journalDA = new JournalDA();
            this.utilisateurDA = new UtilisateurDA();
        }

        //------ajouter une ligne dans le journal--------------------------------
         public bool ajouterJournal(JournalBE journalBE)
        {
            return journalDA.ajouter(journalBE);
        }

         //------supprimer une ligne du journal----------------------------------
         public bool supprimerJournal(JournalBE journalBE)
         {
             return journalDA.supprimer(journalBE); 
         }

        //-----rechercher des lignes dans le journal suivant un critère----------
         public List<LigneEtatJournal> listerSuivantCritere(string critere)
         {
             return journalDA.listerSuivantCriteres(critere);
         }

         //-----lister valeur colone--------------------------------------------
         public List<String> listerValeurColonne(string colonne)
         {
             return utilisateurDA.listerValeursColonne(colonne); 
         }

    }
}
