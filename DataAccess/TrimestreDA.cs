using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    class TrimestreDA : DA<TrimestreBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'une nouvelle Sequence ------------------------------
        public override Boolean ajouter(TrimestreBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO trimestre (CODETRIMESTRE, NOMTRIMESTRE) VALUES (@codeT, @nomT)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", S.codetrimestre);
                cmd.Parameters.AddWithValue("@nomT", S.nomtrimestre);

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

        //--------------------------Fin ajout-----------------------------

        //--------------------------Suppression d'une Sequence ------

        public override Boolean supprimer(TrimestreBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM trimestre WHERE CODETRIMESTRE=@codeT";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", S.codetrimestre);

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

        //--------------------------Fin Suppression-----------------------------

        //--------------------------Modification d'une Sequence -----------

        // mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(TrimestreBE T, TrimestreBE newT)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE trimestre SET CODETRIMESTRE=@nouveaucodeT, NOMTRIMESTRE=@nomT WHERE CODETRIMESTRE=@anciencodeT";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@nouveaucodeT", newT.codetrimestre);
                cmd.Parameters.AddWithValue("@anciencodeT", T.codetrimestre);
                cmd.Parameters.AddWithValue("@nomT", newT.nomtrimestre);

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

        public Boolean modifier(TrimestreBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            { 

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE trimestre SET NOMTRIMESTRE=@nomT WHERE CODETRIMESTRE=@codeT";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", S.codetrimestre);
                cmd.Parameters.AddWithValue("@nomT", S.nomtrimestre);

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

        //---------------------------Fin Modification ------------------------

        public override TrimestreBE rechercher(TrimestreBE trim)
        {
            string nomTrimestre;
            TrimestreBE T;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM trimestre WHERE CODETRIMESTRE=@codeT";
                cmd.Parameters.AddWithValue("@codeT", trim.codetrimestre);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        nomTrimestre = Convert.ToString(dataReader["NOMTRIMESTRE"]);
                        
                        T = new TrimestreBE(trim.codetrimestre, nomTrimestre);
                        dataReader.Close();
                        // this.con.fermer();
                        return T;
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
        public override List<TrimestreBE> listerTous()
        {
            List<TrimestreBE> list = new List<TrimestreBE>();
            String code;
            String nom;
            TrimestreBE t;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM trimestre";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        nom = Convert.ToString(dataReader["NOMTRIMESTRE"]);
                        code = Convert.ToString(dataReader["CODETRIMESTRE"]);

                        t = new TrimestreBE(code, nom);
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

        //liste les éléments respectant un critère passé en paramètre
        public override List<TrimestreBE> listerSuivantCritere(string critere)
        {
            List<TrimestreBE> list = new List<TrimestreBE>();
            String code;
            String nom;
            TrimestreBE t;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM trimestre WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        nom = Convert.ToString(dataReader["NOMTRIMESTRE"]);
                        code = Convert.ToString(dataReader["CODETRIMESTRE"]);

                        t = new TrimestreBE(code, nom);
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

        //liste les valeurs d'une colonne            
        public override List<string> listerValeursColonne(string colonne)
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM trimestre ";

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

        //fonction qui compte le nombre de séquence d'un trimestre
        public int getNombreSequenceDunTrimestre(String codeTrimestre)
        {

            int nbSequence;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT count(codeseq) FROM sequence WHERE codeTrimestre = '" + codeTrimestre + "'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    if (dataReader.Read())
                    {
                        nbSequence = Convert.ToInt16(dataReader["count(codeseq)"]);

                        dataReader.Close();

                        return nbSequence;
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        //fonction qui retourne la liste des codes de séquence d'un trimestre
        public List<String> getListCodeSequenceDunTrimestre(String codeTrimestre)
        {

            List<String> ListCodeSequence = new List<String>();
            String codeSequence;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT codeseq FROM sequence WHERE codeTrimestre = '" + codeTrimestre + "' ORDER BY codeseq";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeSequence = Convert.ToString(dataReader["codeseq"]);

                        ListCodeSequence.Add(codeSequence);
                    }

                    dataReader.Close();

                    return ListCodeSequence;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //fonction qui compte le nombre de  trimestre
        public int getNombreTrimestre()
        {

            int nbTrimestre;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT count(*) FROM trimestre";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    if (dataReader.Read())
                    {
                        nbTrimestre = Convert.ToInt16(dataReader["count(*)"]);

                        dataReader.Close();

                        return nbTrimestre;
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        //fonction qui retourne la liste des codes de trimestre
        public List<String> getListCodeTrimestre()
        {

            List<String> ListCodeSequence = new List<String>();
            String codeSequence;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT codeTrimestre FROM trimestre ORDER BY codeTrimestre";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeSequence = Convert.ToString(dataReader["codeTrimestre"]);

                        ListCodeSequence.Add(codeSequence);
                    }

                    dataReader.Close();

                    return ListCodeSequence;
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
