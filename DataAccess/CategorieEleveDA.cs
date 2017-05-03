using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class CategorieEleveDA : DA<CategorieEleveBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------ajouter CategorieEleve -----------------//
        public override Boolean ajouter(CategorieEleveBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO CategorieEleve (codecateleve,nomcateleve) VALUES (@code, @nom)";
                cmd.Parameters.AddWithValue("@code", entity.codeCatEleve);
                cmd.Parameters.AddWithValue("@nom", entity.nomCatEleve);

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
        public override Boolean supprimer(CategorieEleveBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM CategorieEleve WHERE codecateleve = @code";
                cmd.Parameters.AddWithValue("@code", entity.codeCatEleve);

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
        public override CategorieEleveBE rechercher(CategorieEleveBE entity)
        {
            String nomcateleve;
            String codecateleve;
            CategorieEleveBE c;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM CategorieEleve WHERE codecateleve=@code";
                cmd.Parameters.AddWithValue("@code", entity.codeCatEleve);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        codecateleve = Convert.ToString(dataReader["codecateleve"]);
                        nomcateleve = Convert.ToString(dataReader["nomcateleve"]);
                        c = new CategorieEleveBE(codecateleve, nomcateleve);
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

        //----------------debut modifier ---------------//
        public override Boolean modifier(CategorieEleveBE entity, CategorieEleveBE newEntity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE categorieeleve SET nomcateleve=@nomcateleve, codecateleve=@code WHERE codecateleve=@codecateleve";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@nomcateleve", newEntity.nomCatEleve);
                cmd.Parameters.AddWithValue("@code", newEntity.codeCatEleve);

                cmd.Parameters.AddWithValue("@codecateleve", entity.codeCatEleve);
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
        public override List<CategorieEleveBE> listerTous()
        {
            List<CategorieEleveBE> list = new List<CategorieEleveBE>();
            String nomcateleve;
            String codecateleve;
            CategorieEleveBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM CategorieEleve";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codecateleve = Convert.ToString(dataReader["codecateleve"]);
                        nomcateleve = Convert.ToString(dataReader["nomcateleve"]);
                        c = new CategorieEleveBE(codecateleve, nomcateleve);
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

        public override List<CategorieEleveBE> listerSuivantCritere(string critere)
        {
            List<CategorieEleveBE> list = new List<CategorieEleveBE>();
            String nomcateleve;
            String codecateleve;
            CategorieEleveBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM CategorieEleve WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codecateleve = Convert.ToString(dataReader["codecateleve"]);
                        nomcateleve = Convert.ToString(dataReader["nomcateleve"]);
                        c = new CategorieEleveBE(codecateleve, nomcateleve);
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
                cmd.CommandText = "SELECT * FROM CategorieEleve ";

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
                cmd.CommandText = "SELECT COUNT(*) FROM CategorieEleve";

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
        //-----------fin compter -------------------------------------------------

        public Boolean modifier(CategorieEleveBE S)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE categorieeleve SET nomcateleve=@nomCatEleve WHERE codecateleve=@codeCatEleve";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@nomCatEleve", S.nomCatEleve);
                cmd.Parameters.AddWithValue("@codeCatEleve", S.codeCatEleve);

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
    }
}
