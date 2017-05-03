using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class MagasinDA : DA<MagasinBE>
    {
        private Connexion con = Connexion.getConnexion();

        //********************* création d'objet, parametre obj, retourne booléen
        public override Boolean ajouter(MagasinBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO magasin (codemagasin,nommagasin) VALUES (@codeMagasin, @nomMagasin)";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeMagasin", obj.codeMagasin);
                cmd.Parameters.AddWithValue("@nomMagasin", obj.nomMagasin);

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
        //********************* FIN création d'objet, parametre obj, retourne booléen
        
        // ****************************** suppression d'objet, parametre obj, retourne booléen
        public override Boolean supprimer(MagasinBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM magasin WHERE codemagasin=@codeMagasin";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeMagasin", obj.codeMagasin);

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
        // ****************************** FIN suppression d'objet, parametre obj, retourne booléen
        
        //*************************** mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(MagasinBE obj, MagasinBE newobj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE magasin SET nommagasin=@nomMagasin, codemagasin=@codeMagasin WHERE codemagasin=@codeM";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@nomMagasin", newobj.nomMagasin);
                cmd.Parameters.AddWithValue("@codeMagasin", newobj.codeMagasin);

                cmd.Parameters.AddWithValue("@codeM", obj.codeMagasin);
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
        //*************************** FIN mise à jour d'objet, parametre obj, retourne booléen
       
        // rechercher d'un objet à partir de l'id, parametre obj, retourne l'objet
        public MagasinBE rechercher(int id) { return null; }

        // ******************* rechercher d'un objet à partir de son code, parametre obj, retourne l'objet
       //   param : @code : le code du magasin
        public override MagasinBE rechercher(MagasinBE mag) {
            MagasinBE magasin;
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM magasin WHERE codemagasin=@codeMagasin";
                cmd.Parameters.AddWithValue("@codeMagasin", mag.codeMagasin);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        dataReader.Read();
                        magasin = new MagasinBE(Convert.ToString(dataReader["codemagasin"]), Convert.ToString(dataReader["nommagasin"]));
                        dataReader.Close();
                        // this.con.fermer();
                        return magasin;
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
           
        public override List<MagasinBE> listerTous() {
            try
            {
                List<MagasinBE> listMagasin = new List<MagasinBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM magasin";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        MagasinBE magasin = new MagasinBE(Convert.ToString(dataReader["codemagasin"]), Convert.ToString(dataReader["nommagasin"]));
                        listMagasin.Add(magasin);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listMagasin.Count != 0)
                        return listMagasin;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        // ************************************ FIN retourner la liste de tout les objets
        
        // ************************************ retourner la liste des objets qui correspondent à un certain critère
        public override List<String> listerValeursColonne(String colonne) {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Magasin ";

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
        // ************************************ FIN retourner la liste des objets qui correspondent à un certain critère


        public override List<MagasinBE> listerSuivantCritere(string critere)
        {
            try
            {
                List<MagasinBE> listMagasin = new List<MagasinBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM magasin";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        MagasinBE magasin = new MagasinBE(Convert.ToString(dataReader["codemagasin"]), Convert.ToString(dataReader["nommagasin"]));
                        listMagasin.Add(magasin);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listMagasin.Count != 0)
                        return listMagasin;
                    else return null;
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
                cmd.CommandText = "SELECT COUNT(*) FROM magasin";

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

        public Boolean modifier(MagasinBE obj)
        {
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE magasin SET nommagasin=@nomMagasin WHERE codemagasin=@codeMagasin";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@nomMagasin", obj.nomMagasin);
                cmd.Parameters.AddWithValue("@codeMagasin", obj.codeMagasin);

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
                return true;
                // Fermeture de la connexion
                //  this.con.fermer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
