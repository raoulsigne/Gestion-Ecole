using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class DepartementDA : DA<DepartementBE>
    {
        private Connexion con = Connexion.getConnexion();

        //---------------ajouter-----------------------------------------
        public override Boolean ajouter(DepartementBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO Departement (codedept,nomdept) VALUES (@code, @nom)";
                cmd.Parameters.AddWithValue("@code", entity.codeDept);
                cmd.Parameters.AddWithValue("@nom", entity.nomDept);

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
        //----------------fin ajouter --------------------------------//

        //----------------debut supprimer ---------------------------//
        public override Boolean supprimer(DepartementBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Departement WHERE codedept = @code";
                cmd.Parameters.AddWithValue("@code", entity.codeDept);

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
        public override DepartementBE rechercher(DepartementBE entity)
        {
            String nomdept;
            String codedept;
            DepartementBE d;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM Departement WHERE codedept=@code";
                cmd.Parameters.AddWithValue("@code", entity.codeDept);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        codedept = Convert.ToString(dataReader["codedept"]);
                        nomdept = Convert.ToString(dataReader["nomdept"]);
                        d = new DepartementBE(codedept, nomdept);
                        dataReader.Close();
                        return d;
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

        //----------------debut modifier ----------------------------------//
        public override Boolean modifier(DepartementBE entity, DepartementBE newEntity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE departement SET codedept=@codedept, nomdept=@nomdept WHERE codedept=@code"; 

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@code", entity.codeDept);
                cmd.Parameters.AddWithValue("@codedept", newEntity.codeDept);
                cmd.Parameters.AddWithValue("@nomdept", newEntity.nomDept);

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

        //----------------debut lister --------------------------------------------
        public override List<DepartementBE> listerTous()
        {
            List<DepartementBE> list = new List<DepartementBE>();
            String nomdept;
            String codedept;
            DepartementBE d;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Departement";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codedept = Convert.ToString(dataReader["codedept"]);
                        nomdept = Convert.ToString(dataReader["nomdept"]);
                        d = new DepartementBE(codedept, nomdept);
                        list.Add(d);
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

        public override List<DepartementBE> listerSuivantCritere(string critere)
        {
            List<DepartementBE> list = new List<DepartementBE>();
            String nomdept;
            String codedept;
            DepartementBE d;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Departement WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codedept = Convert.ToString(dataReader["codedept"]);
                        nomdept = Convert.ToString(dataReader["nomdept"]);
                        d = new DepartementBE(codedept, nomdept);
                        list.Add(d);
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
                cmd.CommandText = "SELECT * FROM Departement ";

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
                cmd.CommandText = "SELECT COUNT(*) FROM Departement";

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
        //-----------fin compter ----------------------------------------------
    }
}
