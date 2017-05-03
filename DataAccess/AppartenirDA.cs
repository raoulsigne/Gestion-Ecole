using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    public class AppartenirDA : DA<AppartenirBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------ajouter Appartenir -----------------//
        public override Boolean ajouter(AppartenirBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO appartenir (codecateleve,matricule,annee) VALUES (@cateleve, @matricule,@annee)";
                cmd.Parameters.AddWithValue("@cateleve", entity.codeCatEleve);
                cmd.Parameters.AddWithValue("@matricule", entity.matricule);
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
        //----------------fin ajouter -----------------//

        //----------------debut supprimer -----------------//
        public override Boolean supprimer(AppartenirBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Appartenir WHERE codecateleve = @code AND matricule=@matricule and annee=@annee";
                cmd.Parameters.AddWithValue("@code", entity.codeCatEleve);
                cmd.Parameters.AddWithValue("@matricule", entity.matricule);
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
        //----------------fin supprimer -----------------//

        //----------------chercher Acheter -----------------//
        public override AppartenirBE rechercher(AppartenirBE entity)
        {
            String code;
            String matricule;
            int annee;
            AppartenirBE a = new AppartenirBE();

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM appartenir WHERE codecateleve=@codecateleve and annee=@annee AND matricule=@matricule";
                cmd.Parameters.AddWithValue("@annee", entity.annee);
                cmd.Parameters.AddWithValue("@matricule", entity.matricule);
                cmd.Parameters.AddWithValue("@codecateleve", entity.categorieeleve);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codecateleve"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        a = new AppartenirBE(code, matricule, annee);
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
        //----------------Fin chercher -----------------//

        //----------------chercher Acheter -----------------//
        public AppartenirBE rechercherCategorie(AppartenirBE entity)
        {
            String code;
            String matricule;
            int annee;
            AppartenirBE a = new AppartenirBE();

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM appartenir WHERE annee=@annee AND matricule=@matricule";
                cmd.Parameters.AddWithValue("@annee", entity.annee);
                cmd.Parameters.AddWithValue("@matricule", entity.matricule);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codecateleve"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        a = new AppartenirBE(code, matricule, annee);
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
        //----------------Fin chercher -----------------//

        //----------------debut modifier ---------------//
        public override Boolean modifier(AppartenirBE entity, AppartenirBE newEntity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE appartenir SET annee=@annee, codecateleve=@codecateleve, matricule=@mat WHERE matricule=@mat and codecateleve=@code and annee=@an";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@matricule", newEntity.matricule);
                cmd.Parameters.AddWithValue("@annee", newEntity.annee);
                cmd.Parameters.AddWithValue("@codecateleve", newEntity.codeCatEleve);

                cmd.Parameters.AddWithValue("@mat", entity.matricule);
                cmd.Parameters.AddWithValue("@an", entity.annee);
                cmd.Parameters.AddWithValue("@code", entity.codeCatEleve);

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
        //----------------fin modifier ---------------//


        //----------------debut lister --------------------------------------------
        public override List<AppartenirBE> listerTous()
        {
            List<AppartenirBE> list = new List<AppartenirBE>();
            String code;
            String matricule;
            int annee;
            AppartenirBE a;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Appartenir";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet Appartenir à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codecateleve"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        a = new AppartenirBE(code, matricule, annee);
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


        public override List<AppartenirBE> listerSuivantCritere(string critere)
        {
            List<AppartenirBE> list = new List<AppartenirBE>();
            String code;
            String matricule;
            int annee;
            AppartenirBE a;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM appartenir WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codecateleve"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        a = new AppartenirBE(code, matricule, annee);
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
                cmd.CommandText = "SELECT * FROM Acheter ";

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
                cmd.CommandText = "SELECT COUNT(*) FROM Acheter";

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
        //-----------fin compter -----------------------------------------------
    }
}
