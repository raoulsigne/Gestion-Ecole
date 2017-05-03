using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class DirigerDA : DA<DirigerBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------ajouter -----------------//
        public override Boolean ajouter(DirigerBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO Diriger (codeclasse, codeprof,annee) VALUES (@classe, @prof, @annee)";
                cmd.Parameters.AddWithValue("@classe", entity.codeClasse);
                cmd.Parameters.AddWithValue("@prof", entity.codeProf);
                cmd.Parameters.AddWithValue("@annee", entity.annee);

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
        public override Boolean supprimer(DirigerBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Diriger WHERE codeclasse = @classe AND codeprof = @prof and annee=@annee";
                cmd.Parameters.AddWithValue("@classe", entity.codeClasse);
                cmd.Parameters.AddWithValue("@prof", entity.codeProf);
                cmd.Parameters.AddWithValue("@annee", entity.codeProf);

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
        public override DirigerBE rechercher(DirigerBE entity)
        {
            String codeClasse;
            String codeProf;
            int annee;
            DirigerBE d;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM Diriger WHERE codeclasse=@classe AND codeprof=@prof";
                cmd.Parameters.AddWithValue("@classe", entity.codeClasse);
                cmd.Parameters.AddWithValue("@prof", entity.codeProf);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        codeClasse = Convert.ToString(dataReader["codeclasse"]);
                        codeProf = Convert.ToString(dataReader["codeprof"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        d = new DirigerBE(codeClasse, codeProf, annee);
                        dataReader.Close();
                        return d;
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
        public override Boolean modifier(DirigerBE entity, DirigerBE newEntity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE diriger SET codeprof=@codeprof, codeclasse=@codeclasse, annee=@annee WHERE codeprof=@codep and codeclasse=@codec and annee=@an";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeprof", newEntity.codeProf);
                cmd.Parameters.AddWithValue("@codeclasse", newEntity.codeClasse);
                cmd.Parameters.AddWithValue("@annee", newEntity.annee);

                cmd.Parameters.AddWithValue("@codep", entity.codeProf);
                cmd.Parameters.AddWithValue("@codec", entity.codeClasse);
                cmd.Parameters.AddWithValue("@an", entity.annee);
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
        public override List<DirigerBE> listerTous()
        {
            List<DirigerBE> list = new List<DirigerBE>();
            String codeClasse;
            String codeProf;
            int annee;
            DirigerBE d;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Diriger";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeClasse = Convert.ToString(dataReader["codeclasse"]);
                        codeProf = Convert.ToString(dataReader["codeprof"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        d = new DirigerBE(codeClasse, codeProf, annee);
                        list.Add(d);
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

        public override List<DirigerBE> listerSuivantCritere(string critere)
        {
            List<DirigerBE> list = new List<DirigerBE>();
            String codeClasse;
            String codeProf;
            int annee;
            DirigerBE d;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Diriger WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeClasse = Convert.ToString(dataReader["codeclasse"]);
                        codeProf = Convert.ToString(dataReader["codeprof"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        d = new DirigerBE(codeClasse, codeProf, annee);
                        list.Add(d);
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
                cmd.CommandText = "SELECT * FROM Diriger ";

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

        //-----------debut compter ----------------------------------------------
        public int compter()
        {
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Diriger";

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
        //-----------fin compter ----------------------------------------------
    }
}
