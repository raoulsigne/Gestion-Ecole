using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class PaysDA : DA<PaysBE>
    {
        private Connexion con = Connexion.getConnexion();

        //*************************** création d'objet, parametre obj, retourne booléen
        public override Boolean ajouter(PaysBE obj) {

            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO pays (codepays,nompays,nationalite) VALUES (@codepays, @nompays, @nationalite)";

                // utilisation de l'objet PaysBE passé en paramètre
                cmd.Parameters.AddWithValue("@codepays", obj.codePays);
                cmd.Parameters.AddWithValue("@nompays", obj.nomPays);
                cmd.Parameters.AddWithValue("@nationalite", obj.nationalite);

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
        //*************************** FIN création d'objet, parametre obj, retourne booléen

        //*************************** suppression d'objet, parametre obj, retourne booléen
        public override Boolean supprimer(PaysBE obj) {

            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM pays WHERE codepays=@codepays";

                // utilisation de l'objet PaysBE passé en paramètre
                cmd.Parameters.AddWithValue("@codepays", obj.codePays);

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
        //*************************** FIN suppression d'objet, parametre obj, retourne booléen
        
        //***************************** mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(PaysBE obj, PaysBE newobj) {

            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE pays SET codepays=@codepays, nompays=@nompays ,nationalite=@nationalite WHERE codepays=@code";

                // utilisation de l'objet PaysBE passé en paramètre
                cmd.Parameters.AddWithValue("@nompays", newobj.nomPays);
                cmd.Parameters.AddWithValue("@nationalite", newobj.nationalite);
                cmd.Parameters.AddWithValue("@codepays", newobj.codePays);
                cmd.Parameters.AddWithValue("@code", obj.codePays);

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
        //***************************** FIN mise à jour d'objet, parametre obj, retourne booléen

        //*********************** rechercher d'un objet à partir de son code, parametre obj, retourne l'objet
        public override PaysBE rechercher(PaysBE pays) {
            string codepays;
            string nompays;
            string nationalite;

            PaysBE paysBE;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM pays WHERE codepays=@codepays";
                cmd.Parameters.AddWithValue("@codepays", pays.codePays);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        dataReader.Read();
                        codepays = Convert.ToString(dataReader["codepays"]);
                        nompays = Convert.ToString(dataReader["nompays"]);
                        nationalite = Convert.ToString(dataReader["nationalite"]);
                        paysBE = new PaysBE(codepays, nompays, nationalite);
                        dataReader.Close();
                        // this.con.fermer();
                        return paysBE;
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
        //*********************** rechercher d'un objet à partir de son code, parametre obj, retourne l'objet

        //***************************** retourner la liste de tout les objets
        public override List<PaysBE> listerTous()
        {
            try
            {
                List<PaysBE> listPaysBE = new List<PaysBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM pays";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        PaysBE paysBE = new PaysBE(Convert.ToString(dataReader["codepays"]), Convert.ToString(dataReader["nompays"]), Convert.ToString(dataReader["nationalite"]));
                        listPaysBE.Add(paysBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listPaysBE.Count != 0)
                        return listPaysBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //***************************** FIN retourner la liste de tout les objets
        
        // retourner la liste des objets qui correspondent à un certain critère
        public override List<PaysBE> listerSuivantCritere(String critere)
        {
            try
            {
                List<PaysBE> listobjBE = new List<PaysBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM pays WHERE " + critere;

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        PaysBE objBE = new PaysBE(Convert.ToString(dataReader["codepays"]), Convert.ToString(dataReader["nompays"]), Convert.ToString(dataReader["nationalite"]));
                        listobjBE.Add(objBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    //return list to be displayed
                    if (listobjBE.Count != 0)
                        return listobjBE;
                    else return null;
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
                cmd.CommandText = "SELECT * FROM pays";

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
                cmd.CommandText = "SELECT COUNT(*) FROM pays";

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
    }
}
