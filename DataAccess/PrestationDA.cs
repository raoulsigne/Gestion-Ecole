using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class PrestationDA : DA<PrestationBE>
    {
        private Connexion con = Connexion.getConnexion();

        //************************** création d'objet, parametre obj, retourne booléen
        public override Boolean ajouter(PrestationBE obj)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO prestation (codeprestation,nomprestation, priorite) VALUES (@codeprestation, @nomprestation, @priorite)";

                // utilisation de l'objet PrestationBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeprestation", obj.codePrestation);
                cmd.Parameters.AddWithValue("@nomprestation", obj.nomPrestation);
                cmd.Parameters.AddWithValue("@priorite", obj.priorite);

                cmd.Transaction = transaction;
                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
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
        //************************** FIN création d'objet, parametre obj, retourne booléen

        //***************************** suppression d'objet, parametre obj, retourne booléen
        public override Boolean supprimer(PrestationBE obj)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM prestation WHERE codeprestation=@codeprestation";

                // utilisation de l'objet PrestationBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeprestation", obj.codePrestation);

                cmd.Transaction = transaction;
                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
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
        //***************************** suppression d'objet, parametre obj, retourne booléen

        //*********************** mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(PrestationBE obj, PrestationBE newobj) {

            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE prestation SET codeprestation=@nouveaucodeprestation, nomprestation=@nomprestation, priorite=@priorite WHERE codeprestation=@anciencodeprestation";

                // utilisation de l'objet PaysBE passé en paramètre
                cmd.Parameters.AddWithValue("@nomprestation", newobj.nomPrestation);
                cmd.Parameters.AddWithValue("@nouveaucodeprestation", newobj.codePrestation);
                cmd.Parameters.AddWithValue("@anciencodeprestation", obj.codePrestation);

                cmd.Parameters.AddWithValue("@priorite", newobj.priorite);

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

        
        //*********************** FIN mise à jour d'objet, parametre obj, retourne booléen

        //*************************** rechercher d'un objet à partir de son code, parametre obj, retourne l'objet
        public override PrestationBE rechercher(PrestationBE prestation)
        {
            string codePrestation;
            string nomPrestation;
            int priorite;

            PrestationBE prestationBE;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM prestation WHERE codeprestation=@codeprestation";
                cmd.Parameters.AddWithValue("@codeprestation", prestation.codePrestation);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        dataReader.Read();
                        codePrestation = Convert.ToString(dataReader["codeprestation"]);
                        nomPrestation = Convert.ToString(dataReader["nomprestation"]);
                        priorite = Convert.ToInt16(dataReader["priorite"]);
                        prestationBE = new PrestationBE(codePrestation, nomPrestation, priorite);
                        dataReader.Close();
                        // this.con.fermer();
                        return prestationBE;
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
        //*************************** FIN rechercher d'un objet à partir de son code, parametre obj, retourne l'objet


        //******************************** retourner la liste de tout les objets
        public override List<PrestationBE> listerTous()
        {
            try
            {
                List<PrestationBE> listPrestationBE = new List<PrestationBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM prestation";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        PrestationBE prestationBE = new PrestationBE(Convert.ToString(dataReader["codeprestation"]), Convert.ToString(dataReader["nomprestation"]), Convert.ToInt16(dataReader["priorite"]));
                        listPrestationBE.Add(prestationBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listPrestationBE.Count != 0)
                        return listPrestationBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //******************************** FIN retourner la liste de tout les objets

        //********************** retourner la liste des objets qui correspondent à un certain critère
        public override List<PrestationBE> listerSuivantCritere(String critere)
        {
            try
            {
                List<PrestationBE> listobjBE = new List<PrestationBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM prestation WHERE " + critere;

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        PrestationBE objBE = new PrestationBE(Convert.ToString(dataReader["codeprestation"]), Convert.ToString(dataReader["nomprestation"]), Convert.ToInt16(dataReader["priorite"]));
                        listobjBE.Add(objBE);
                    }
                    dataReader.Close();
                    return listobjBE;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //********************** retourner la liste des objets qui correspondent à un certain critère


        public override List<string> listerValeursColonne(string colonne)
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM prestation";

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
                cmd.CommandText = "SELECT COUNT(*) FROM prestation";

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

        public Boolean modifier(PrestationBE obj)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE prestation SET nomprestation=@nomprestation, priorite=@priorite WHERE codeprestation=@codeprestation";

                // utilisation de l'objet PaysBE passé en paramètre
                cmd.Parameters.AddWithValue("@nomprestation", obj.nomPrestation);
                cmd.Parameters.AddWithValue("@codeprestation", obj.codePrestation);
                cmd.Parameters.AddWithValue("@priorite", obj.priorite);

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
