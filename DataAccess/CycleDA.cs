using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class CycleDA : DA<CycleBE>
    {
        private Connexion con = Connexion.getConnexion();

        //---------------ajouter-----------------------------------------
        public override Boolean ajouter(CycleBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO Cycle (codecycle,nomcycle) VALUES (@code, @nom)";
                cmd.Parameters.AddWithValue("@code", entity.codeCycle);
                cmd.Parameters.AddWithValue("@nom", entity.nomCycle);

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
        public override Boolean supprimer(CycleBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Cycle WHERE codecycle = @code";
                cmd.Parameters.AddWithValue("@code", entity.codeCycle);

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
        public override CycleBE rechercher(CycleBE entity)
        {
            String nomcycle;
            String codecycle;
            CycleBE c;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM Cycle WHERE codecycle=@code";
                cmd.Parameters.AddWithValue("@code", entity.codeCycle);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        codecycle = Convert.ToString(dataReader["codecycle"]);
                        nomcycle = Convert.ToString(dataReader["nomcycle"]);
                        c = new CycleBE(codecycle, nomcycle);
                        dataReader.Close();
                        return c;
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
        public override Boolean modifier(CycleBE entity, CycleBE newEntity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE cycle SET nomcycle=@nomcycle,codecycle=@codecycle WHERE codecycle=@code";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codecycle", newEntity.codeCycle);
                cmd.Parameters.AddWithValue("@nomcycle", newEntity.nomCycle);
                
                cmd.Parameters.AddWithValue("@code", entity.codeCycle);
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
        public override List<CycleBE> listerTous()
        {
            List<CycleBE> list = new List<CycleBE>();
            String nomcycle;
            String codecycle;
            CycleBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Cycle";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codecycle = Convert.ToString(dataReader["codecycle"]);
                        nomcycle = Convert.ToString(dataReader["nomcycle"]);
                        c = new CycleBE(codecycle, nomcycle);
                        list.Add(c);
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

        public override List<CycleBE> listerSuivantCritere(string critere)
        {
            List<CycleBE> list = new List<CycleBE>();
            String nomcycle;
            String codecycle;
            CycleBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Cycle WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codecycle = Convert.ToString(dataReader["codecycle"]);
                        nomcycle = Convert.ToString(dataReader["nomcycle"]);
                        c = new CycleBE(codecycle, nomcycle);
                        list.Add(c);
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
                cmd.CommandText = "SELECT * FROM Cycle ";

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
                cmd.CommandText = "SELECT COUNT(*) FROM Cycle";

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

        public Boolean modifier(CycleBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE cycle SET NOMCYCLE=@nomC WHERE CODECYCLE=@codeC";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeC", entity.codeCycle);
                cmd.Parameters.AddWithValue("@nomC", entity.nomCycle);

                // Exécution de la commande SQL
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();
                tx.Commit();

                return true;
                // Fermeture de la connexion
                //  this.con.fermer();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //----------------fin modifier ------------------------------------//

    }
}
