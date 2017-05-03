using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class MentionDA : DA<MentionBE>
    {
        private Connexion con = Connexion.getConnexion();

        //******************************** création d'objet, parametre obj, retourne booléen
        public override Boolean ajouter(MentionBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO mention (idmention,notemin,notemax,mention) VALUES (@idMention, @noteMin, @noteMax, @mention)";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@idMention", obj.idMention);
                cmd.Parameters.AddWithValue("@noteMin", obj.noteMin);
                cmd.Parameters.AddWithValue("@noteMax", obj.noteMax);
                cmd.Parameters.AddWithValue("@mention", obj.mention);

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
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //******************************** FIN création d'objet, parametre obj, retourne booléen
        
        //******************************* suppression d'objet, parametre obj, retourne booléen
        public override Boolean supprimer(MentionBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction(); 
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM mention WHERE idmention=@idMention";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@idMention", obj.idMention);

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
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //*******************************FIN suppression d'objet, parametre obj, retourne booléen
        
        //***************************** mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(MentionBE obj, MentionBE newobj) {
            MySqlTransaction tx = con.connexion.BeginTransaction(); 
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE mention SET notemin=@noteMin, notemax=@noteMax, mention=@mention WHERE idmention=@idMention";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@noteMin", newobj.noteMin);
                cmd.Parameters.AddWithValue("@noteMax", newobj.noteMax);
                cmd.Parameters.AddWithValue("@mention", newobj.mention);
                cmd.Parameters.AddWithValue("@idMention", obj.idMention);

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
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //***************************** FIN mise à jour d'objet, parametre obj, retourne booléen
       
        //***************************** rechercher d'un objet à partir de l'id, parametre id, retourne l'objet
        public override MentionBE rechercher(MentionBE m) {
            int idMention;
            double noteMin;
            double noteMax;
            string mention;
            MentionBE me;

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM mention WHERE idmention=@idMention";
                cmd.Parameters.AddWithValue("@idMention", m.idMention);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        dataReader.Read();
                        idMention = Convert.ToInt16(dataReader["idmention"]);
                        noteMin = Convert.ToDouble(dataReader["notemin"]);
                        noteMax = Convert.ToDouble(dataReader["notemax"]);
                        mention = Convert.ToString(dataReader["mention"]);
                        me = new MentionBE(idMention, noteMin, noteMax, mention);
                        dataReader.Close();
                        // this.con.fermer();
                        return me;
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
        //***************************** FIN rechercher d'un objet à partir de l'id, parametre id, retourne l'objet
        

        //**************************************** retourner la liste de tout les objets
        public override List<MentionBE> listerTous() {
            try
            {
                List<MentionBE> listMentBE = new List<MentionBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM mention";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        MentionBE mentBE = new MentionBE(Convert.ToInt16(dataReader["idmention"]), Convert.ToDouble(dataReader["notemin"]), Convert.ToDouble(dataReader["notemax"]), Convert.ToString(dataReader["mention"]));
                        listMentBE.Add(mentBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listMentBE.Count != 0)
                        return listMentBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //**************************************** FIN retourner la liste de tout les objets
        
        //**************************************** retourner la liste des objets qui correspondent à un certain critère
        public override List<MentionBE> listerSuivantCritere(String critere)
        {
            try
            {
                List<MentionBE> listobjBE = new List<MentionBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM mention WHERE " + critere;

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        MentionBE objBE = new MentionBE(Convert.ToInt16(dataReader["idmention"]), Convert.ToDouble(dataReader["notemin"]), Convert.ToDouble(dataReader["notemax"]), Convert.ToString(dataReader["mention"]));
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
        //**************************************** FIN retourner la liste des objets qui correspondent à un certain critère

        public override List<string> listerValeursColonne(string colonne)
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM mention";

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
                cmd.CommandText = "SELECT COUNT(*) FROM mention";

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

        //retourne la mention connaissant la moyenne
        public String getMention(Double moyenne) {

            List<String> listobjBE = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT mention FROM mention WHERE notemin <= @moyenne AND @moyenne <notemax;";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@moyenne", moyenne);

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        String mention = Convert.ToString(dataReader["mention"]);
                        listobjBE.Add(mention);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listobjBE.Count != 0)
                        return listobjBE.ElementAt(0);
                    else return null;
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
