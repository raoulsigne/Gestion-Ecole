using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    public class TypeclasseDA : DA<TypeclasseBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'un nouveau Type de classe ------------------------------
        public override Boolean ajouter(TypeclasseBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'un  commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO typeclasse (CODETYPECLASSE, NOMTYPECLASSE, FRAISINSCRIPTION) VALUES (@codeT, @nomT, @frais)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", S.codetypeclasse);
                cmd.Parameters.AddWithValue("@nomT", S.nomtypeclasse);
                cmd.Parameters.AddWithValue("@frais", S.fraisinscription);

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

        //--------------------------Suppression d'un  Type de classe ------

        public override Boolean supprimer(TypeclasseBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'un  commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM typeclasse WHERE CODETYPECLASSE=@codeT";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", S.codetypeclasse);

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

        //--------------------------Modification d'un  Type de classe -----------

        // mise à jour d'objet, parametre obj, retourne booléen
        public Boolean modifier(TypeclasseBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'un  commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE typeclasse SET NOMTYPECLASSE=@nomT, FRAISINSCRIPTION=@frais WHERE CODETYPECLASSE=@codeT";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", S.codetypeclasse);
                cmd.Parameters.AddWithValue("@nomT", S.nomtypeclasse);
                cmd.Parameters.AddWithValue("@frais", S.fraisinscription);

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

        //---------------Rechercher des informations sur un  Type de classe spécifique---------------------------------

        public override TypeclasseBE rechercher(TypeclasseBE typeClasse)
        {
            string NOMTYPECLASSE;
            decimal frais;

            TypeclasseBE S;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'un  commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM typeclasse WHERE CODETYPECLASSE=@codeT";
                cmd.Parameters.AddWithValue("@codeT", typeClasse.codetypeclasse);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        NOMTYPECLASSE = Convert.ToString(dataReader["NOMTYPECLASSE"]);
                        frais = Convert.ToDecimal(dataReader["FRAISINSCRIPTION"]);
                        S = new TypeclasseBE(typeClasse.codetypeclasse, NOMTYPECLASSE, frais);
                        dataReader.Close();
                        // this.con.fermer();
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

            //-----------------------------Fin de la recherche----------
        }


        // modifier un objet
        public override Boolean modifier(TypeclasseBE entity, TypeclasseBE newEntity)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'un  commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE typeclasse SET NOMTYPECLASSE=@nomT, FRAISINSCRIPTION=@frais, CODETYPECLASSE=@codeT1 WHERE CODETYPECLASSE=@codeT";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", entity.codetypeclasse);

                cmd.Parameters.AddWithValue("@codeT1", newEntity.codetypeclasse);
                cmd.Parameters.AddWithValue("@nomT", newEntity.nomtypeclasse);
                cmd.Parameters.AddWithValue("@frais", newEntity.fraisinscription);

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

        // retourner la liste de tout les objets
        public override List<TypeclasseBE> listerTous()
        {
            List<TypeclasseBE> list = new List<TypeclasseBE>();
            String nomTypeClasse;
            String codeTypeClasse;
            Int32 fraisInscription;
            TypeclasseBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM typeclasse";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeTypeClasse = Convert.ToString(dataReader["codetypeclasse"]);
                        nomTypeClasse = Convert.ToString(dataReader["nomtypeclasse"]);
                        fraisInscription = Convert.ToInt32(dataReader["fraisinscription"]);
                        c = new TypeclasseBE(codeTypeClasse, nomTypeClasse, fraisInscription);
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

        // retourner la liste des objets qui correspondent à un certain critère
        public override List<TypeclasseBE> listerSuivantCritere(string critere)
        {
            List<TypeclasseBE> list = new List<TypeclasseBE>();
            String nomTypeClasse;
            String codeTypeClasse;
            Int32 fraisInscription;
            TypeclasseBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM typeclasse WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeTypeClasse = Convert.ToString(dataReader["codetypeclasse"]);
                        nomTypeClasse = Convert.ToString(dataReader["nomtypeclasse"]);
                        fraisInscription = Convert.ToInt32(dataReader["fraisinscription"]);
                        c = new TypeclasseBE(codeTypeClasse, nomTypeClasse, fraisInscription);
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

        /*
         * lister tous les donnees d'une colonne de la table
         * @param colonne est le nom de la colonne à lister
         */
        public override List<String> listerValeursColonne(String colonne) 
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM typeclasse";

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
    }
}
