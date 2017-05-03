using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class GroupeDA : DA<GroupeBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------ajouter CategorieEleve -----------------//
        public override Boolean ajouter(GroupeBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO Groupe (role,description) VALUES (@role, @desc)";
                cmd.Parameters.AddWithValue("@role", entity.role);
                cmd.Parameters.AddWithValue("@desc", entity.description);

                // Exécution de la commande SQL
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();
                tx.Commit();

                return true;
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //----------------fin ajouter -------------------------------//

        //----------------debut supprimer -----------------//
        public override Boolean supprimer(GroupeBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Groupe WHERE role = @role";
                cmd.Parameters.AddWithValue("@role", entity.role);

                // Exécution de la commande SQL
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();
                tx.Commit();

                return true;
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //----------------fin supprimer ---------------------//

        //----------------chercher Acheter -----------------//
        public override GroupeBE rechercher(GroupeBE entity)
        {
            String description;
            String role;
            GroupeBE g;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM Groupe WHERE role=@role";
                cmd.Parameters.AddWithValue("@role", entity.role);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        role = Convert.ToString(dataReader["role"]);
                        description = Convert.ToString(dataReader["description"]);
                        g = new GroupeBE(role, description);
                        dataReader.Close();
                        return g;
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
        //----------------Fin chercher ------------------------------------//

        //----------------debut modifier ---------------//
        public override Boolean modifier(GroupeBE entity, GroupeBE newEntity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE groupe SET description=@description,role=@role WHERE role=@rol";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@role", newEntity.role);
                cmd.Parameters.AddWithValue("@description", newEntity.description);
                cmd.Parameters.AddWithValue("@rol", entity.role);

                // Exécution de la commande SQL
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();
                tx.Commit();

                return true;
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //----------------fin modifier ------------------------------------//

        //-----------------------------------------------------------------------------------------
        //---------Moi---debut lister tous les groupes d'utilisateurs----------------------------
        public override List<GroupeBE> listerTous()
        {
            List<GroupeBE> list = new List<GroupeBE>();
            String role;
            String description;
            GroupeBE g;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM groupe order by role";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer les objets groupe et les mettre dans la liste à retourner
                    while (dataReader.Read())
                    {
                        role = Convert.ToString(dataReader["role"]);
                        description = Convert.ToString(dataReader["description"]);
                        g = new GroupeBE(role, description);
                        list.Add(g);
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


        //----------------Chercher la description d'un role-----------------------
        public String getDescriptionRole(String role)
        {
            String description;
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT description FROM Groupe WHERE role=@role";
                cmd.Parameters.AddWithValue("@role", role);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        description = Convert.ToString(dataReader["description"]);
                        return description;
                    }

                    dataReader.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //----------------Fin getDescription-----------------------------------




        public override List<GroupeBE> listerSuivantCritere(string critere)
        {
            List<GroupeBE> list = new List<GroupeBE>();
            String role;
            String description;
            GroupeBE g;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM groupe WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        description = Convert.ToString(dataReader["description"]);
                        role = Convert.ToString(dataReader["role"]);
                        g = new GroupeBE(description, role);
                        list.Add(g);
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
                cmd.CommandText = "SELECT * FROM groupe ";

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

        //-----------debut compter ----------------------------------------------
        public int compter()
        {
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Groupe";

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
        //-----------------------------fin compter --------------------------------------
    }
}
