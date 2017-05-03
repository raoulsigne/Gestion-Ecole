using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    public class ProgrammerDA : DA<ProgrammerBE>
    {
        private Connexion con =  Connexion.getConnexion();

        //----------------Ajout d'une nouvelle programmation ------------------------------
        public override Boolean ajouter(ProgrammerBE P)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {
               
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO programmer (CODECLASSE,CODEMAT,CODEPROF, COEF, ANNEE, CODEGROUPE) VALUES (@codeC, @codeM, @codeP, @coef, @annee,@groupe)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeC", P.codeclasse);
                cmd.Parameters.AddWithValue("@codeM", P.codematiere);
                cmd.Parameters.AddWithValue("@codeP", P.codeprof);
                cmd.Parameters.AddWithValue("@coef", P.coef);
                cmd.Parameters.AddWithValue("@annee", P.annee);
                cmd.Parameters.AddWithValue("@groupe", P.codegroupe);

                cmd.Transaction = transaction;

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();

                transaction.Commit();

                return true ;
                // Fermeture de la connexion
              //  this.con.fermer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                transaction.Rollback();

                return false ;
            }
        }

        public Boolean modifier(ProgrammerBE P)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                if(this.rechercher(P) != null)
                    cmd.CommandText = "UPDATE programmer SET CODEPROF=@codeP, COEF=@coef, CODEGROUPE=@groupe WHERE CODECLASSE=@codeC AND CODEMAT=@codeM AND ANNEE=@annee";
                else
                    cmd.CommandText = "REPLACE INTO programmer (CODECLASSE,CODEMAT,CODEPROF, COEF, ANNEE, CODEGROUPE) VALUES (@codeC, @codeM, @codeP, @coef, @annee,@groupe)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeC", P.codeclasse);
                cmd.Parameters.AddWithValue("@codeM", P.codematiere);
                cmd.Parameters.AddWithValue("@codeP", P.codeprof);
                cmd.Parameters.AddWithValue("@coef", P.coef);
                cmd.Parameters.AddWithValue("@annee", P.annee);
                cmd.Parameters.AddWithValue("@groupe", P.codegroupe);

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

        //--------------------------Suppression d'une Proggrammation ------

        public override Boolean supprimer(ProgrammerBE P)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM programmer WHERE CODECLASSE=@codeC and CODEMAT=@codeM and CODEPROF=@codeP and ANNEE=@annee";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeC", P.codeclasse);
                cmd.Parameters.AddWithValue("@codeM", P.codematiere);
                cmd.Parameters.AddWithValue("@codeP", P.codeprof);
                cmd.Parameters.AddWithValue("@annee", P.annee);

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

        //--------------------------Modification d'une programmation -----------

        // mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(ProgrammerBE P,ProgrammerBE newP)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE programmer SET CODECLASSE=@codeC1, CODEMAT=@codeM1, CODEPROF=@codeP1, ANNEE=@annee1, COEF=@coef, CODEGROUPE=@groupe WHERE CODECLASSE=@codeC and CODEMAT=@codeM and CODEPROF=@codeP and ANNEE=@annee";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeC1", newP.codeclasse);
                cmd.Parameters.AddWithValue("@codeM1", newP.codematiere);
                cmd.Parameters.AddWithValue("@annee1", newP.annee);
                cmd.Parameters.AddWithValue("@codeP1", newP.codeprof);

                cmd.Parameters.AddWithValue("@codeC", P.codeclasse);
                cmd.Parameters.AddWithValue("@codeM", P.codematiere);
                cmd.Parameters.AddWithValue("@annee", P.annee);
                
                cmd.Parameters.AddWithValue("@codeP", P.codeprof);
                cmd.Parameters.AddWithValue("@coef", newP.coef);
                cmd.Parameters.AddWithValue("@groupe", newP.codegroupe);

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

        public override ProgrammerBE rechercher(ProgrammerBE programmer)
        {
            int coef;
            ProgrammerBE P = null;
            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM programmer WHERE CODECLASSE=@codeC and CODEMAT=@codeM and CODEPROF=@codeP and annee=@annee";
                cmd.Parameters.AddWithValue("@codeC", programmer.codeclasse);
                cmd.Parameters.AddWithValue("@codeM", programmer.codematiere);
                cmd.Parameters.AddWithValue("@codeP", programmer.codeprof);                
                cmd.Parameters.AddWithValue("@annee", programmer.annee);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        coef = Convert.ToInt32(dataReader["COEF"]);
                        P = new ProgrammerBE(programmer.codeclasse, programmer.codematiere, Convert.ToString(dataReader["codeprof"]), coef, programmer.annee, Convert.ToString(dataReader["codegroupe"]));
                        dataReader.Close();
                        // this.con.fermer();
                    }
                    return P;
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
        public override List<ProgrammerBE> listerTous()
        {
            List<ProgrammerBE> list = new List<ProgrammerBE>();
            int coef;
            int annee;
            ProgrammerBE P;
            string codeclasse;
            string matricule;
            string codeprof;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Programmer";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        codeclasse = Convert.ToString(dataReader["codeclasse"]);
                        matricule = Convert.ToString(dataReader["codemat"]);
                        codeprof = Convert.ToString(dataReader["codeprof"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        P = new ProgrammerBE(codeclasse, matricule, codeprof, coef, annee, Convert.ToString(dataReader["codegroupe"]));
                        list.Add(P);
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

        public override List<ProgrammerBE> listerSuivantCritere(string critere)
        {
            List<ProgrammerBE> list = new List<ProgrammerBE>();
            int coef;
            int annee;
            ProgrammerBE P;
            string codeclasse;
            string matricule;
            string codeprof;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Programmer WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        codeclasse = Convert.ToString(dataReader["codeclasse"]);
                        matricule = Convert.ToString(dataReader["codemat"]);
                        codeprof = Convert.ToString(dataReader["codeprof"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        P = new ProgrammerBE(codeclasse, matricule, codeprof, coef, annee, Convert.ToString(dataReader["codegroupe"]));
                        list.Add(P);
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
                cmd.CommandText = "SELECT * FROM Programmer";

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
                cmd.CommandText = "SELECT COUNT(*) FROM Programmer";

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

        public List<string> listeCodeMatiereDuneClasse(string codeclasse, int annee)
        {
            List<String> list = new List<String>();

            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT codemat FROM programmer "
                                  +" WHERE codeclasse = "+"'"+ codeclasse +"' AND annee = "+"'"+ annee +"'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(Convert.ToString(dataReader["codemat"]));
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

        public List<string> listeCodeGroupeDuneClasse(string codeclasse, int annee)
        {
            List<String> list = new List<String>();

            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT codegroupe FROM programmer "
                                  + " WHERE codeclasse = " + "'" + codeclasse + "' AND annee = " + "'" + annee + "' GROUP BY codegroupe";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(Convert.ToString(dataReader["codegroupe"]));
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
    }
}

