using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class ParametresDA : DA<ParametresBE>
    {
        private Connexion con = Connexion.getConnexion();

        //**************************** création d'objet, parametre obj, retourne booléen
        public override Boolean ajouter(ParametresBE obj) {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO parametres "+
                    "(nomecole,adresse,tel,fax,email,siteweb,directeur,pays,region,ministere, ministery,country,regiona,annee, departement, department, ville, titreDuChef, titleOfChief, logo, REPERTOIRE_PHOTO)" +
                    " VALUES (@nomecole, @adresse, @tel, @fax, @email, @siteweb, @directeur, @pays, @region, @ministere, @ministery, @country, @regiona, @annee, @departement, @department, @ville, @titreDuchef,@titleOfChief, @logo, @REPERTOIRE_PHOTO)";

                // utilisation de l'objet ParametresBE passé en paramètre
                //cmd.Parameters.AddWithValue("@idparametre", obj.idParametre);
                cmd.Parameters.AddWithValue("@nomecole", obj.nomEcole);
                cmd.Parameters.AddWithValue("@adresse", obj.adresse);
                cmd.Parameters.AddWithValue("@tel", obj.tel);
                cmd.Parameters.AddWithValue("@fax", obj.fax);
                cmd.Parameters.AddWithValue("@email", obj.email);
                cmd.Parameters.AddWithValue("@siteweb", obj.siteWeb);
                cmd.Parameters.AddWithValue("@directeur", obj.directeur);
                cmd.Parameters.AddWithValue("@titleOfChief", obj.titleOfChief);
                cmd.Parameters.AddWithValue("@pays", obj.pays);
                cmd.Parameters.AddWithValue("@region", obj.region);
                cmd.Parameters.AddWithValue("@ministere", obj.ministere);
                cmd.Parameters.AddWithValue("@ministery", obj.ministery);
                cmd.Parameters.AddWithValue("@country", obj.country);
                cmd.Parameters.AddWithValue("@regiona", obj.regionA);
                cmd.Parameters.AddWithValue("@annee", obj.annee);
                cmd.Parameters.AddWithValue("@departement", obj.departement);
                cmd.Parameters.AddWithValue("@department", obj.department);
                cmd.Parameters.AddWithValue("@ville", obj.ville);
                cmd.Parameters.AddWithValue("@titreDuChef", obj.titreDuChef);
                cmd.Parameters.AddWithValue("@logo", obj.logo);
                cmd.Parameters.AddWithValue("@REPERTOIRE_PHOTO", obj.REPERTOIRE_PHOTO);
                //cmd.Parameters.AddWithValue("@FICHIER_LANGUE", obj.FICHIER_LANGUE);

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
        //**************************** FIN création d'objet, parametre obj, retourne booléen
        
        //**************************** suppression d'objet, parametre obj, retourne booléen
        public override Boolean supprimer(ParametresBE obj) {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM parametres WHERE idparametre=@idparametre";

                // utilisation de l'objet ParametresBE passé en paramètre
                cmd.Parameters.AddWithValue("@idparametre", obj.idParametre);

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
        //**************************** FIN suppression d'objet, parametre obj, retourne booléen
        
        //**************************** mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(ParametresBE obj, ParametresBE newobj) {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE parametres SET idparametre=@nouveauidparametre, nomecole=@nomecole,adresse=@adresse,tel=@tel,fax=@fax,email=@email, "+
                    "siteweb=@siteweb,directeur=@directeur,titleofchief=@titleofchief, pays=@pays,region=@region,ministere=@ministere, ministery=@ministery,country=@country, " +
                    "regiona=@regiona,annee=@annee, departement=@departement, department=@department, ville=@ville, titreDuChef=@titreDuChef, logo=@logo, REPERTOIRE_PHOTO=@REPERTOIRE_PHOTO WHERE idparametre=@ancienidparametre";

                // utilisation de l'objet ParametresBE passé en paramètre
                cmd.Parameters.AddWithValue("@nouveauidparametre", newobj.idParametre);
                cmd.Parameters.AddWithValue("@ancienidparametre", obj.idParametre);

                cmd.Parameters.AddWithValue("@nomecole", newobj.nomEcole);
                cmd.Parameters.AddWithValue("@adresse", newobj.adresse);
                cmd.Parameters.AddWithValue("@tel", newobj.tel);
                cmd.Parameters.AddWithValue("@fax", newobj.fax);
                cmd.Parameters.AddWithValue("@email", newobj.email);
                cmd.Parameters.AddWithValue("@siteweb", newobj.siteWeb);
                cmd.Parameters.AddWithValue("@directeur", newobj.directeur);
                cmd.Parameters.AddWithValue("@titleofchief", newobj.titleOfChief);
                cmd.Parameters.AddWithValue("@pays", newobj.pays);
                cmd.Parameters.AddWithValue("@region", newobj.region);
                cmd.Parameters.AddWithValue("@ministere", newobj.ministere);
                cmd.Parameters.AddWithValue("@ministery", newobj.ministery);
                cmd.Parameters.AddWithValue("@country", newobj.country);
                cmd.Parameters.AddWithValue("@regiona", newobj.regionA);
                cmd.Parameters.AddWithValue("@annee", newobj.annee);
                cmd.Parameters.AddWithValue("@departement", newobj.departement);
                cmd.Parameters.AddWithValue("@department", newobj.department);
                cmd.Parameters.AddWithValue("@ville", newobj.ville);
                cmd.Parameters.AddWithValue("@titreDuChef", newobj.titreDuChef);
                cmd.Parameters.AddWithValue("@logo", newobj.logo);
                cmd.Parameters.AddWithValue("@REPERTOIRE_PHOTO", newobj.REPERTOIRE_PHOTO);
                //cmd.Parameters.AddWithValue("@FICHIER_LANGUE", newobj.FICHIER_LANGUE);

                cmd.Transaction = transaction;
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

        public Boolean modifier(ParametresBE obj)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE parametres SET nomecole=@nomecole,adresse=@adresse,tel=@tel,fax=@fax,email=@email,siteweb=@siteweb,directeur=@directeur,titleofchief=@titleOfChief,pays=@pays, " +
                    " region=@region,ministere=@ministere,ministery=@ministery,country=@country,regiona=@regiona,annee=@annee, departement=@departement,department=@department, ville=@ville, titreDuChef=@titreDuChef, logo=@logo, REPERTOIRE_PHOTO=@REPERTOIRE_PHOTO WHERE idparametre=@idparametre";

                // utilisation de l'objet ParametresBE passé en paramètre
                cmd.Parameters.AddWithValue("@idparametre", obj.idParametre);
                cmd.Parameters.AddWithValue("@nomecole", obj.nomEcole);
                cmd.Parameters.AddWithValue("@adresse", obj.adresse);
                cmd.Parameters.AddWithValue("@tel", obj.tel);
                cmd.Parameters.AddWithValue("@fax", obj.fax);
                cmd.Parameters.AddWithValue("@email", obj.email);
                cmd.Parameters.AddWithValue("@siteweb", obj.siteWeb);
                cmd.Parameters.AddWithValue("@directeur", obj.directeur);
                cmd.Parameters.AddWithValue("@titleOfChief", obj.titleOfChief);
                cmd.Parameters.AddWithValue("@pays", obj.pays);
                cmd.Parameters.AddWithValue("@region", obj.region);
                cmd.Parameters.AddWithValue("@ministere", obj.ministere);
                cmd.Parameters.AddWithValue("@ministery", obj.ministery);
                cmd.Parameters.AddWithValue("@country", obj.country);
                cmd.Parameters.AddWithValue("@regiona", obj.regionA);
                cmd.Parameters.AddWithValue("@annee", obj.annee);
                cmd.Parameters.AddWithValue("@departement", obj.departement);
                cmd.Parameters.AddWithValue("@department", obj.department);
                cmd.Parameters.AddWithValue("@ville", obj.ville);
                cmd.Parameters.AddWithValue("@titreDuChef", obj.titreDuChef);
                cmd.Parameters.AddWithValue("@logo", obj.logo);
                cmd.Parameters.AddWithValue("@REPERTOIRE_PHOTO", obj.REPERTOIRE_PHOTO);
                //cmd.Parameters.AddWithValue("@FICHIER_LANGUE", obj.FICHIER_LANGUE);

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
        //**************************** FIN mise à jour d'objet, parametre obj, retourne booléen
        
        //************************ rechercher d'un objet à partir de l'id, parametre obj, retourne l'objet
        public override ParametresBE rechercher(ParametresBE parametre) {
            int idParametre;
            string nomEcole;
            string adresse;
            string tel;
            string fax;
            string email;
            string siteWeb;
            string directeur;
            string pays;
            string region;
            string ministere;
            string country;
            string regionA;
            int annee;
            String departement;
            String ville;
            String titreDuChef;

            String REPERTOIRE_PHOTO;
            //String FICHIER_LANGUE;
            ParametresBE m;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM parametres WHERE idparametre=@idparametre";
                cmd.Parameters.AddWithValue("@idparametre", parametre.idParametre);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        idParametre = Convert.ToInt16(dataReader["idparametre"]);
                        nomEcole = Convert.ToString(dataReader["nomecole"]);
                        adresse = Convert.ToString(dataReader["adresse"]);
                        tel = Convert.ToString(dataReader["tel"]);
                        fax = Convert.ToString(dataReader["fax"]);
                        email = Convert.ToString(dataReader["email"]);
                        siteWeb = Convert.ToString(dataReader["siteweb"]);
                        directeur = Convert.ToString(dataReader["directeur"]);
                        pays = Convert.ToString(dataReader["pays"]);
                        region = Convert.ToString(dataReader["region"]);
                        ministere = Convert.ToString(dataReader["ministere"]);
                        country = Convert.ToString(dataReader["country"]);
                        regionA = Convert.ToString(dataReader["regiona"]);
                        annee = Convert.ToInt16(dataReader["annee"]);
                        departement = Convert.ToString(dataReader["departement"]);
                        ville = Convert.ToString(dataReader["ville"]);
                        titreDuChef = Convert.ToString(dataReader["titreDuChef"]);
                        REPERTOIRE_PHOTO = Convert.ToString(dataReader["REPERTOIRE_PHOTO"]);
                        //FICHIER_LANGUE = Convert.ToString(dataReader["FICHIER_LANGUE"]);
                        m = new ParametresBE(idParametre, nomEcole, adresse, tel, fax, email, siteWeb, directeur, pays, region,
                            ministere, Convert.ToString(dataReader["ministery"]), country, regionA, annee, departement, Convert.ToString(dataReader["department"]), ville,
                            titreDuChef, Convert.ToString(dataReader["titleofchief"]), Convert.ToString(dataReader["logo"]), REPERTOIRE_PHOTO);
                        dataReader.Close();
                        // this.con.fermer();
                        return m;
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

        public ParametresBE rechercherDerniereValeur()
        {
            int idParametre;
            string nomEcole;
            string adresse;
            string tel;
            string fax;
            string email;
            string siteWeb;
            string directeur;
            string pays;
            string region;
            string ministere;
            string country;
            string regionA;
            int annee;
            String departement;
            String ville;
            String titreDuChef;

            String REPERTOIRE_PHOTO;
            String FICHIER_LANGUE;

            ParametresBE m;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM parametres WHERE idparametre=(SELECT MAX(idparametre) FROM parametres)";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        idParametre = Convert.ToInt16(dataReader["idparametre"]);
                        nomEcole = Convert.ToString(dataReader["nomecole"]);
                        adresse = Convert.ToString(dataReader["adresse"]);
                        tel = Convert.ToString(dataReader["tel"]);
                        fax = Convert.ToString(dataReader["fax"]);
                        email = Convert.ToString(dataReader["email"]);
                        siteWeb = Convert.ToString(dataReader["siteweb"]);
                        directeur = Convert.ToString(dataReader["directeur"]);
                        pays = Convert.ToString(dataReader["pays"]);
                        region = Convert.ToString(dataReader["region"]);
                        ministere = Convert.ToString(dataReader["ministere"]);
                        country = Convert.ToString(dataReader["country"]);
                        regionA = Convert.ToString(dataReader["regiona"]);
                        annee = Convert.ToInt16(dataReader["annee"]);
                        departement = Convert.ToString(dataReader["departement"]);
                        ville = Convert.ToString(dataReader["ville"]);
                        titreDuChef = Convert.ToString(dataReader["titreDuChef"]);

                        REPERTOIRE_PHOTO = Convert.ToString(dataReader["REPERTOIRE_PHOTO"]);
                        //FICHIER_LANGUE = Convert.ToString(dataReader["FICHIER_LANGUE"]);

                        m = new ParametresBE(idParametre, nomEcole, adresse, tel, fax, email, siteWeb, directeur, pays, region,
                            ministere, Convert.ToString(dataReader["ministery"]), country, regionA, annee, departement, Convert.ToString(dataReader["department"]), ville,
                            titreDuChef, Convert.ToString(dataReader["titleofchief"]), Convert.ToString(dataReader["logo"]), REPERTOIRE_PHOTO);
                        dataReader.Close();
                        // this.con.fermer();
                        return m;
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
        //************************ FIN rechercher d'un objet à partir de l'id, parametre obj, retourne l'objet
        
        // retourner la liste de tout les objets
        public override List<ParametresBE> listerTous()
        {
            try
            {
                List<ParametresBE> listParamBE = new List<ParametresBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM parametres";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {

                        ParametresBE paramBE = new ParametresBE(Convert.ToInt16(dataReader["idparametre"]), Convert.ToString(dataReader["nomecole"]),
                            Convert.ToString(dataReader["adresse"]), Convert.ToString(dataReader["tel"]), Convert.ToString(dataReader["fax"]),
                            Convert.ToString(dataReader["email"]), Convert.ToString(dataReader["siteweb"]), Convert.ToString(dataReader["directeur"]),
                            Convert.ToString(dataReader["pays"]), Convert.ToString(dataReader["region"]), Convert.ToString(dataReader["ministere"]), Convert.ToString(dataReader["ministery"]),
                            Convert.ToString(dataReader["country"]), Convert.ToString(dataReader["regiona"]), Convert.ToInt16(dataReader["annee"]),
                            Convert.ToString(dataReader["departement"]), Convert.ToString(dataReader["department"]), Convert.ToString(dataReader["ville"]),
                            Convert.ToString(dataReader["titreDuChef"]), Convert.ToString(dataReader["titleofchief"]), Convert.ToString(dataReader["logo"]), Convert.ToString(dataReader["REPERTOIRE_PHOTO"]));
                        listParamBE.Add(paramBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listParamBE.Count != 0)
                        return listParamBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //*************************** retourner la liste des objets qui correspondent à un certain critère
        public override List<ParametresBE> listerSuivantCritere(String critere)
        {
            try
            {
                List<ParametresBE> listobjBE = new List<ParametresBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM parametres WHERE " + critere;

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        ParametresBE paramBE = new ParametresBE(Convert.ToInt16(dataReader["idparametre"]), Convert.ToString(dataReader["nomecole"]),
                            Convert.ToString(dataReader["adresse"]), Convert.ToString(dataReader["tel"]), Convert.ToString(dataReader["fax"]),
                            Convert.ToString(dataReader["email"]), Convert.ToString(dataReader["siteweb"]), Convert.ToString(dataReader["directeur"]),
                            Convert.ToString(dataReader["pays"]), Convert.ToString(dataReader["region"]), Convert.ToString(dataReader["ministere"]), Convert.ToString(dataReader["ministery"]),
                            Convert.ToString(dataReader["country"]), Convert.ToString(dataReader["regiona"]), Convert.ToInt16(dataReader["annee"]),
                            Convert.ToString(dataReader["departement"]), Convert.ToString(dataReader["department"]), Convert.ToString(dataReader["ville"]),
                            Convert.ToString(dataReader["titreDuChef"]), Convert.ToString(dataReader["titleofchief"]), Convert.ToString(dataReader["logo"]), Convert.ToString(dataReader["REPERTOIRE_PHOTO"]));
                        listobjBE.Add(paramBE);
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
        //*************************** retourner la liste des objets qui correspondent à un certain critère

        public override List<string> listerValeursColonne(string colonne)
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM parametres";

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
                cmd.CommandText = "SELECT COUNT(*) FROM parametres";

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

        internal int AnneeEnCours()
        {
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT MAX(annee) FROM parametres";
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DateTime.Today.Year;
            }
        }

        //retourne les paramètre de l'application
        public ParametresBE getParametre()
        {
            int idParametre;
            string nomEcole;
            string adresse;
            string tel;
            string fax;
            string email;
            string siteWeb;
            string directeur;
            string pays;
            string region;
            string ministere;
            string country;
            string regionA;
            int annee;
            String departement;
            String ville;
            String titreDuChef;

            ParametresBE m;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM parametres";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        idParametre = Convert.ToInt16(dataReader["idparametre"]);
                        nomEcole = Convert.ToString(dataReader["nomecole"]);
                        adresse = Convert.ToString(dataReader["adresse"]);
                        tel = Convert.ToString(dataReader["tel"]);
                        fax = Convert.ToString(dataReader["fax"]);
                        email = Convert.ToString(dataReader["email"]);
                        siteWeb = Convert.ToString(dataReader["siteweb"]);
                        directeur = Convert.ToString(dataReader["directeur"]);
                        pays = Convert.ToString(dataReader["pays"]);
                        region = Convert.ToString(dataReader["region"]);
                        ministere = Convert.ToString(dataReader["ministere"]);
                        country = Convert.ToString(dataReader["country"]);
                        regionA = Convert.ToString(dataReader["regiona"]);
                        annee = Convert.ToInt16(dataReader["annee"]);
                        departement = Convert.ToString(dataReader["departement"]);
                        ville = Convert.ToString(dataReader["ville"]);
                        titreDuChef = Convert.ToString(dataReader["titreDuChef"]);
                        m = new ParametresBE(idParametre, nomEcole, adresse, tel, fax, email, siteWeb, directeur, pays, region,
                            ministere, Convert.ToString(dataReader["ministery"]), country, regionA, annee, departement, Convert.ToString(dataReader["department"]), ville,
                            titreDuChef, Convert.ToString(dataReader["titleofchief"]), Convert.ToString(dataReader["logo"]), Convert.ToString(dataReader["REPERTOIRE_PHOTO"]));
                        dataReader.Close();
                        // this.con.fermer();
                        return m;
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
    }
}
