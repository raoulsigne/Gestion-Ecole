using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    class GroupeMatiereDA : DA<GroupeMatiereBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'une nouvelle programmation ------------------------------
        public override Boolean ajouter(GroupeMatiereBE gm)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO groupematiere (CODEGROUPE,NOMGROUPE) VALUES (@codegroupe,@nomgroupe)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codegroupe", gm.codegroupe);
                cmd.Parameters.AddWithValue("@nomgroupe", gm.nomgroupe);
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

        public override Boolean supprimer(GroupeMatiereBE gm)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM groupematiere WHERE CODEGROUPE=@code";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@code", gm.codegroupe);
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

        public override Boolean modifier(GroupeMatiereBE gm, GroupeMatiereBE newgm)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE groupematiere SET NOMGROUPE=@nomC, CODEGROUPE=@codeC WHERE CODEGROUPE=@code";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeC", newgm.codegroupe);
                cmd.Parameters.AddWithValue("@nomC", newgm.nomgroupe);
                cmd.Parameters.AddWithValue("@code", gm.codegroupe);

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

        public override GroupeMatiereBE rechercher(GroupeMatiereBE groupe)
        {
            GroupeMatiereBE gm;
            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM groupematiere WHERE CODEGROUPE=@code";
                cmd.Parameters.AddWithValue("@code", groupe.codegroupe);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        dataReader.Read();
                        gm = new GroupeMatiereBE(Convert.ToString(dataReader["codegroupe"]), Convert.ToString(dataReader["nomgroupe"]));
                        dataReader.Close();
                        // this.con.fermer();
                        return gm;
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

        public override List<GroupeMatiereBE> listerTous()
        {
            List<GroupeMatiereBE> list = new List<GroupeMatiereBE>();
            GroupeMatiereBE gm;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM groupematiere";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        gm = new GroupeMatiereBE(Convert.ToString(dataReader["codegroupe"]), Convert.ToString(dataReader["nomgroupe"]));
                        list.Add(gm);
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

        public override List<GroupeMatiereBE> listerSuivantCritere(string critere)
        {
            List<GroupeMatiereBE> list = new List<GroupeMatiereBE>();
            GroupeMatiereBE gm;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM groupematiere WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        gm = new GroupeMatiereBE(Convert.ToString(dataReader["codegroupe"]), Convert.ToString(dataReader["nomgroupe"]));
                        list.Add(gm);
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
                cmd.CommandText = "SELECT * FROM groupematiere";

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

        public Boolean modifier(GroupeMatiereBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE groupematiere SET NOMGROUPE=@nomC WHERE CODEGROUPE=@codeC";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeC", entity.codegroupe);
                cmd.Parameters.AddWithValue("@nomC", entity.nomgroupe);

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
    }
}
