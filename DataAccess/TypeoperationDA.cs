using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    public class TypeoperationDA : DA<TypeoperationBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'un nouveau Type d'operation ------------------------------
        public override Boolean ajouter(TypeoperationBE T)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO typeoperation (CODETYPEOP, LIBELLETYPEOP) VALUES (@codeT, @nomT)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", T.codetypeop);
                cmd.Parameters.AddWithValue("@nomT", T.libelletypeop);

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

        //--------------------------Suppression d'un Type d'operation ------

        public override Boolean supprimer(TypeoperationBE T)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM typeoperation WHERE CODETYPEOP=@codeT";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", T.codetypeop);

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

        //--------------------------Modification d'un Type d'operation -----------

        // mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(TypeoperationBE T, TypeoperationBE newT)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE typeoperation SET LIBELLETYPEOP=@nomT, CODETYPEOP=@codeT1 WHERE CODETYPEOP=@codeT";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", T.codetypeop);

                cmd.Parameters.AddWithValue("@codeT1", newT.codetypeop);
                cmd.Parameters.AddWithValue("@nomT", newT.libelletypeop);

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

        public override TypeoperationBE rechercher(TypeoperationBE type)
        {
            string LIBELLETYPEOP;
            TypeoperationBE S;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM typeoperation WHERE CODETYPEOP=@codeT";
                cmd.Parameters.AddWithValue("@codeT", type.codetypeop);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        dataReader.Read();
                        LIBELLETYPEOP = Convert.ToString(dataReader["LIBELLETYPEOP"]);

                        S = new TypeoperationBE(type.codetypeop, LIBELLETYPEOP);
                        dataReader.Close();
                        // this.con.fermer();
                        return S;
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
        public override List<TypeoperationBE> listerTous()
        {
            List<TypeoperationBE> list = new List<TypeoperationBE>();
            String code;
            String libelle;
            TypeoperationBE type;
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM typeoperation";
                
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codetypeop"]);
                        libelle = Convert.ToString(dataReader["LIBELLETYPEOP"]);
                        type = new TypeoperationBE(code, libelle);
                        list.Add(type);
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


        public override List<TypeoperationBE> listerSuivantCritere(string critere)
        {
            List<TypeoperationBE> list = new List<TypeoperationBE>();
            String code;
            String libelle;
            TypeoperationBE type;
            
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM typeoperation WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codetypeop"]);
                        libelle = Convert.ToString(dataReader["LIBELLETYPEOP"]);
                        type = new TypeoperationBE(code, libelle);
                        list.Add(type);
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
                cmd.CommandText = "SELECT * FROM typeoperation";

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
                cmd.CommandText = "SELECT COUNT(*) FROM typeoperation";

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
    }
}
