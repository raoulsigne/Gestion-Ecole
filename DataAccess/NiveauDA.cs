using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class NiveauDA : DA<NiveauBE>
    {
        private Connexion con = Connexion.getConnexion();

        //******************************** création d'objet, parametre obj, retourne booléen
        public override Boolean ajouter(NiveauBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO niveau (codeniveau,nomniveau,niveau) VALUES (@codeNiveau, @nomNiveau, @niveau)";

                // utilisation de l'objet NiveauBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeNiveau", obj.codeNiveau);
                cmd.Parameters.AddWithValue("@nomNiveau", obj.nomNiveau);
                cmd.Parameters.AddWithValue("@niveau", obj.niveau);

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
        //******************************** FIN création d'objet, parametre obj, retourne booléen
        
        //****************************** suppression d'objet, parametre obj, retourne booléen
        public override Boolean supprimer(NiveauBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM niveau WHERE codeniveau=@codeNiveau";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeNiveau", obj.codeNiveau);

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
        //****************************** FIN suppression d'objet, parametre obj, retourne booléen
        
        //****************************** mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(NiveauBE obj, NiveauBE newobj) {
            MySqlTransaction tx = con.connexion.BeginTransaction(); 
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE niveau SET nomniveau=@nomNiveau, niveau=@niveau, codeniveau=@codeNiveau WHERE codeniveau=@code";

                // utilisation de l'objet NiveauBE passé en paramètre
                cmd.Parameters.AddWithValue("@nomNiveau", newobj.nomNiveau);
                cmd.Parameters.AddWithValue("@niveau", newobj.niveau);
                cmd.Parameters.AddWithValue("@codeNiveau", newobj.codeNiveau);

                cmd.Parameters.AddWithValue("@code", obj.codeNiveau);
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
        
        // rechercher d'un objet à partir de son code, parametre obj, retourne l'objet
        public override NiveauBE rechercher(NiveauBE nivo) {
            string codeNiveau;
            string nomNiveau;
            int niveau;

            NiveauBE m;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM niveau WHERE codeniveau=@codeNiveau";
                cmd.Parameters.AddWithValue("@codeNiveau", nivo.codeNiveau);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        dataReader.Read();
                        codeNiveau = Convert.ToString(dataReader["codeniveau"]);
                        nomNiveau = Convert.ToString(dataReader["nomniveau"]);
                        niveau = Convert.ToInt16(dataReader["niveau"]);
                        m = new NiveauBE(codeNiveau, nomNiveau, niveau);
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
        //*************************** FIN rechercher d'un objet à partir de l'id, parametre obj, retourne l'objet
        
        //*************************** retourner la liste de tout les objets
        public override List<NiveauBE> listerTous()
        {
            try
            {
                List<NiveauBE> listNivBE = new List<NiveauBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM niveau ORDER BY NIVEAU";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        NiveauBE nivBE = new NiveauBE(Convert.ToString(dataReader["codeniveau"]), Convert.ToString(dataReader["nomniveau"]), Convert.ToInt16(dataReader["niveau"]));
                        listNivBE.Add(nivBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listNivBE.Count != 0)
                        return listNivBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //*************************** FIN retourner la liste de tout les objets
        
        //***************************** retourner la liste des objets qui correspondent à un certain critère
        public override List<NiveauBE> listerSuivantCritere(String critere)
        {
            try
            {
                List<NiveauBE> listobjBE = new List<NiveauBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM niveau WHERE " + critere;

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        NiveauBE objBE = new NiveauBE(Convert.ToString(dataReader["codeniveau"]), Convert.ToString(dataReader["nomniveau"]), Convert.ToInt16(dataReader["niveau"]));
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
        //***************************** FIN retourner la liste des objets qui correspondent à un certain critère

        public override List<string> listerValeursColonne(string colonne)
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM niveau";

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
                cmd.CommandText = "SELECT COUNT(*) FROM niveau";

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

        public Boolean modifier(NiveauBE obj)
        {
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE niveau SET nomniveau=@nomNiveau, niveau=@niveau WHERE codeniveau=@codeNiveau";

                // utilisation de l'objet NiveauBE passé en paramètre
                cmd.Parameters.AddWithValue("@nomNiveau", obj.nomNiveau);
                cmd.Parameters.AddWithValue("@niveau", obj.niveau);
                cmd.Parameters.AddWithValue("@codeNiveau", obj.codeNiveau);

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
