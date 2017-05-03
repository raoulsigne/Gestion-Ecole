using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class OperationDA : DA<OperationBE>
    {
        private Connexion con = Connexion.getConnexion();

        //********************************** création d'objet, parametre obj, retourne booléen
        public override Boolean ajouter(OperationBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO operation (codeop,codetypeop,libelleop) VALUES (@codeOp, @codeTypeOp, @libelleOp)";

                // utilisation de l'objet OperationBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeOp", obj.codeOp);
                cmd.Parameters.AddWithValue("@codeTypeOp", obj.codeTypeOp);
                cmd.Parameters.AddWithValue("@libelleOp", obj.libelleOp);

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
        //********************************** création d'objet, parametre obj, retourne booléen
        
        //*************************** suppression d'objet, parametre obj, retourne booléen
        public override Boolean supprimer(OperationBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM operation WHERE codeop=@codeOp";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeOp", obj.codeOp);

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
        //*************************** FIN suppression d'objet, parametre obj, retourne booléen
        
        //****************************** mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(OperationBE obj, OperationBE newobj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE operation SET codetypeop=@codeTypeOp, libelleop=@libelleOp, codeop=@codeOp WHERE codeop=@code";

                // utilisation de l'objet OperationBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeTypeOp", newobj.codeTypeOp);
                cmd.Parameters.AddWithValue("@libelleOp", newobj.libelleOp);
                cmd.Parameters.AddWithValue("@codeOp", newobj.codeOp);

                cmd.Parameters.AddWithValue("@code", obj.codeOp);
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
        //****************************** FIN mise à jour d'objet, parametre obj, retourne booléen
        
        //***************************** rechercher d'un objet à partir de son code, parametre obj, retourne l'objet
        public override OperationBE rechercher(OperationBE operation) {
            string codeOp;
            string codeTypeOP;
            string libelleOp;

            OperationBE m;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM operation WHERE codeop=@codeOp";
                cmd.Parameters.AddWithValue("@codeOp", operation.codeOp);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        codeOp = Convert.ToString(dataReader["codeop"]);
                        codeTypeOP = Convert.ToString(dataReader["codetypeop"]);
                        libelleOp = Convert.ToString(dataReader["libelleop"]);
                        m = new OperationBE(codeOp, codeTypeOP, libelleOp);
                        dataReader.Close();
                        // this.con.fermer();
                        return m;
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
        //***************************** FIN rechercher d'un objet à partir de son code, parametre obj, retourne l'objet
        
        //******************************** retourner la liste de tout les objets
        public override List<OperationBE> listerTous()
        {
            try
            {
                List<OperationBE> listOPBE = new List<OperationBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM operation";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        OperationBE opBE = new OperationBE(Convert.ToString(dataReader["codeop"]), Convert.ToString(dataReader["codetypeop"]), Convert.ToString(dataReader["libelleop"]));
                        listOPBE.Add(opBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listOPBE.Count != 0)
                        return listOPBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //******************************** retourner la liste de tout les objets
        
        //******************************** retourner la liste des objets qui correspondent à un certain critère
        public override List<OperationBE> listerSuivantCritere(String critere)
        {
            try
            {
                List<OperationBE> listobjBE = new List<OperationBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM operation WHERE " + critere;

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        OperationBE objBE = new OperationBE(Convert.ToString(dataReader["codeop"]), Convert.ToString(dataReader["codetypeop"]), Convert.ToString(dataReader["libelleop"]));
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
        //******************************** retourner la liste des objets qui correspondent à un certain critère

        public override List<string> listerValeursColonne(string colonne)
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM operation";

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
                cmd.CommandText = "SELECT COUNT(*) FROM operation";

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
