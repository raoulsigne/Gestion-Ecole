using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class MatiereDA : DA<MatiereBE>
    {
        private Connexion con = Connexion.getConnexion();

        // ************************** création d'objet, parametre obj, retourne booléen
        public override Boolean ajouter(MatiereBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO matiere (codemat,nommat,namemat,annee) VALUES (@codeMatiere, @nomMatiere, @nameMatiere, @annee)";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeMatiere", obj.codeMat);
                cmd.Parameters.AddWithValue("@nomMatiere", obj.nomMat);
                cmd.Parameters.AddWithValue("@nameMatiere", obj.nameMat);
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
        // ************************** FIN création d'objet, parametre obj, retourne booléen
       
        //******************************* suppression d'objet, parametre obj, retourne booléen
        public override Boolean supprimer(MatiereBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM matiere WHERE codemat=@codeMatiere";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeMatiere", obj.codeMat);

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
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //******************************* FIN suppression d'objet, parametre obj, retourne booléen
        
        //***************************** mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(MatiereBE obj, MatiereBE newobj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE matiere SET codemat=@code, nommat=@nomMatiere, namemat=@nameMatiere, annee=@annee WHERE codemat=@codeMatiere";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@nomMatiere", newobj.nomMat);
                cmd.Parameters.AddWithValue("@nameMatiere", newobj.nameMat);
                cmd.Parameters.AddWithValue("@annee", newobj.annee);
                cmd.Parameters.AddWithValue("@code", newobj.codeMat);
                cmd.Parameters.AddWithValue("@codeMatiere", obj.codeMat);

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
        //***************************** FIN mise à jour d'objet, parametre obj, retourne booléen
        
        //***************************** rechercher d'un objet à partir de son code, parametre obj, retourne l'objet
        // Paramètre : code : le code de la matière
        public override MatiereBE rechercher(MatiereBE matiere) {
            string codeMatiere;
            string nomMatiere;
            string nameMatiere;
            int annee;
            MatiereBE mat;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM matiere WHERE codemat=@codeMatiere";
                cmd.Parameters.AddWithValue("@codeMatiere", matiere.codeMat);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        codeMatiere = Convert.ToString(dataReader["codemat"]);
                        nomMatiere = Convert.ToString(dataReader["nommat"]);
                        nameMatiere = Convert.ToString(dataReader["namemat"]);
                        annee = Convert.ToInt16(dataReader["annee"]);
                        mat = new MatiereBE(codeMatiere, nomMatiere, nameMatiere, annee);
                        dataReader.Close();
                        // this.con.fermer();
                        return mat;
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
        //***************************** FIN rechercher d'un objet à partir de son code, parametre obj, retourne l'objet

        //********************************** retourner la liste de tout les objets
        public override List<MatiereBE> listerTous() {
            try
            {
                List<MatiereBE> listMatBE = new List<MatiereBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM matiere";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        MatiereBE matBE = new MatiereBE(Convert.ToString(dataReader["codemat"]), Convert.ToString(dataReader["nommat"]), Convert.ToString(dataReader["namemat"]), Convert.ToInt16(dataReader["annee"]));
                        listMatBE.Add(matBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listMatBE.Count != 0)
                        return listMatBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //********************************** FIN retourner la liste de tous les objets
      
        //*********************************** retourner la liste des objets qui correspondent à un certain critère
        public override List<MatiereBE> listerSuivantCritere(String critere)
        {
            try
            {
                List<MatiereBE> listobjBE = new List<MatiereBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM matiere WHERE " + critere;

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        MatiereBE objBE = new MatiereBE(Convert.ToString(dataReader["codemat"]), Convert.ToString(dataReader["nommat"]), Convert.ToString(dataReader["namemat"]), Convert.ToInt16(dataReader["annee"]));                        
                        listobjBE.Add(objBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
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
        //*********************************** retourner la liste des objets qui correspondent à un certain critère

        public override List<string> listerValeursColonne(string colonne)
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM matiere";

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
                cmd.CommandText = "SELECT COUNT(*) FROM matiere";

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

        public Boolean modifier(MatiereBE obj)
        {
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE matiere SET nommat=@nomMatiere, namemat=@nameMatiere, annee=@annee WHERE codemat=@codeMatiere";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@nomMatiere", obj.nomMat);
                cmd.Parameters.AddWithValue("@nameMatiere", obj.nameMat);
                cmd.Parameters.AddWithValue("@annee", obj.annee);
                cmd.Parameters.AddWithValue("@codeMatiere", obj.codeMat);

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
                return true;
                // Fermeture de la connexion
                //  this.con.fermer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
