using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    public class AcheterDA : DA<AcheterBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------ajouter Acheter -----------------//
        public override Boolean ajouter(AcheterBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try{
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                //MySqlCommand cmd = new MySqlCommand(Sql, con.connexion, tx);
                cmd.CommandText = "INSERT INTO acheter (codesetarticle,matricule,login,annee,dateachat,montant,quantite) VALUES (@setarticle, @matricule, @login,@annee,@dateachat,@montant,@quantite)";
                cmd.Parameters.AddWithValue("@setarticle", entity.codesetarticle);
                cmd.Parameters.AddWithValue("@matricule", entity.matricule);
                cmd.Parameters.AddWithValue("@login", entity.login);
                cmd.Parameters.AddWithValue("@annee", entity.annee);
                cmd.Parameters.AddWithValue("@dateachat", entity.datAchat);
                cmd.Parameters.AddWithValue("@montant", entity.montant);
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
        //----------------fin ajouter -----------------//

        //----------------debut supprimer -----------------//
        public override Boolean supprimer(AcheterBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Acheter WHERE codesetarticle = @code and matricule=@matricule and annee=@annee";
                cmd.Parameters.AddWithValue("@code", entity.codesetarticle);
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
        public override AcheterBE rechercher(AcheterBE entity)
        {
            String code;
            String matricule;
            String login;
            int annee;
            DateTime date;
            AcheterBE a;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM acheter WHERE codesetarticle=@code";
                cmd.Parameters.AddWithValue("@code", entity.codesetarticle);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codesetarticle"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        login = Convert.ToString(dataReader["login"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        date = Convert.ToDateTime(dataReader["dateachat"]);
                        a = new AcheterBE(code,matricule,login,annee,date,Convert.ToDecimal(dataReader["montant"]),Convert.ToInt32(dataReader["quantite"]));
                        dataReader.Close();
                        // this.con.fermer();
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

        public int rechercherNumero(AcheterBE entity)
        {
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM acheter WHERE codesetarticle=@code and matricule=@matricule and login=@login and annee=@annee and dateachat=@dateachat and montant=@montant and quantite=@quantite";
                cmd.Parameters.AddWithValue("@code", entity.codesetarticle);
                cmd.Parameters.AddWithValue("@matricule", entity.matricule);
                cmd.Parameters.AddWithValue("@login", entity.login);
                cmd.Parameters.AddWithValue("@annee", entity.annee);
                cmd.Parameters.AddWithValue("@dateachat", entity.datAchat);
                cmd.Parameters.AddWithValue("@montant", entity.montant);
                cmd.Parameters.AddWithValue("@quantite", entity.quantite);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        return Convert.ToInt32(dataReader["numero"]);
                    }

                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        //----------------Fin chercher -----------------//

        //----------------debut modifier ---------------//
        public override Boolean modifier(AcheterBE entity, AcheterBE newEntity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE acheter SET annee=@annee, dateachat=@dateachat, montant=@montant, login=@login, quantite=@quantite, matricule=@matricule, codesetarticle=@codesetarticle WHERE matricule=@mat and codesetarticle=@code, annee=@an";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@matricule", newEntity.matricule);
                cmd.Parameters.AddWithValue("@annee", newEntity.annee);
                cmd.Parameters.AddWithValue("@dateachat", newEntity.datAchat);
                cmd.Parameters.AddWithValue("@login", newEntity.login);
                cmd.Parameters.AddWithValue("@codesetarticle", newEntity.codesetarticle);
                cmd.Parameters.AddWithValue("@montant", newEntity.montant);
                cmd.Parameters.AddWithValue("@quantite", newEntity.quantite);

                cmd.Parameters.AddWithValue("@mat", entity.matricule);
                cmd.Parameters.AddWithValue("@an", entity.annee);
                cmd.Parameters.AddWithValue("@code", entity.codesetarticle);
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
        public override List<AcheterBE> listerTous()
        {
            List<AcheterBE> list = new List<AcheterBE>();
            String code;
            String matricule;
            String login;
            int annee;
            DateTime date;
            AcheterBE a;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Acheter";
                
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codesetarticle"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        login = Convert.ToString(dataReader["login"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        date = Convert.ToDateTime(dataReader["dateachat"]);
                        a = new AcheterBE(code, matricule, login, annee, date, Convert.ToDecimal(dataReader["montant"]), Convert.ToInt32(dataReader["quantite"]));
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


        public override List<AcheterBE> listerSuivantCritere(string critere)
        {
            List<AcheterBE> list = new List<AcheterBE>();
            String code;
            String matricule;
            String login;
            int annee;
            DateTime date;
            AcheterBE a;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Acheter WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codesetarticle"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        login = Convert.ToString(dataReader["login"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        date = Convert.ToDateTime(dataReader["dateachat"]);
                        a = new AcheterBE(code, matricule, login, annee, date, Convert.ToDecimal(dataReader["montant"]), Convert.ToInt32(dataReader["quantite"]));
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

        //-----------debut compter -----------------
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
        //-----------fin compter -----------------

        //recherche si un article a deja été vendu
        public Boolean ADejaEteVendu(String codeArticle)
        {
            List<AcheterBE> list = new List<AcheterBE>();
            String code;
            String matricule;
            String login;
            int annee;
            DateTime date;
            AcheterBE a;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Acheter ach, SetArticle setA, Composer comp, Article art, Stocker stoc WHERE ach.codesetarticle = setA.codesetarticle AND setA.codesetarticle = comp.codesetarticle AND comp.codearticle = art.codearticle AND art.codearticle = stoc.codearticle AND stoc.codearticle = '" + codeArticle + "'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codesetarticle"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        login = Convert.ToString(dataReader["login"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        date = Convert.ToDateTime(dataReader["dateachat"]);
                        a = new AcheterBE(code, matricule, login, annee, date, Convert.ToDecimal(dataReader["montant"]),Convert.ToInt32(dataReader["quantite"]));
                        list.Add(a);
                    }
                    dataReader.Close();

                    if (list.Count != 0)
                        return true; //l'article a deja été vendu
                    else return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
