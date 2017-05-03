using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;

using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    public class RealiserDA : DA<RealiserBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'une nouvelle realisation ------------------------------
        public override Boolean ajouter(RealiserBE R)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO realiser (CODEOP, LOGIN, NUMEROOP, MOTIF, MONTANT, DATEOP,CONCERNE) VALUES (@codeO, @login, @numO, @motif, @montant, @dateO, @concerne)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeO", R.codeop);
                cmd.Parameters.AddWithValue("@login", R.login);
                cmd.Parameters.AddWithValue("@numO", R.numeroop);
                cmd.Parameters.AddWithValue("@motif", R.motif);
                cmd.Parameters.AddWithValue("@montant", R.montant);
                cmd.Parameters.AddWithValue("@dateO", R.dateop);
                cmd.Parameters.AddWithValue("@concerne", R.concerne);

                cmd.Transaction = transaction;

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();

                transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                transaction.Rollback();

                return false;
            }
        }

        //--------------------------Fin ajout-----------------------------

        //--------------------------Suppression d'une realisation ------

        public override Boolean supprimer(RealiserBE R)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM realiser WHERE CODEOP=@codeO AND LOGIN=@login AND NUMEROOP=@numero";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeO", R.codeop);
                cmd.Parameters.AddWithValue("@login", R.login);
                cmd.Parameters.AddWithValue("@numero",R.numeroop);

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

        //--------------------------Modification d'une realisation -----------

        // mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(RealiserBE R, RealiserBE newR)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE realiser SET CODEOP=@codeO1, LOGIN=@login1, NUMEROOP=@numO1, MOTIF=@motif, MONTANT=@montant, DATEOP=@dateO, concerne=@concerne WHERE CODEOP=@codeO and LOGIN=@login and  NUMEROOP=@numO";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeO1", newR.codeop);
                cmd.Parameters.AddWithValue("@login1", newR.login);
                cmd.Parameters.AddWithValue("@numO1", newR.numeroop);
                
                cmd.Parameters.AddWithValue("@numO", R.numeroop);
                cmd.Parameters.AddWithValue("@motif", newR.motif);
                cmd.Parameters.AddWithValue("@montant", newR.montant);
                cmd.Parameters.AddWithValue("@dateO", newR.dateop.Date);
                cmd.Parameters.AddWithValue("@codeO", R.codeop);
                cmd.Parameters.AddWithValue("@login", R.login);
                cmd.Parameters.AddWithValue("@concerne", newR.concerne);

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

        public Boolean modifier(RealiserBE R)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE realiser SET concerne=@concerne, MOTIF=@motif, MONTANT=@montant, DATEOP=@dateO WHERE CODEOP=@codeO AND LOGIN=@login AND NUMEROOP=@numO";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@numO", R.numeroop);
                cmd.Parameters.AddWithValue("@motif", R.motif);
                cmd.Parameters.AddWithValue("@montant", R.montant);
                cmd.Parameters.AddWithValue("@dateO", R.dateop.Date);
                cmd.Parameters.AddWithValue("@codeO", R.codeop);
                cmd.Parameters.AddWithValue("@login", R.login);
                cmd.Parameters.AddWithValue("@concerne", R.concerne);

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

        public RealiserBE rechercherParNumero(RealiserBE realiser)
        {
            string codeop;
            string login;
            string numeroop;
            string motif;
            decimal montant;
            DateTime dateop;
            string concerne;
            RealiserBE r;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM Realiser WHERE numeroop=@numeroop";
                cmd.Parameters.AddWithValue("@numeroop", realiser.numeroop);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        codeop = Convert.ToString(dataReader["codeop"]);
                        login = Convert.ToString(dataReader["login"]);
                        numeroop = Convert.ToString(dataReader["numeroop"]);
                        motif = Convert.ToString(dataReader["motif"]);
                        montant = Convert.ToDecimal(dataReader["montant"]);
                        dateop = Convert.ToDateTime(dataReader["dateop"]);
                        concerne = Convert.ToString(dataReader["concerne"]);

                        r = new RealiserBE(codeop, login, numeroop, motif, montant, dateop, concerne);
                        dataReader.Close();
                        return r;
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

        public override RealiserBE rechercher(RealiserBE realiser)
        {
            string codeop;
            string login;
            string numeroop;
            string motif;
            decimal montant;
            DateTime dateop;
            string concerne;
            RealiserBE r;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM Realiser WHERE codeop=@codeop AND login=@login AND numeroop=@numeroop";
                cmd.Parameters.AddWithValue("@numeroop", realiser.numeroop);
                cmd.Parameters.AddWithValue("@codeop", realiser.codeop);
                cmd.Parameters.AddWithValue("@login", realiser.login);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        codeop = Convert.ToString(dataReader["codeop"]);
                        login = Convert.ToString(dataReader["login"]);
                        numeroop = Convert.ToString(dataReader["numeroop"]);
                        motif = Convert.ToString(dataReader["motif"]);
                        montant = Convert.ToDecimal(dataReader["montant"]);
                        dateop = Convert.ToDateTime(dataReader["dateop"]);
                        concerne = Convert.ToString(dataReader["concerne"]);

                        r = new RealiserBE(codeop, login, numeroop, motif, montant, dateop, concerne);
                        dataReader.Close();
                        return r;
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
        public override List<RealiserBE> listerTous()
        {
            List<RealiserBE> list = new List<RealiserBE>();
            String codeop;
            String login;
            String numeroop;
            String motif;
            decimal montant;
            DateTime dateop;
            String concerne;
            RealiserBE r;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Realiser";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        codeop = Convert.ToString(dataReader["codeop"]);
                        login = Convert.ToString(dataReader["login"]);
                        numeroop = Convert.ToString(dataReader["numeroop"]);
                        motif = Convert.ToString(dataReader["motif"]);
                        montant = Convert.ToDecimal(dataReader["montant"]);
                        dateop = Convert.ToDateTime(dataReader["dateop"]);
                        concerne = Convert.ToString(dataReader["concerne"]);
                        r = new RealiserBE(codeop, login, numeroop, motif, montant,dateop,concerne);
                        list.Add(r);
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


        public override List<RealiserBE> listerSuivantCritere(string critere)
        {
            List<RealiserBE> list = new List<RealiserBE>();
            String codeop;
            String login;
            String numeroop;
            String motif;
            decimal montant;
            DateTime dateop;
            String concerne;
            RealiserBE r;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Realiser WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        codeop = Convert.ToString(dataReader["codeop"]);
                        login = Convert.ToString(dataReader["login"]);
                        numeroop = Convert.ToString(dataReader["numeroop"]);
                        motif = Convert.ToString(dataReader["motif"]);
                        montant = Convert.ToDecimal(dataReader["montant"]);
                        dateop = Convert.ToDateTime(dataReader["dateop"]);
                        concerne = Convert.ToString(dataReader["concerne"]);
                        r = new RealiserBE(codeop, login, numeroop, motif, montant, dateop,concerne);
                        list.Add(r);
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
                cmd.CommandText = "SELECT * FROM Realiser";

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

        public String[] rechercherMaxNumeroOP()
        {
            String[] numeroop = {"",""};
            
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT mid(max(numeroop),1,6), mid(max(numeroop),7,4) FROM Realiser";
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    if (dataReader.Read())
                    {
                        numeroop[0] = Convert.ToString(dataReader[0]);
                        numeroop[1] = Convert.ToString(dataReader[1]);
                    }
                    dataReader.Close();

                    return numeroop;
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
