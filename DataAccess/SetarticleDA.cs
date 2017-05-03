using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    public class SetarticleDA : DA<SetarticleBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'un nouveau Setarticle ------------------------------
        public override Boolean ajouter(SetarticleBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO setarticle (CODESETARTICLE, ANNEE, NOMSETARTICLE, MONTANT) VALUES "
                    + " (@codeS, @annee, @nomS, @montant)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", S.codesetarticle);
                cmd.Parameters.AddWithValue("@annee", S.annee);
                cmd.Parameters.AddWithValue("@nomS", S.nomsetarticle);
                cmd.Parameters.AddWithValue("@montant", S.montant);

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

        //--------------------------Suppression d'un Setarticle ------

        public override Boolean supprimer(SetarticleBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM setarticle WHERE CODESETARTICLE=@codeS";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", S.codesetarticle);

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

        //--------------------------Modification d'un Setarticle -----------

        // mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(SetarticleBE S, SetarticleBE newS)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE setarticle SET codesetarticle=@nouveaucodeSet, annee=@annee, nomsetarticle=@nomsetarticle, montant=@montant WHERE codesetarticle=@anciencodeSet";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@annee", newS.annee);
                cmd.Parameters.AddWithValue("@nomsetarticle", newS.nomsetarticle);
                cmd.Parameters.AddWithValue("@montant", newS.montant);
                cmd.Parameters.AddWithValue("@nouveaucodeSet", newS.codesetarticle);
                cmd.Parameters.AddWithValue("@anciencodeSet", S.codesetarticle);

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

        //---------------------------Fin Modification --------------------------

        //---------------Rechercher des informations sur un Setarticle spécifique---------------------------------

        public override SetarticleBE rechercher(SetarticleBE setarticle)
        {
            string NOMSETARTICLE;
            int annee;
            decimal montant;

            SetarticleBE S;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM setarticle WHERE CODESETARTICLE=@codeS";
                cmd.Parameters.AddWithValue("@codeS", setarticle.codesetarticle);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {

                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        NOMSETARTICLE = Convert.ToString(dataReader["NOMSETARTICLE"]);
                        annee = Convert.ToInt32(dataReader["ANNEE"]);
                        montant = Convert.ToDecimal(dataReader["MONTANT"]);
                        S = new SetarticleBE(setarticle.codesetarticle, annee, NOMSETARTICLE, montant);
                        dataReader.Close();
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
        public override List<SetarticleBE> listerTous()
        {
            List<SetarticleBE> list = new List<SetarticleBE>();
            string nomsetarticle;
            int annee;
            String code;
            decimal montant;
            SetarticleBE s;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM setarticle";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codesetarticle"]);
                        nomsetarticle = Convert.ToString(dataReader["nomsetarticle"]);
                        montant = Convert.ToDecimal(dataReader["montant"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        s = new SetarticleBE(code, annee, nomsetarticle, montant);
                        list.Add(s);
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

        //liste les éléments respectant un critère passé en paramètre
        public override List<SetarticleBE> listerSuivantCritere(string critere)
        {
            List<SetarticleBE> list = new List<SetarticleBE>();
            string nomsetarticle;
            int annee;
            String code;
            decimal montant;
            SetarticleBE s;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM setarticle WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codesetarticle"]);
                        nomsetarticle = Convert.ToString(dataReader["nomsetarticle"]);
                        montant = Convert.ToDecimal(dataReader["montant"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        s = new SetarticleBE(code, annee, nomsetarticle, montant);
                        list.Add(s);
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
                cmd.CommandText = "SELECT * FROM setarticle";

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
                cmd.CommandText = "SELECT COUNT(*) FROM setarticle";

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

        //modifier un enregistrement
        public Boolean modifier(SetarticleBE entity)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE setarticle SET annee=@annee, nomsetarticle=@nomsetarticle, montant=@montant WHERE codesetarticle=@codeSet";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@annee", entity.annee);
                cmd.Parameters.AddWithValue("@nomsetarticle", entity.nomsetarticle);
                cmd.Parameters.AddWithValue("@montant", entity.montant);
                cmd.Parameters.AddWithValue("@codeSet", entity.codesetarticle);

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

    }
}
