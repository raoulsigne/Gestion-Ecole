using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    public class TypeevaluationDA : DA<TypeevaluationBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'un nouveau Type d'evaluation ------------------------------
        public override Boolean ajouter(TypeevaluationBE T)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO typeevaluation (CODEEVALUATION, NOMEVAL) VALUES (@codeE, @nomE)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeE", T.codeevaluation);
                cmd.Parameters.AddWithValue("@nomE", T.nomeval);

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

        //--------------------------Suppression d'une Type d'evaluation ------

        public override Boolean supprimer(TypeevaluationBE T)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM typeevaluation WHERE CODEEVALUATION=@codeE";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeE", T.codeevaluation);

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

        //--------------------------Modification d'une Type d'evaluation -----------

        // mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(TypeevaluationBE T, TypeevaluationBE  newT)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE typeevaluation SET NOMEVAL=@nomE, CODEEVALUATION=@codeE1 WHERE CODEEVALUATION=@codeE";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeE", T.codeevaluation);

                cmd.Parameters.AddWithValue("@codeE1", newT.codeevaluation);
                cmd.Parameters.AddWithValue("@nomE", newT.nomeval);

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

        //---------------Rechercher des informations sur une Type d'evaluation spécifique---------------------------------
        public override TypeevaluationBE rechercher(TypeevaluationBE type)
        {
            string NOMEVAL;
            TypeevaluationBE t;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM typeevaluation WHERE CODEEVALUATION=@codeE";
                cmd.Parameters.AddWithValue("@codeE", type.codeevaluation);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        dataReader.Read();
                        NOMEVAL = Convert.ToString(dataReader["NOMEVAL"]);

                        t = new TypeevaluationBE(type.codeevaluation, NOMEVAL);
                        dataReader.Close();
                        // this.con.fermer();
                        return t;
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
        public override List<TypeevaluationBE> listerTous()
        {
            List<TypeevaluationBE> list = new List<TypeevaluationBE>();
            String code;
            String nom;
            TypeevaluationBE t;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM typeevaluation";
                
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codeevaluation"]);
                        nom = Convert.ToString(dataReader["nomeval"]);
                        t = new TypeevaluationBE(code, nom);
                        list.Add(t);
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


        public override List<TypeevaluationBE> listerSuivantCritere(string critere)
        {
            List<TypeevaluationBE> list = new List<TypeevaluationBE>();
            String code;
            String nom;
            TypeevaluationBE t;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM typeevaluation WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codeevaluation"]);
                        nom = Convert.ToString(dataReader["nomeval"]);
                        t = new TypeevaluationBE(code, nom);
                        list.Add(t);
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
                cmd.CommandText = "SELECT * FROM typeevaluation";

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
                cmd.CommandText = "SELECT COUNT(*) FROM typeevaluation";

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
        //-----------fin compter -----------------


        public Boolean modifier(TypeevaluationBE T)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE typeevaluation SET NOMEVAL=@nomE WHERE CODEEVALUATION=@codeE";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeE", T.codeevaluation);
                cmd.Parameters.AddWithValue("@nomE", T.nomeval);

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
    }
}
