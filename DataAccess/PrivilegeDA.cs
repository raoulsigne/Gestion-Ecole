using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class PrivilegeDA : DA<PrivilegeBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------MOI-------------------------------------------------------------------------------
        //*********************** création d'objet, parametre obj, retourne booléen
        public override Boolean ajouter(PrivilegeBE obj)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL

                cmd.CommandText = "INSERT INTO privilege (codeprivilege,nomprivilege) VALUES (@codeprivilege, @nomprivilege)";

                // utilisation de l'objet PrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeprivilege", obj.codePrivilege);
                cmd.Parameters.AddWithValue("@nomprivilege", obj.nomPrivilege);

                cmd.Transaction = transaction;

                // Exécution de la commande SQL

                cmd.ExecuteNonQuery();

                transaction.Commit();

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                transaction.Rollback();

                return false;
            }
        }
        //*********************** création d'objet, parametre obj, retourne booléen


        public Boolean ajouterRemplacer(PrivilegeBE obj)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL

                cmd.CommandText = "REPLACE INTO privilege (codeprivilege,nomprivilege) VALUES (@codeprivilege, @nomprivilege)";

                // utilisation de l'objet PrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeprivilege", obj.codePrivilege);
                cmd.Parameters.AddWithValue("@nomprivilege", obj.nomPrivilege);

                cmd.Transaction = transaction;

                // Exécution de la commande SQL

                cmd.ExecuteNonQuery();

                transaction.Commit();


                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                transaction.Rollback();

                return false;
            }
        }
        //*********************** création d'objet, parametre obj, retourne booléen




        //****************************** suppression d'objet, parametre obj, retourne booléen
        public override Boolean supprimer(PrivilegeBE obj)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM privilege WHERE codeprivilege=@codeprivilege";

                // utilisation de l'objet PrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeprivilege", obj.codePrivilege);

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
        //****************************** FIN suppression d'objet, parametre obj, retourne booléen

        //****************************** suppression de toute la table booléen
        public Boolean supprimerTout()
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM privilege";

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
        //****************************** FIN suppression d'objet, parametre obj, retourne booléen


        //********************* mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(PrivilegeBE obj, PrivilegeBE newobj)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE privilege SET codeprivilege=@codeprivilege, nomprivilege=@nomprivilege WHERE codeprivilege=@code";

                // utilisation de l'objet PrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@nomprivilege", newobj.nomPrivilege);
                cmd.Parameters.AddWithValue("@codeprivilege", newobj.codePrivilege);

                cmd.Parameters.AddWithValue("@code", obj.codePrivilege);

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

                transaction.Commit();

                return false;
            }
        }
        //********************* FIN mise à jour d'objet, parametre obj, retourne booléen

        // rechercher d'un objet à partir de l'id, parametre obj, retourne l'objet
        public PrivilegeBE rechercher(int id) { return null; }

        //*********************** rechercher d'un objet à partir de son code, parametre obj, retourne l'objet
        public override PrivilegeBE rechercher(PrivilegeBE privilege)
        {
            string codePrivilege;
            string nomPrivilege;

            PrivilegeBE privilegeBE;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM privilege WHERE codeprivilege=@codeprivilege";
                cmd.Parameters.AddWithValue("@codeprivilege", privilege.codePrivilege);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        codePrivilege = Convert.ToString(dataReader["codeprivilege"]);
                        nomPrivilege = Convert.ToString(dataReader["nomprivilege"]);
                        privilegeBE = new PrivilegeBE(codePrivilege, nomPrivilege);
                        dataReader.Close();
                        // this.con.fermer();
                        return privilegeBE;
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
        //*********************** FIN rechercher d'un objet à partir de son code, parametre obj, retourne l'objet

        //*************************** retourner la liste de tout les objets
        public override List<PrivilegeBE> listerTous()
        {
            try
            {
                List<PrivilegeBE> listPrivilegeBE = new List<PrivilegeBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM privilege";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        PrivilegeBE privilegeBE = new PrivilegeBE(Convert.ToString(dataReader["codeprivilege"]), Convert.ToString(dataReader["nomprivilege"]));
                        listPrivilegeBE.Add(privilegeBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listPrivilegeBE.Count != 0)
                        return listPrivilegeBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //*************************** retourner la liste de tout les objets

        //************************* retourner la liste des objets qui correspondent à un certain critère
        public override List<PrivilegeBE> listerSuivantCritere(String critere)
        {
            try
            {
                List<PrivilegeBE> listobjBE = new List<PrivilegeBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM privilege WHERE " + critere;

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        PrivilegeBE objBE = new PrivilegeBE(Convert.ToString(dataReader["codeprivilege"]), Convert.ToString(dataReader["nomprivilege"]));
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
        //************************* retourner la liste des objets qui correspondent à un certain critère


        public override List<string> listerValeursColonne(string colonne)
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM privilege";

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
                cmd.CommandText = "SELECT COUNT(*) FROM privilege";

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
