using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;
using Ecole.ClasseConception;

namespace Ecole.DataAccess
{
    class JournalDA : DA<JournalBE>
    {
        private Connexion con = Connexion.getConnexion();

        //------------------------ Ajout d'objet dans le journal ---------------------------
        public override Boolean ajouter(JournalBE journal)
        {
            // créer une transaction
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL

                cmd.CommandText = "INSERT INTO journal (login,action,date,heure) VALUES (@login, @action, @date,@heure)";

                // utilisation de l'objet JournalBE passé en paramètre pour définir les valeur des paramètres
                cmd.Parameters.AddWithValue("@login", journal.login);
                cmd.Parameters.AddWithValue("@action", journal.action);
                cmd.Parameters.AddWithValue("@date", journal.date);
                cmd.Parameters.AddWithValue("@heure", journal.heure);

                //associer la transaction à notre commande sql
                cmd.Transaction = transaction;

                // Exécution de la commande SQL

                cmd.ExecuteNonQuery();

               //valider la transaction
                transaction.Commit();

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //annuler la transaction
                transaction.Rollback();

                return false;
            }
        }
        //---------------------------- fin ajout----------------------------------------

        //------------suppression d'objet dans le journal booléen-----------------------
        public override Boolean supprimer(JournalBE journal)
        {
            // créer une transaction
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM journal WHERE login=@login and action=@action, date=@date,heure=@heure";

                // utilisation de l'objet journalBE passé en paramètre
                cmd.Parameters.AddWithValue("@login", journal.login);
                cmd.Parameters.AddWithValue("@action", journal.action);
                cmd.Parameters.AddWithValue("@date", journal.date);
                cmd.Parameters.AddWithValue("@heure", journal.heure);
                cmd.Transaction = transaction;

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();

                transaction.Commit();

                return true;
                // Fermeture de la connexion
                //  this.con.fermer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                transaction.Rollback();

                return false;
            }
        }
        //-----------------fin suppression-------------------------------------

        //----------------------- vider le journal-----------------------------
        public Boolean vider()
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try 
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM journal";

                cmd.Transaction = transaction;

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();

                transaction.Commit();

                return true;
                // Fermeture de la connexion
                //  this.con.fermer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                transaction.Rollback();

                return false;
            }
        }
        //---------------------fin vider------------------------------------

        //-- retourner la liste des objets qui correspondent à un certain critère--------------
        public List<LigneEtatJournal> listerSuivantCriteres(String critere)
        {
            int num = 0;
            try   
            {
                List<LigneEtatJournal> listJournal = new List<LigneEtatJournal>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT j.*, u.nom FROM journal j, utilisateur u " 
                                  +"WHERE j.login = u.login "+ critere;

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //construire la liste à retourner
                    while (dataReader.Read())
                    {
                        num = num + 1;
                        LigneEtatJournal Lignejournal = new LigneEtatJournal(Convert.ToString(num), Convert.ToString(dataReader["login"]), Convert.ToString(dataReader["nom"]), Convert.ToString(dataReader["action"]), ((DateTime)dataReader["date"]).ToString("dd-MM-yyyy"), Convert.ToString(dataReader["heure"]));
                        listJournal.Add(Lignejournal);
                    }

                    //fermer le Data Reader
                    dataReader.Close();

                   
                    //retourner la list si pas vide
                    if (listJournal.Count != 0)
                        return listJournal;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //-------------------fin rechercher suivant critère-------------------------------


        public override List<String> listerValeursColonne(String colonne) {return null;}
       // public override List<JournalBE> listerSuivantCritere(string critere);

        public override JournalBE rechercher(JournalBE entity) { return null; }


        public override Boolean modifier(JournalBE entity, JournalBE newEntity) { return true; }


        public override List<JournalBE> listerTous() { return null; }
        public override List<JournalBE> listerSuivantCritere(String critere) { return null; }


        /**
         * Fonction qui prend en parametre une action pour journaliser
         * @param action est l'action que nous voulons journaliser
         */
        public bool journaliser(string action)
        {
            
            JournalBE journal = new JournalBE(Ecole.UI.ConnexionUI.utilisateur.login, action, DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Now.ToString("hh:mm:ss"));
            if (this.ajouter(journal))
                return true;
            else
                return false;
        }
    }
}
