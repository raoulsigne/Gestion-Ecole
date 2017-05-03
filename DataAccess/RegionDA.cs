using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;

using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    public class RegionDA : DA<RegionBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'une nouvelle region ------------------------------
        public override Boolean ajouter(RegionBE R)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO region (CODEREGION, NOMREGION) VALUES (@codeR, @nomR)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeR", R.coderegion);
                cmd.Parameters.AddWithValue("@nomR", R.nomregion);

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

        //--------------------------Fin ajout-----------------------------

        //--------------------------Suppression d'une region ------

        public override Boolean supprimer(RegionBE R)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM region WHERE CODEREGION=@codeR";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeR", R.coderegion);

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

        //--------------------------Modification d'une region -----------

        // mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(RegionBE R, RegionBE newR)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE region SET NOMREGION=@nomR, CODEREGION=@codeR WHERE CODEREGION=@code";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@nomR", newR.nomregion);
                cmd.Parameters.AddWithValue("@codeR", newR.coderegion);
                cmd.Parameters.AddWithValue("@code", R.coderegion);
                
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

        //---------------Rechercher des informations sur une region spécifique---------------------------------
        
        public override RegionBE rechercher(RegionBE region)
        {
            string nomregion;

            RegionBE R;
            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM region WHERE CODEREGION=@code1";
                cmd.Parameters.AddWithValue("@code1", region.coderegion);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        dataReader.Read();
                        nomregion = Convert.ToString(dataReader["NOMREGION"]);
                        R = new RegionBE(region.coderegion, nomregion);
                        dataReader.Close();
                        // this.con.fermer();
                        return R;
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
        public override List<RegionBE> listerTous()
        {
            List<RegionBE> list = new List<RegionBE>();
            String code;
            String nom;
            RegionBE r;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Region";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["coderegion"]);
                        nom = Convert.ToString(dataReader["nomregion"]);
                        r = new RegionBE(code, nom);
                        list.Add(r);
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


        public override List<RegionBE> listerSuivantCritere(string critere)
        {
            List<RegionBE> list = new List<RegionBE>();
            String code;
            String nom;
            RegionBE r;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Region WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["coderegion"]);
                        nom = Convert.ToString(dataReader["nomregion"]);
                        r = new RegionBE(code, nom);
                        list.Add(r);
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
                cmd.CommandText = "SELECT * FROM Region";

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
                cmd.CommandText = "SELECT COUNT(*) FROM Region";

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
        //-----------------------------Fin de la recherche----------
    }
}
