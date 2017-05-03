using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class ArticleDA : DA<ArticleBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------ajouter Article -----------------//
        public override Boolean ajouter(ArticleBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO Article (codearticle, codecatarticle, designation) VALUES (@article, @catarticle, @designation)";
                cmd.Parameters.AddWithValue("@article", entity.codeArticle);
                cmd.Parameters.AddWithValue("@catarticle", entity.codeCatArticle);
                cmd.Parameters.AddWithValue("@designation", entity.designation);

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
        public override Boolean supprimer(ArticleBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Article WHERE codearticle = @code";
                cmd.Parameters.AddWithValue("@code", entity.codeArticle);

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
        public override ArticleBE rechercher(ArticleBE entity)
        {
            String codearticle;
            String codecatarticle;
            String designation;
            ArticleBE a;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM article WHERE codearticle=@code";
                cmd.Parameters.AddWithValue("@code", entity.codeArticle);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        codearticle = Convert.ToString(dataReader["codearticle"]);
                        codecatarticle = Convert.ToString(dataReader["codecatarticle"]);
                        designation = Convert.ToString(dataReader["designation"]);
                        a = new ArticleBE(codearticle, codecatarticle, designation);
                        dataReader.Close();
                        return a;
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
        public override Boolean modifier(ArticleBE entity, ArticleBE newEntity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE article SET codearticle=@code, designation=@designation, codecatarticle=@codecatarticle WHERE codearticle=@codearticle";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@code", newEntity.codeArticle);
                cmd.Parameters.AddWithValue("@designation", newEntity.designation);
                cmd.Parameters.AddWithValue("@codecatarticle", newEntity.codeCatArticle);

                cmd.Parameters.AddWithValue("@codearticle", entity.codeArticle);
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
        public override List<ArticleBE> listerTous()
        {
            List<ArticleBE> list = new List<ArticleBE>();
            String code;
            String codecatarticle;
            String designation;
            ArticleBE a;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Article";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codearticle"]);
                        codecatarticle = Convert.ToString(dataReader["codecatarticle"]);
                        designation = Convert.ToString(dataReader["designation"]);
                        a = new ArticleBE(code, codecatarticle, designation);
                        list.Add(a);
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

        public override List<ArticleBE> listerSuivantCritere(string critere)
        {
            List<ArticleBE> list = new List<ArticleBE>();
            String code;
            String codecatarticle;
            String designation;
            ArticleBE a;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Article WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codearticle"]);
                        codecatarticle = Convert.ToString(dataReader["codecatarticle"]);
                        designation = Convert.ToString(dataReader["designation"]);
                        a = new ArticleBE(code, codecatarticle, designation);
                        list.Add(a);
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
                cmd.CommandText = "SELECT * FROM Article";

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
                cmd.CommandText = "SELECT COUNT(*) FROM Article";

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

        public Boolean modifier(ArticleBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE article SET codecatarticle=@codecatarticle, designation=@designation WHERE codearticle=@codearticle";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codecatarticle", entity.codeCatArticle);
                cmd.Parameters.AddWithValue("@designation", entity.designation);
                cmd.Parameters.AddWithValue("@codearticle", entity.codeArticle);

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

        //retourne la quantité vendu d'un article pour une année
        public int getQuantiteVendu(string codeArticle, int annee)
        {
            int quantiteVendu = 0;
            string codearticle = "";

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT codearticle, sum(quantite) FROM Composer c, acheter a WHERE c.codearticle = '" + codeArticle + "' AND c.codesetarticle = a.codesetarticle AND a.annee = c.annee AND a.annee = '" + annee + "' GROUP BY(c.codearticle)";
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        codearticle = Convert.ToString(dataReader["codearticle"]);
                        quantiteVendu = Convert.ToInt16(dataReader["sum(quantite)"]);
                    }
                    dataReader.Close();

                    return quantiteVendu;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
