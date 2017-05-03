using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    public class TrancheDA : DA<TrancheBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'une nouvelle Tranche ------------------------------
        public override Boolean ajouter(TrancheBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO tranche (CODETRANCHE, NOMTRANCHE) VALUES (@codeT, @nomT)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", S.codetranche);
                cmd.Parameters.AddWithValue("@nomT", S.nomtranche);

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

        //--------------------------Suppression d'une Tranche ------

        public override Boolean supprimer(TrancheBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM tranche WHERE CODETRANCHE=@codeT";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", S.codetranche);

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

        //--------------------------Modification d'une Tranche -----------

        // mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(TrancheBE S, TrancheBE newS)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE tranche SET NOMTRANCHE=@nomT, CODETRANCHE=@codeT1 WHERE CODETRANCHE=@codeT";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", S.codetranche);

                cmd.Parameters.AddWithValue("@codeT1", newS.codetranche);
                cmd.Parameters.AddWithValue("@nomT", newS.nomtranche);

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

        public override TrancheBE rechercher(TrancheBE tranche)
        {
            string NOMTRANCHE;
            TrancheBE t;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();
                t = new TrancheBE();
                // Requête SQL
                cmd.CommandText = "SELECT * FROM tranche WHERE CODETRANCHE=@codeT";
                cmd.Parameters.AddWithValue("@codeT", tranche.codetranche);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        NOMTRANCHE = Convert.ToString(dataReader["NOMTRANCHE"]);
                        t = new TrancheBE(tranche.codetranche, NOMTRANCHE);
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
        public override List<TrancheBE> listerTous()
        {
            List<TrancheBE> list = new List<TrancheBE>();
            string nom;
            String code;
            TrancheBE t;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Tranche";
                
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codetranche"]);
                        nom = Convert.ToString(dataReader["nomtranche"]);
                        t = new TrancheBE(code, nom);
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


        public override List<TrancheBE> listerSuivantCritere(string critere)
        {
            List<TrancheBE> list = new List<TrancheBE>();
            string nom;
            String code;
            TrancheBE t;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Tranche WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codetranche"]);
                        nom = Convert.ToString(dataReader["nomtranche"]);
                        t = new TrancheBE(code, nom);
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
                cmd.CommandText = "SELECT * FROM Tranche ";

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
                cmd.CommandText = "SELECT COUNT(*) FROM Tranche";

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

        public Boolean modifier(TrancheBE T)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE tranche SET nomtranche=@nomT WHERE codetranche=@codeT";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", T.codetranche);
                cmd.Parameters.AddWithValue("@nomT", T.nomtranche);

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
