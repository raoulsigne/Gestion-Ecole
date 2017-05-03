using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    public class UtilisateurDA : DA<UtilisateurBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'un nouvel utilisateur ------------------------------
        public override Boolean ajouter(UtilisateurBE U)
        {
            MySqlTransaction transaction = null;

            try
            {
                transaction = con.connexion.BeginTransaction();

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO utilisateur (LOGIN, ROLE, PASSWORD, NOM) VALUES (@login, @role, PASSWORD(@pwd), @nom)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@login", U.login);
                cmd.Parameters.AddWithValue("@role", U.role);
                cmd.Parameters.AddWithValue("@pwd", U.password);
                cmd.Parameters.AddWithValue("@nom", U.nom);

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

                if(transaction != null)
                    transaction.Rollback();

                return false;
            }
        }

        //--------------------------Fin ajout-----------------------------

        //--------------------------Suppression d'un utilisateur ------

        public override Boolean supprimer(UtilisateurBE U)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM utilisateur WHERE LOGIN=@login";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@login", U.login);

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

        //--------------------------Fin Suppression-----------------------------

        //--------------------------Modification d'un utilisateur -----------

        // mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(UtilisateurBE utilisateur, UtilisateurBE newUtilisateur)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE utilisateur SET ROLE=@role, PASSWORD=PASSWORD(@pwd), NOM=@nom, LOGIN=@login1 WHERE LOGIN=@login";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@login", utilisateur.login);

                cmd.Parameters.AddWithValue("@login1", newUtilisateur.login);
                cmd.Parameters.AddWithValue("@role", newUtilisateur.role);
                cmd.Parameters.AddWithValue("@pwd", newUtilisateur.password);
                cmd.Parameters.AddWithValue("@nom", newUtilisateur.nom);

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
        //---------------------------Fin Modification --------------------------

        //---------------Rechercher des informations sur un utilisateur spécifique---------------------------------

        public override UtilisateurBE rechercher(UtilisateurBE user)
        {
            string ROLE;
            string pwd;
            string nom;

            UtilisateurBE U;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM utilisateur WHERE LOGIN=@login";
                cmd.Parameters.AddWithValue("@login", user.login);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        ROLE = Convert.ToString(dataReader["ROLE"]);
                        pwd = Convert.ToString(dataReader["PASSWORD"]);
                        nom = Convert.ToString(dataReader["NOM"]);
                        U = new UtilisateurBE(user.login, ROLE, pwd, nom);
                        dataReader.Close();
                        // this.con.fermer();
                        return U;
                    }


                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public UtilisateurBE rechercherAvecPassword(UtilisateurBE utilisateur)
        {
            UtilisateurBE U;

            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM utilisateur WHERE LOGIN=@login and PASSWORD=PASSWORD('"+utilisateur.password+"')";
                cmd.Parameters.AddWithValue("@login", utilisateur.login);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        U = new UtilisateurBE(utilisateur.login, Convert.ToString(dataReader["ROLE"]), Convert.ToString(dataReader["PASSWORD"]), Convert.ToString(dataReader["NOM"]));
                        return U;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

            //-----------------------------Fin de la recherche----------
            //----------------debut lister --------------------------------------------
        public override List<UtilisateurBE> listerTous()
        {
            List<UtilisateurBE> list = new List<UtilisateurBE>();
            String nom;
            String role;
            String pwd;
            String login;
            UtilisateurBE u;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Utilisateur";
                
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        role = Convert.ToString(dataReader["ROLE"]);
                        pwd = Convert.ToString(dataReader["PASSWORD"]);
                        nom = Convert.ToString(dataReader["NOM"]);
                        login = Convert.ToString(dataReader["LOGIN"]);
                        u = new UtilisateurBE(login, role, pwd, nom);
                        list.Add(u);
                    }
                    dataReader.Close();
                        
                    return list;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //----------------fin lister --------------------------------------------


        public override List<UtilisateurBE> listerSuivantCritere(string critere)
        {
            List<UtilisateurBE> list = new List<UtilisateurBE>();
            String nom;
            String role;
            String pwd;
            String login;
            UtilisateurBE u;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Utilisateur WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        role = Convert.ToString(dataReader["ROLE"]);
                        pwd = Convert.ToString(dataReader["PASSWORD"]);
                        nom = Convert.ToString(dataReader["NOM"]);
                        login = Convert.ToString(dataReader["LOGIN"]);
                        u = new UtilisateurBE(login, role, pwd, nom);
                        list.Add(u);
                    }
                    dataReader.Close();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override List<string> listerValeursColonne(string colonne)
        {
            List<String> list = new List<String>();
            
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Utilisateur ORDER BY login";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        list.Add(Convert.ToString(dataReader[colonne]));
                    }
                    dataReader.Close();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //-----------debut compter -----------------
        public int compter()
        {
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Utilisateur";

                // Exécution de la commande SQL
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
        //-----------fin compter ------------
    }
}
