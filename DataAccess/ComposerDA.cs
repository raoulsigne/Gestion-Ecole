using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class ComposerDA : DA<ComposerBE> 
    {
        private Connexion con = Connexion.getConnexion();

        //----------------ajouter -----------------//
        public override Boolean ajouter(ComposerBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO Composer (codearticle, codesetarticle,annee, quantite) VALUES (@article, @setarticle, @annee, @quantite)";
                cmd.Parameters.AddWithValue("@article", entity.codeArticle);
                cmd.Parameters.AddWithValue("@setarticle", entity.codeSetArticle);
                cmd.Parameters.AddWithValue("@annee", entity.annee);
                cmd.Parameters.AddWithValue("@quantite", entity.quantite);

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
        //----------------fin ajouter -------------------------------//

        //----------------debut supprimer -----------------//
        public override Boolean supprimer(ComposerBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Composer WHERE codearticle = @code AND codesetarticle = @codesetarticle";
                cmd.Parameters.AddWithValue("@code", entity.codeArticle);
                cmd.Parameters.AddWithValue("@codesetarticle", entity.codeSetArticle);

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
        public override ComposerBE rechercher(ComposerBE entity)
        {
            String codeArticle;
            String codeSetArticle;
            int annee;
            int quantite;
            ComposerBE c;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM Composer WHERE codearticle=@article AND codesetarticle=@setarticle";
                cmd.Parameters.AddWithValue("@article", entity.codeArticle);
                cmd.Parameters.AddWithValue("@setarticle", entity.codeSetArticle);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        codeArticle = Convert.ToString(dataReader["codearticle"]);
                        codeSetArticle = Convert.ToString(dataReader["codesetarticle"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        quantite = Convert.ToInt32(dataReader["quantite"]);
                        c = new ComposerBE(codeArticle, codeSetArticle, annee, quantite);
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
        public override Boolean modifier(ComposerBE entity, ComposerBE newEntity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE composer SET quantite=@quantite, codearticle=@codearticle, codesetarticle=@codesetarticle, annee=@annee WHERE codearticle=@codea and codesetarticle=@codes and annee=@an";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@quantite", newEntity.quantite);
                cmd.Parameters.AddWithValue("@annee", newEntity.annee);
                cmd.Parameters.AddWithValue("@codearticle", newEntity.codeArticle);
                cmd.Parameters.AddWithValue("@codesetarticle", newEntity.codeSetArticle);

                cmd.Parameters.AddWithValue("@an", entity.annee);
                cmd.Parameters.AddWithValue("@codea", entity.codeArticle);
                cmd.Parameters.AddWithValue("@codes", entity.codeSetArticle);
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
        public override List<ComposerBE> listerTous()
        {
            List<ComposerBE> list = new List<ComposerBE>();
            String codeArticle;
            String codeSetArticle;
            int annee;
            int quantite;
            ComposerBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Composer";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeArticle = Convert.ToString(dataReader["codearticle"]);
                        codeSetArticle = Convert.ToString(dataReader["codesetarticle"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        quantite = Convert.ToInt32(dataReader["quantite"]);
                        c = new ComposerBE(codeArticle, codeSetArticle, annee, quantite);
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

        public override List<ComposerBE> listerSuivantCritere(string critere)
        {
            List<ComposerBE> list = new List<ComposerBE>();
            String codeArticle;
            String codeSetArticle;
            int annee;
            int quantite;
            ComposerBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Composer WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeArticle = Convert.ToString(dataReader["codearticle"]);
                        codeSetArticle = Convert.ToString(dataReader["codesetarticle"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        quantite = Convert.ToInt32(dataReader["quantite"]);
                        c = new ComposerBE(codeArticle, codeSetArticle, annee, quantite);
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
                cmd.CommandText = "SELECT * FROM Composer ";

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
                cmd.CommandText = "SELECT COUNT(*) FROM Composer";

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
    }
}
