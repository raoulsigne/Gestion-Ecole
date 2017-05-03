using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    public class SerieDA : DA<SerieBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'une nouvelle Serie ------------------------------
        public override Boolean ajouter(SerieBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO serie (CODESERIE, NOMSERIE) VALUES (@codeS, @nomS)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", S.codeserie);
                cmd.Parameters.AddWithValue("@nomS", S.nomserie);

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

        //--------------------------Suppression d'une Serie ------

        public override Boolean supprimer(SerieBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM serie WHERE CODESERIE=@codeS";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", S.codeserie);

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

        //--------------------------Modification d'une Serie -----------

        // mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(SerieBE AncienObj, SerieBE NouveauObj)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE serie SET NOMSERIE=@nomS, CODESERIE=@codeS1 WHERE CODESERIE=@codeS";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", AncienObj.codeserie);

                cmd.Parameters.AddWithValue("@codeS1", NouveauObj.codeserie);
                cmd.Parameters.AddWithValue("@nomS", NouveauObj.nomserie);

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
        public Boolean modifier(SerieBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE serie SET NOMSERIE=@nomS WHERE CODESERIE=@codeS";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", S.codeserie);
                cmd.Parameters.AddWithValue("@nomS", S.nomserie);

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

        public Boolean remplacer(SerieBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "REPLACE INTO serie (CODESERIE, NOMSERIE) VALUES (@codeS, @nomS)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", S.codeserie);
                cmd.Parameters.AddWithValue("@nomS", S.nomserie);

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

        //---------------Rechercher des informations sur une Serie spécifique---------------------------------


        public override SerieBE rechercher(SerieBE serie)
        {
            string NOMSERIE;
            SerieBE S;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM serie WHERE CODESERIE=@codeS";
                cmd.Parameters.AddWithValue("@codeS", serie.codeserie);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la série à retourner
                    if (dataReader.Read())
                    {
                        dataReader.Read();
                        NOMSERIE = Convert.ToString(dataReader["NOMSERIE"]);

                        S = new SerieBE(serie.codeserie, NOMSERIE);
                        dataReader.Close();
                        // this.con.fermer();
                        return S;
                    }


                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            //-----------------------------Fin de la recherche----------
        }


        // retourner la liste de tout les objets
        public override List<SerieBE> listerTous()
        {
            List<SerieBE> list = new List<SerieBE>();
            String nomserie;
            String codeserie;
            SerieBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM serie";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeserie = Convert.ToString(dataReader["codeserie"]);
                        nomserie = Convert.ToString(dataReader["nomserie"]);
                        c = new SerieBE(codeserie, nomserie);
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

        // retourner la liste des objets qui correspondent à un certain critère
        public override List<SerieBE> listerSuivantCritere(string critere)
        {
            List<SerieBE> list = new List<SerieBE>();
            String nomserie;
            String codeserie;
            SerieBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM serie WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeserie = Convert.ToString(dataReader["codeserie"]);
                        nomserie = Convert.ToString(dataReader["nomserie"]);
                        c = new SerieBE(codeserie, nomserie);
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

        /*
         * lister tous les donnees d'une colonne de la table
         * @param colonne est le nom de la colonne à lister
         */
        public override List<String> listerValeursColonne(String colonne) 
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM serie";

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

    }
}
