using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class InscrireDA : DA<InscrireBE>
    {
        private Connexion con = Connexion.getConnexion();

        // ******************************* création d'objet, parametre obj, retourne booléen
        public override Boolean ajouter(InscrireBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO inscrire (codeclasse,matricule,annee) VALUES (@codeClasse, @matricule, @annee)";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeClasse", obj.codeClasse);
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@annee", obj.annee);

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
        // ******************************* Fin création d'objet, parametre obj, retourne booléen

        // ************************* suppression d'objet, parametre obj, retourne booléen
        public override Boolean supprimer(InscrireBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM inscrire WHERE codeclasse=@codeclasse AND matricule=@matricule and annee=@annee";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeclasse", obj.codeClasse);
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@annee", obj.annee);

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
        // ************************* FIN suppression d'objet, parametre obj, retourne booléen

        // ************************** mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(InscrireBE obj, InscrireBE newobj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE inscrire SET annee=@annee, codeclasse=@codeclasse, matricule=@matricule WHERE codeclasse=@code AND matricule=@mat and annee=@an";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeclasse", newobj.codeClasse);
                cmd.Parameters.AddWithValue("@matricule", newobj.matricule);
                cmd.Parameters.AddWithValue("@annee", newobj.annee);
                cmd.Parameters.AddWithValue("@code", obj.codeClasse);
                cmd.Parameters.AddWithValue("@mat", obj.matricule);
                cmd.Parameters.AddWithValue("@an", obj.annee);

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

        public Boolean modifier(InscrireBE obj)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE inscrire SET codeclasse=@codeclasse WHERE matricule=@matricule and annee=@anne";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeclasse", obj.codeClasse);
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@anne", obj.annee);

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

        public override InscrireBE rechercher(InscrireBE inscrire) {
            string codeClasse;
            string matricule;
            int annee;
            InscrireBE inscr;
            try
            {
                
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM inscrire WHERE annee=@annee AND matricule=@matricule and codeclasse=@codeclasse";
                cmd.Parameters.AddWithValue("@codeclasse", inscrire.codeClasse);
                cmd.Parameters.AddWithValue("@annee", inscrire.annee);
                cmd.Parameters.AddWithValue("@matricule", inscrire.matricule);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        codeClasse = Convert.ToString(dataReader["codeclasse"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        annee = Convert.ToInt16(dataReader["annee"]);
                        inscr = new InscrireBE(codeClasse, matricule, annee);
                        dataReader.Close();
                        return inscr;
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public InscrireBE rechercherClasse(InscrireBE inscrire)
        {
            string codeClasse;
            string matricule;
            int annee;
            InscrireBE inscr;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM inscrire WHERE annee=@annee AND matricule=@matricule";
                cmd.Parameters.AddWithValue("@annee", inscrire.annee);
                cmd.Parameters.AddWithValue("@matricule", inscrire.matricule);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        codeClasse = Convert.ToString(dataReader["codeclasse"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        annee = Convert.ToInt16(dataReader["annee"]);
                        inscr = new InscrireBE(codeClasse, matricule, annee);
                        dataReader.Close();
                        return inscr;
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //************************* FIN rechercher d'un objet à partir de son code, parametre obj, retourne l'objet

 
        // ****************************** retourner la liste de tout les objets
        public override List<InscrireBE> listerTous() {
            try
            {
                List<InscrireBE> listInscBE = new List<InscrireBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM inscrire";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        InscrireBE inscBE = new InscrireBE(Convert.ToString(dataReader["codeclasse"]), Convert.ToString(dataReader["matricule"]), Convert.ToInt16(dataReader["annee"]));
                        listInscBE.Add(inscBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listInscBE.Count != 0)
                        return listInscBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        // ****************************** FIN retourner la liste de tout les objets


        public override List<InscrireBE> listerSuivantCritere(string critere)
        {
            try
            {
                List<InscrireBE> listobjBE = new List<InscrireBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM inscrire WHERE " + critere;

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        InscrireBE objBE = new InscrireBE(Convert.ToString(dataReader["codeclasse"]), Convert.ToString(dataReader["matricule"]), Convert.ToInt16(dataReader["annee"]));
                        listobjBE.Add(objBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listobjBE.Count != 0)
                        return listobjBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // ************************ retourner la liste des objets qui correspondent à un certain critère
        public override List<String> listerValeursColonne(String colonne)
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Inscrire";

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
        // ************************ FIN retourner la liste des objets qui correspondent à un certain critère

        //-----------debut compter -----------------
        public int compter()
        {
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM inscrire";

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

        internal int maxAnnee(string p)
        {
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT MAX(annee) FROM inscrire WHERE matricule = " + "'" + p + "'";
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DateTime.Today.Year;
            }
        }
    }
}
