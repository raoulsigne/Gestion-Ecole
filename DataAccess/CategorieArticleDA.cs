using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class CategorieArticleDA : DA<CategorieArticleBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------ajouter CategorieArticle -----------------//
        public override Boolean ajouter(CategorieArticleBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO CategorieArticle (codecatarticle,nomcatarticle) VALUES (@code, @nom)";
                cmd.Parameters.AddWithValue("@code", entity.codeCatArticle);
                cmd.Parameters.AddWithValue("@nom", entity.nomCatArticle);

                // Exécution de la commande SQL
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();
                tx.Commit();

                return true;
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //----------------fin ajouter -----------------//

        //----------------debut supprimer -----------------//
        public override Boolean supprimer(CategorieArticleBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM CategorieArticle WHERE codecatarticle = @code";
                cmd.Parameters.AddWithValue("@code", entity.codeCatArticle);

                // Exécution de la commande SQL
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();
                tx.Commit();

                return true;
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //----------------fin supprimer ---------------------//

        //----------------chercher Acheter -----------------//
        public override CategorieArticleBE rechercher(CategorieArticleBE entity)
        {
            String nomcatarticle;
            String codecatarticle;
            CategorieArticleBE c;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM CategorieArticle WHERE codecatarticle=@code";
                cmd.Parameters.AddWithValue("@code", entity.codeCatArticle);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        codecatarticle = Convert.ToString(dataReader["codecatarticle"]);
                        nomcatarticle = Convert.ToString(dataReader["nomcatarticle"]);
                        c = new CategorieArticleBE(codecatarticle, nomcatarticle);
                        dataReader.Close();
                        return c;
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
        //----------------Fin chercher ------------------------------------//

        //----------------debut modifier ---------------//
        public override Boolean modifier(CategorieArticleBE entity, CategorieArticleBE newEntity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE categoriearticle SET nomcatarticle=@nomcatarticle, codecatarticle=@code WHERE codecatarticle=@codecatarticle";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@nomcatarticle", newEntity.nomCatArticle);
                cmd.Parameters.AddWithValue("@code", newEntity.codeCatArticle);

                cmd.Parameters.AddWithValue("@codecatarticle", entity.codeCatArticle);

                // Exécution de la commande SQL
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();
                tx.Commit();

                return true;
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //----------------fin modifier ------------------------------------//

        //----------------debut lister --------------------------------------------
        public override List<CategorieArticleBE> listerTous()
        {
            List<CategorieArticleBE> list = new List<CategorieArticleBE>();
            String nomcatarticle;
            String codecatarticle;
            CategorieArticleBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM CategorieArticle";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codecatarticle = Convert.ToString(dataReader["codecatarticle"]);
                        nomcatarticle = Convert.ToString(dataReader["nomcatarticle"]);
                        c = new CategorieArticleBE(codecatarticle, nomcatarticle);
                        list.Add(c);
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


        public override List<CategorieArticleBE> listerSuivantCritere(string critere)
        {
            List<CategorieArticleBE> list = new List<CategorieArticleBE>();
            String nomcatarticle;
            String codecatarticle;
            CategorieArticleBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM CategorieArticle WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codecatarticle = Convert.ToString(dataReader["codecatarticle"]);
                        nomcatarticle = Convert.ToString(dataReader["nomcatarticle"]);
                        c = new CategorieArticleBE(codecatarticle, nomcatarticle);
                        list.Add(c);
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
                cmd.CommandText = "SELECT * FROM CategorieArticle ";

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
                cmd.CommandText = "SELECT COUNT(*) FROM CategorieArticle";

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
        //-----------fin compter -------------------------------------------------

        public Boolean modifier(CategorieArticleBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE categoriearticle SET nomcatarticle=@nomCatArticle WHERE codecatarticle=@codeCatArticle";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@nomCatArticle", entity.nomCatArticle);
                cmd.Parameters.AddWithValue("@codeCatArticle", entity.codeCatArticle);

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
